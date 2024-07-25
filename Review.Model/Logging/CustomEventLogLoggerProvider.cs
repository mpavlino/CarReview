using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Review.Model.Logging {
    public class CustomEventLogLoggerProvider : ILoggerProvider {
        private readonly string _logName;
        private readonly string _sourceName;

        public CustomEventLogLoggerProvider( string logName, string sourceName ) {
            _logName = logName;
            _sourceName = sourceName;

            if( !EventLog.SourceExists( _sourceName ) ) {
                EventLog.CreateEventSource( new EventSourceCreationData( _sourceName, _logName ) );
            }
            else {
                var currentLogName = EventLog.LogNameFromSourceName( _sourceName, "." );
                if( currentLogName != _logName ) {
                    EventLog.DeleteEventSource( _sourceName );
                    EventLog.CreateEventSource( new EventSourceCreationData( _sourceName, _logName ) );
                }
            }
        }

        public ILogger CreateLogger( string categoryName ) {
            return new CustomEventLogLogger( _logName, _sourceName );
        }

        public void Dispose() {
        }
    }
}
