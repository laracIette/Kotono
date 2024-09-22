namespace Kotono.Graphics.Objects.Lights
{
    internal sealed class SpotLight : AttenuatedLight
    {
        internal const int MAX_COUNT = 100;

        internal float CutoffAngle { get; set; }

        internal float OuterCutoffAngle { get; set; }

        private bool _isShrink = false;

        public override void Update()
        {
            if (_isShrink)
            {
                if (OuterCutoffAngle >= 5.0f)
                {
                    CutoffAngle += 10.0f * Time.Delta;
                }
                OuterCutoffAngle += 10.0f * Time.Delta;
            }
            else
            {
                CutoffAngle -= 10.0f * Time.Delta;
                OuterCutoffAngle -= 10.0f * Time.Delta;
            }

            CutoffAngle = Math.Clamp(CutoffAngle, 0.0f, 12.5f);
            OuterCutoffAngle = Math.Clamp(OuterCutoffAngle, 0.0f, 17.5f);

            IsOn = OuterCutoffAngle != 0.0f;
        }

        private void OnFKeyPressed() => _isShrink = !_isShrink;
    }
}
