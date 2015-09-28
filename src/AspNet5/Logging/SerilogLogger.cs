namespace AspNet5.Logging
{
    public class SerilogLogger : ILogger
    {
        private readonly Serilog.ILogger logger;

        public SerilogLogger(Serilog.ILogger logger)
        {
            this.logger = logger;
        }

        public void Log(string message, params object[] args)
        {
            this.logger.Information(message, args);
        }
    }
}
