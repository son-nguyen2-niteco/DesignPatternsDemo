using System;
using System.Text;

namespace DPD.ConsoleApp
{
    internal static class BuilderDemo2
    {
        public static void Run()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Hello");
            stringBuilder.Append(" world");
            stringBuilder.Append('!');
            stringBuilder.AppendLine();

            var text = stringBuilder.ToString();

            Console.WriteLine(text);
        }
    }
}