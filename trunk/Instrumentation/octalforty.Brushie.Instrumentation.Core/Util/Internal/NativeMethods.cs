using System;
using System.Runtime.InteropServices;

namespace octalforty.Brushie.Instrumentation.Core.Util.Internal
{
    /// <summary>
    /// Contains DllImport directives for native methods.
    /// </summary>
    internal sealed class NativeMethods
    {
        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceCounter(ref Int64 performanceCount);

        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(ref Int64 frequency);
    }
}
