using System;

using octalforty.Brushie.Instrumentation.Core;
using octalforty.Brushie.Instrumentation.Core.Messages;
using octalforty.Brushie.Instrumentation.Core.Util;

namespace octalforty.Brushie.Test
{
    class Program
    {
        static void Main()
        {
            PerformanceCounter performanceCounter = new PerformanceCounter();
            performanceCounter.Start();

            for(int i = 0; i < 10000; ++i)
                InstrumentationManager.Instrument(new TextMessage(MessageSeverity.Debug, "asdf",
                    DateTime.Now, "Hi!"));
            
            Console.WriteLine("Persisted in {0} sec.", performanceCounter.Finish());
        }
    }
}
