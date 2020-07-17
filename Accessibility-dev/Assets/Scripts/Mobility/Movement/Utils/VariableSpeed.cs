using UnityEngine;

namespace Mobility.Movement.Utils {
    public class VariableSpeed {
        public float current { get; private set; }

        public float regular { get; private set; }

        public float min { get; private set; }

        public float max { get; private set; }

        public bool isLow { get; private set; } = false;

        public VariableSpeed(float regular, float min, float max) {
            this.regular = regular;
            this.min = min;
            this.max = max;

            UpdateSpeed();
        }

        public void UpdateSpeed() {
            isLow = !isLow;
            current = regular * (isLow ? Random.Range(min, 1) : Random.Range(1, max));
        }
    }
}
