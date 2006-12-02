using System;
using System.Runtime.InteropServices;

namespace octalforty.Brushie.Instrumentation.Core.Util.Internal
{
    /// <summary>
    /// Contains DllImport directives for native methods.
    /// </summary>
    internal sealed class NativeMethods
    {
        /// <summary>
        /// Retrieves the current value of the high-resolution performance 
        /// counter if one is provided by the OEM.
        /// </summary>
        /// <param name="performanceCount"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceCounter(ref Int64 performanceCount);

        /// <summary>
        /// Retrieves the frequency of the high-resolution performance counter 
        /// if one is provided by the OEM. 
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(ref Int64 frequency);
    }
}