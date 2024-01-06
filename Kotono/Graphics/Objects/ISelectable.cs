namespace Kotono.Graphics.Objects
{
    internal interface ISelectable
    {
        public bool IsSelected { get; }

        public bool IsActive { get; }
    }
}
