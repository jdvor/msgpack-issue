namespace MessagePackIssue
{
    using MessagePack;
    using MessagePack.Resolvers;
    using MessagePackIssue.TestSubjects;
    using System;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            var opts = Options1();

            ////foreach (var obj in Data.Tests())
            ////{
            ////    Serialize(obj, opts);
            ////}

            var ef = new EventFactory(Array.Empty<Type>());
            var proxy = (IInitializeNewCustomer) ef.Create(typeof(IInitializeNewCustomer));
            proxy.Email = "john.doe@example.com";
            proxy.Age = 43;
            proxy.PreferredLanguage = "en";

            Serialize(proxy, opts);
        }

        private static MessagePackSerializerOptions Options1()
        {
            var resolvers = new IFormatterResolver[]
            {
                NativeGuidResolver.Instance,
                NativeDecimalResolver.Instance,
                NativeDateTimeResolver.Instance,
                StandardResolver.Instance,
                NotFoundResolver.Instance,
            };
            return MessagePackSerializerOptions.Standard
                .WithResolver(CompositeResolver.Create(resolvers))
                .WithOmitAssemblyVersion(true)
                .WithCompression(MessagePackCompression.Lz4BlockArray);
        }

        private static void Serialize(object obj, MessagePackSerializerOptions opts)
        {
            var type = obj.GetType();
            try
            {
                var bytes = MessagePackSerializer.Serialize(type, obj, opts);
                Console.WriteLine($"{type.Name}: OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{type.Name}: FAIL");
                Console.WriteLine(Describe(ex));
                Console.WriteLine();
            }
        }

        private static string Describe(Exception ex)
        {
            var typeNames = new List<string>(4) { ex.GetType().Name };
            Exception e = ex;
            while (e.InnerException != null)
            {
                e = e.InnerException;
                typeNames.Add(e.GetType().Name);
            }

            if (typeNames.Count == 1)
            {
                return $"{ex.Message} ({typeNames[0]})";
            }

            typeNames.Reverse();
            return $"{e.Message} ({string.Join(" -> ", typeNames)})\r\n{e.StackTrace}";
        }
    }
}
