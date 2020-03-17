namespace MessagePackIssue
{
    using MessagePack;
    using MessagePack.Formatters;
    using System;

    public sealed class NotFoundResolver : IFormatterResolver
    {
        public static readonly NotFoundResolver Instance = new NotFoundResolver();

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            var t = typeof(T);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No resolver have been found for {t.FullName}");
            Console.ForegroundColor = ConsoleColor.Gray;
            return null;
        }
    }
}
