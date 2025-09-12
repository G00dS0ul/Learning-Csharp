namespace StopWatch
{
    public class StartStopWatch
    {
        private DateTime startTime;
        private DateTime stopTime;
        private bool running;

        public void StartTime(DateTime input)
        {
            if (running)
                throw new InvalidOperationException("The stopwatch is already running.");
            startTime = input;
            stopTime = default; // clear any previous stop
            running = true;
        }

        public void StopTime(DateTime input)
        {
            if (!running)
                throw new InvalidOperationException("Stopwatch has not been started.");
            stopTime = input;
            running = false; // allow starting again
        }

        public TimeSpan Duration()
        {
            if (running)
                throw new InvalidOperationException("Stopwatch is still running. Stop it before getting the duration.");

            if (startTime == default || stopTime == default)
                throw new InvalidOperationException("Start and stop the stopwatch before getting the duration.");

            return stopTime - startTime;
        }
    }
}