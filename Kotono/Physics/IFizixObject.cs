﻿namespace Kotono.Physics
{
    internal interface IFizixObject
    {
        public bool IsFizix { get; set; }

        public void UpdateFizix();
    }
}
