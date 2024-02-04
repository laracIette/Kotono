﻿using Kotono.Graphics.Objects.Managers;

namespace Kotono.Graphics.Shaders
{
    internal class PointLightShader()
        : Shader("pointLight")
    {
        internal override void Update()
        {
            base.Update();

            SetMatrix4("view", CameraManager.ActiveCamera.ViewMatrix);
            SetMatrix4("projection", CameraManager.ActiveCamera.ProjectionMatrix);
        }
    }
}
