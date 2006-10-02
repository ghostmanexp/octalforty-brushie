using System;
using System.Collections.Generic;
using System.Text;

using octalforty.Brushie.Instrumentation.Core;
using octalforty.Brushie.Instrumentation.Core.Messages;

namespace octalforty.Brushie.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            InstrumentationManager.Instrument(new TextMessage(MessageSeverity.Debug, "asdf",
                DateTime.Now, "Hi!"));
        }
    }
}
