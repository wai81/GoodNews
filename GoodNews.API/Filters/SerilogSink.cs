using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.API.Filters
{
    public class SerilogSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;

        public SerilogSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);
            var logLevel = logEvent.Level;

            Console.WriteLine($"{DateTime.Now} [{logLevel.ToString().ToUpperInvariant()}] {message}");
        }

        
    }
}
