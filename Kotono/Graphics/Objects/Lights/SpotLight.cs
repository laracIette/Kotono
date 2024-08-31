namespace Kotono.Graphics.Objects.Lights
{
    internal class SpotLight : AttenuatedLight
    {
        internal const int MAX_COUNT = 1;

        internal float CutOffAngle { get; set; }

        internal float OuterCutOffAngle { get; set; }

        public override void Update()
        {
            if (IsOn)
            {
                if (OuterCutOffAngle >= 5.0f)
                {
                    CutOffAngle += 100.0f * Time.Delta;
                }
                OuterCutOffAngle += 100.0f * Time.Delta;
            }
            else
            {
                CutOffAngle -= 100.0f * Time.Delta;
                OuterCutOffAngle -= 100.0f * Time.Delta;
            }

            CutOffAngle = Math.Clamp(CutOffAngle, 0.0f, 12.5f);
            OuterCutOffAngle = Math.Clamp(OuterCutOffAngle, 0.0f, 17.5f);
        }

        private void OnFKeyPressed() => Switch();
    }
}
