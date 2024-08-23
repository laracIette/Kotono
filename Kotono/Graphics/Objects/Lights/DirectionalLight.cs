using Kotono.Utils.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotono.Graphics.Objects.Lights
{
    internal class DirectionalLight : Light
    {
        internal Vector Direction { get; set; }
    }
}
