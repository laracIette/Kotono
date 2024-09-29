using System.Reflection;

namespace Kotono.Input
{
    internal sealed record class InputMethod(InputAction InputAction, IObject Instance, MethodInfo MethodInfo);
}
