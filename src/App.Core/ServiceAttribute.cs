

namespace App.Core;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ServiceAttribute : Attribute
{
    public ServiceAttribute(InstanceLifetime instanceLifetime = InstanceLifetime.Transient)
    {
        InstanceLifetime = instanceLifetime;
    }

    public InstanceLifetime InstanceLifetime { get; set; }
}

public enum InstanceLifetime
{
    /// <summary>
    /// Singleton lifetime services are created the first time they are requested 
    /// </summary>
    Singleton,
    /// <summary>
    /// Transient lifetime services are created each time they are requested
    /// </summary>
    Transient
}
