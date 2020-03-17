# Failing to serialize emitted class

To demonstrate the issue, simply run the console application.

No resolver "fits" proxy class emitted by this [method](https://github.com/jdvor/msgpack-issue/blob/master/msgpack-issue/EventFactory.cs#L84).

The result is:
```
FormatterNotRegisteredException

MessagePackIssue.TestSubjects.__Concrete.IInitializeNewCustomer is not registered in resolver: MessagePack.Resolvers.CompositeResolver+CachingResolver
   at MessagePack.FormatterResolverExtensions.Throw(Type t, IFormatterResolver resolver)
   at MessagePack.MessagePackSerializer.Serialize[T](MessagePackWriter& writer, T value, MessagePackSerializerOptions options)
```

Questions:
1. What prevents MessagePack to accept proxy just as it would instance of normal compile-time class?
2. (alternatively) Is there something to be done in creating MessagePackSerializerOptions which would bypass the problem?