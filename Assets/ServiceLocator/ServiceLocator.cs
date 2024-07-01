using System;
using System.Collections.Generic;

public class ServiceLocator
{
    private static ServiceLocator _instance = new ServiceLocator();
    private Dictionary<Type, object> services = new Dictionary<Type, object>();

    public static ServiceLocator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ServiceLocator();
            }

            return _instance;
        }
    }

    public bool RegisterService<T>(T service)
    {
        if (service == null) throw new ArgumentNullException(nameof(service));

        return services.TryAdd(typeof(T), service);
            
    }

    public T GetService<T>()
    {
        if (services.TryGetValue(typeof(T), out var service))
        {
            return (T)service;
        }
        throw new InvalidOperationException($"Service of type {typeof(T)} is not registered.");
    }
}