namespace MessagePackIssue
{
    using MessagePack;
    using MessagePack.Formatters;
    using System;

    public sealed class CustomResolver : IFormatterResolver
    {
        public static readonly CustomResolver Instance = new CustomResolver();

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            var t = typeof(T);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Not resolved {t.FullName}");
            Console.ForegroundColor = ConsoleColor.Gray;
            return null;
        }
    }
}
