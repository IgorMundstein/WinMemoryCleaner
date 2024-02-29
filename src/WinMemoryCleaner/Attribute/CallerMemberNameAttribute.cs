// Class required because of .NET 4.0 Framework
// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Allows you to obtain the method or property name of the caller to the method.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class CallerMemberNameAttribute : Attribute
    {
    }
}