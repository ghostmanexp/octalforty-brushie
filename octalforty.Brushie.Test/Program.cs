using System;
using System.Collections.Generic;
using System.Text;

using octalforty.Brushie.Instrumentation.Core;
using octalforty.Brushie.Instrumentation.Core.Messages;
using octalforty.Brushie.Instrumentation.Core.Util;

namespace octalforty.Brushie.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PerformanceCounter performanceCounter = PerformanceCounter.AutostartPerformanceCounter();

            for(int i = 0; i < 100; ++i)
                InstrumentationManager.Instrument(new TextMessage(MessageSeverity.Debug, "asdf",
                    DateTime.Now, "Hi!"));
            
            Console.WriteLine("Persisted in {0} sec.", performanceCounter.Finish());
        }
    }
}
