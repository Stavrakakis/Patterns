namespace AspNet5.Logging
{
    using System;

    public interface ILoggerFactory
    {
        ILogger GetLogger<T>();

        ILogger GetLogger(Type context);
    }
}
