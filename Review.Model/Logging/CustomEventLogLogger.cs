using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.Logging {
    public class CustomEventLogLogger : ILogger {
        private readonly string _logName;
        private readonly string _sourceName;

        public CustomEventLogLogger( string logName, string sourceName ) {
            _logName = logName;
            _sourceName = sourceName;
        }

        public IDisposable BeginScope<TState>( TState state ) => null;

        public bool IsEnabled( LogLevel logLevel ) => true;

        public void Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter ) {
            if( !IsEnabled( logLevel ) ) {
                return;
            }

            var message = formatter( state, exception );
            var entryType = GetEntryType( logLevel );

            using( var eventLog = new EventLog( _logName ) { Source = _sourceName } ) {
                eventLog.WriteEntry( message, entryType );
            }
        }

        private EventLogEntryType GetEntryType( LogLevel logLevel ) {
            return logLevel switch {
                LogLevel.Critical => EventLogEntryType.Error,
                LogLevel.Error => EventLogEntryType.Error,
                LogLevel.Warning => EventLogEntryType.Warning,
                LogLevel.Information => EventLogEntryType.Information,
                LogLevel.Debug => EventLogEntryType.Information,
                LogLevel.Trace => EventLogEntryType.Information,
                _ => EventLogEntryType.Information,
            };
        }
    }
}
