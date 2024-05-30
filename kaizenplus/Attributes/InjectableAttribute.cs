using System;

namespace kaizenplus.Attributes
{
    /// <summary>
    /// Marks class for dependency injection auto registration.
    /// Every time an instance is requested, a new version is created of the class
    /// </summary>
    public class TransientInjectableAttribute : Attribute
    {
    }

    /// <summary>
    /// Marks class for dependency injection auto registration.
    /// A single instance will be created thoughout the lifetime of an http request
    /// </summary>
    public class ScopedInjectableAttribute : Attribute
    {
    }

    /// <summary>
    /// Marks class for dependency injection auto registration.
    /// A single instance will be created thoughout the lifetime of the application
    /// </summary>
    public class SingeltonInjectableAttribute : Attribute
    {
    }
}