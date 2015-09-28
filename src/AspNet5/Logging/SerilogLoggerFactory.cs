namespace AspNet5.Logging
{
    using System;

    public class SerilogLoggerFactory : ILoggerFactory
    {
        public ILogger GetLogger(Type context)
        {
            var serilogLogger = Serilog.Log.ForContext(context);
            return new SerilogLogger(serilogLogger);
        }

        public ILogger GetLogger<T>()
        {
            var serilogLogger = Serilog.Log.ForContext<T>();
            return new SerilogLogger(serilogLogger);
        }
    }
}
