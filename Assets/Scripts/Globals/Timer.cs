namespace Globals {
    class Timer {
        internal float timePassed {get; private set;}

        internal void AddTime(float deltaTime) {
            IncreaseTime(deltaTime);
        }

        private void IncreaseTime(float deltaTime) {
            timePassed += deltaTime;
        }

        internal void Reset() {
            timePassed = 0f;
        }
    }
}