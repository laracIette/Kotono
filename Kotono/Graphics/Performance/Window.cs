namespace Kotono.Graphics.Performance
{
    internal class Window
    {
        internal double FrameRate { get; private set; }

        internal double FrameTime { get; private set; }

        private readonly double[] _frameTimes;

        private int _frameTimeIndex;

        internal double UpdateRate { get; private set; }

        internal double UpdateTime { get; private set; }

        private readonly double[] _updateTimes;

        private int _updateTimeIndex;

        internal Window()
        {
            _frameTimes = new double[30];
            _frameTimeIndex = 0;

            _updateTimes = new double[30];
            _updateTimeIndex = 0;
        }

        internal void Update()
        {
            KT.Print(FrameRate.ToString());
        }

        internal void AddFrameTime(double frameTime)
        {
            _frameTimes[_frameTimeIndex] = frameTime;
            _frameTimeIndex = (_frameTimeIndex + 1) % _frameTimes.Length;

            double sum = 0;

            foreach (double time in _frameTimes)
            {
                sum += time;
            }

            FrameTime = sum / _frameTimes.Length;

            FrameRate = 1 / FrameTime;
        }

        internal void AddUpdateTime(double updateTime)
        {

            _updateTimes[_updateTimeIndex] = updateTime;
            _updateTimeIndex = (_updateTimeIndex + 1) % _updateTimes.Length;

            double sum = 0;

            foreach (double time in _updateTimes)
            {
                sum += time;
            }

            UpdateTime = sum / _updateTimes.Length;

            UpdateRate = 1 / UpdateTime;
        }
    }
}
