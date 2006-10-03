using octalforty.Brushie.Instrumentation.Core.Util.Internal;

namespace octalforty.Brushie.Instrumentation.Core.Util
{
    /// <summary>
    /// Simple class, similar to <see cref="System.Diagnostics.Stopwatch"/>, but with a cleaner API,
    /// used to accurately measure time intervals.
    /// </summary>
    public sealed class PerformanceCounter
    {
        #region Private Member Variables
        private long startTime;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets performance counter start time.
        /// </summary>
        public long StartTime
        {
            get { return startTime; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="PerformanceCounter"/> class.
        /// </summary>
        public PerformanceCounter()
        {
        }
        
        /// <summary>
        /// Starts measuring time.
        /// </summary>
        public void Start()
        {
            startTime = 0L;
            NativeMethods.QueryPerformanceCounter(ref startTime);
        }
        
        /// <summary>
        /// Finished measuring time and returns elapsed time in seconds.
        /// </summary>
        /// <returns></returns>
        public float Finish()
        {
            long finishTime = 0L;
            NativeMethods.QueryPerformanceCounter(ref finishTime);

            long frequency = 0L;
            NativeMethods.QueryPerformanceFrequency(ref frequency);

            return ((float)(finishTime - startTime) / (float)frequency);
        }
        
        /// <summary>
        /// Returns a reference to a new started performance counter.
        /// </summary>
        /// <returns></returns>
        public static PerformanceCounter AutostartPerformanceCounter()
        {
            PerformanceCounter performanceCounter = new PerformanceCounter();
            
            performanceCounter.Start();
            return performanceCounter;
        }
    }
}