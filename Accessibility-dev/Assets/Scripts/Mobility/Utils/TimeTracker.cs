namespace Mobility.Utils {
    public class TimeTracker {
        public delegate void OnTimePassed();

        public float timeLeft { get; private set; } = 0f;

        public float timePassed {
            get {
                return seconds - timeLeft;
            }
        }

        private int seconds { get; set; }

        private OnTimePassed onTimePassedCallback { get; set; }

        private bool singleRun { get; set; }

        public int runs { get; private set; } = 0;

        public TimeTracker(int seconds, OnTimePassed onTimePassedCallback, bool singleRun = false) {
            this.seconds = seconds;
            this.timeLeft = (float) seconds;
            this.onTimePassedCallback = onTimePassedCallback;
            this.singleRun = singleRun;
        }

        public void Tick(float newTimePassed) {
            if(runs == 0 || !singleRun)
                timeLeft -= newTimePassed;

            if (timeLeft <= 0 && (runs == 0 || !singleRun)) {
                if(!singleRun) timeLeft += seconds;
                else timeLeft = 0;

                runs++;

                onTimePassedCallback();
            }
        }
    }
}
