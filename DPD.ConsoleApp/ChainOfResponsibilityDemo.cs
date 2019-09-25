using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EnsureThat;

namespace DPD.ConsoleApp
{
    internal static class ChainOfResponsibilityDemo
    {
        public static void Run()
        {
            var parser =
                new IndividualUnitParser(
                    new HourParser(),
                    new MinuteParser()
                );

            var workingMinutes = parser.Handle("1h 30m", null);

            Console.WriteLine(workingMinutes);
        }
    }

    internal class IndividualUnitParser : Handler<string, int>
    {
        public IndividualUnitParser(HourParser hourParser, MinuteParser minuteParser)
        {
            AddHandler(hourParser);
            AddHandler(minuteParser);
        }

        public override int Handle(string input, Func<string, int> next)
        {
            return input.Split(' ').Select(piece => base.Handle(piece, next)).Sum();
        }
    }

    internal class HourParser : IHandler<string, int>
    {
        private readonly Regex _pattern = new Regex("^(\\d+)h$");

        public int Handle(string input, Func<string, int> next)
        {
            if (!_pattern.IsMatch(input))
            {
                return next.Invoke(input);
            }

            var match = _pattern.Match(input);
            var hourAsText = match.Groups[1].Value;

            return (int) Math.Round(double.Parse(hourAsText) * 60);
        }
    }

    internal class MinuteParser : IHandler<string, int>
    {
        private readonly Regex _pattern = new Regex("^(\\d+)m$");

        public int Handle(string input, Func<string, int> next)
        {
            if (!_pattern.IsMatch(input))
            {
                return next.Invoke(input);
            }

            var match = _pattern.Match(input);
            var minuteAsText = match.Groups[1].Value;

            return int.Parse(minuteAsText);
        }
    }

    public interface IHandler<TIn, TOut>
    {
        /// <summary>
        /// Either processes the input then returns result to its caller or passes on the input to the next handler in the chain for further processing.
        /// </summary>
        /// <param name="input">The input object.</param>
        /// <param name="next">The next handler in the chain.</param>
        /// <returns></returns>
        TOut Handle(TIn input, Func<TIn, TOut> next);
    }

    /// <summary>
    /// <para>Represents a composite handler, which comprises of multiple handlers in order to handle a more complicate input.</para>
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    public abstract class Handler<TIn, TOut> : IHandler<TIn, TOut>
    {
        private readonly List<IHandler<TIn, TOut>> _handlers;
        private Func<Func<TIn, TOut>, Func<TIn, TOut>> _chainedDelegate;

        /// <summary>
        /// </summary>
        protected Handler()
        {
            _handlers = new List<IHandler<TIn, TOut>>();
        }

        /// <summary>
        /// Builds a chained delegate from the list of handlers.
        /// </summary>
        private Func<Func<TIn, TOut>, Func<TIn, TOut>> ChainedDelegate
        {
            get
            {
                if (_chainedDelegate != null)
                {
                    return _chainedDelegate;
                }

                Func<Func<TIn, TOut>, Func<TIn, TOut>> chainedDelegate =
                    next => input => _handlers.Last().Handle(input, next);

                for (var index = _handlers.Count - 2; index >= 0; index--)
                {
                    var handler = _handlers[index];
                    var chainedDelegateCloned = chainedDelegate;

                    chainedDelegate = next => input => handler.Handle(input, chainedDelegateCloned(next));
                }

                return _chainedDelegate = chainedDelegate;
            }
        }

        /// <summary>
        /// <para>Invokes handlers one by one until the input has been processed by a handler and returns output, ignoring the rest of the handlers.</para>
        /// <para>It is done by first creating a pipeline execution delegate from existing handlers then invoking that delegate against the input.</para>
        /// </summary>
        /// <param name="input">The input object.</param>
        /// <param name="next">The next handler in the chain. If null is provided, <see cref="ThrowNotSupported{TIn,TOut}"/> will be set as the end of the chain.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if handler list is null.</exception>
        /// <exception cref="ArgumentException">Thrown if handler list is empty.</exception>
        public virtual TOut Handle(TIn input, Func<TIn, TOut> next)
        {
            EnsureArg.HasItems(_handlers, nameof(_handlers));

            if (next == null)
            {
                next = _ => new ThrowNotSupported<TIn, TOut>().Handle(_, null);
            }

            return ChainedDelegate.Invoke(next).Invoke(input);
        }

        /// <summary>
        /// <para>Performs interception to the given <paramref name="handler"/> object.</para>
        /// <para>Then adds the intercepted handler to the last position in the chain.</para>
        /// </summary>
        /// <param name="handler">The handler object.</param>
        protected void AddHandler<THandler>(THandler handler)
            where THandler : class, IHandler<TIn, TOut>
        {
            EnsureArg.IsNotNull(handler, nameof(handler));

            _handlers.Add(handler);
        }
    }

    /// <summary>
    /// A handler that throws <see cref="NotSupportedException"/>. This is usually set as the last handler in the chain.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    public class ThrowNotSupported<TIn, TOut> : IHandler<TIn, TOut>
    {
        /// <summary>
        /// Throws <see cref="NotSupportedException"/> on invocation.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public virtual TOut Handle(TIn input, Func<TIn, TOut> next)
        {
            throw new NotSupportedException($"Cannot handle this input. Input information: {typeof(TIn)}");
        }
    }
}