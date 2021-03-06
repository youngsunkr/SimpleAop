﻿using System;
using SimpleAop;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddTransientWithProxy<TInterface, TImplementation>(this IServiceCollection serviceCollection)
        {
            return AddWithProxy(serviceCollection, typeof(TInterface), typeof(TImplementation), ServiceLifetime.Transient);
        }
        
        public static IServiceCollection AddScopedWithProxy<TInterface, TImplementation>(this IServiceCollection serviceCollection)
        {
            return AddWithProxy(serviceCollection, typeof(TInterface), typeof(TImplementation), ServiceLifetime.Scoped);
        }
        
        public static IServiceCollection AddSingletonWithProxy<TInterface, TImplementation>(this IServiceCollection serviceCollection)
        {
            return AddWithProxy(serviceCollection, typeof(TInterface), typeof(TImplementation), ServiceLifetime.Singleton);
        }

        public static IServiceCollection AddWithProxy(this IServiceCollection collection, Type interfaceType, Type implementationType, ServiceLifetime lifetime)
        {
            var proxyType = DynamicProxyFactory.Create(interfaceType, implementationType);
            var serviceDescriptor = new ServiceDescriptor(interfaceType, proxyType, lifetime);
            collection.Add(serviceDescriptor);
            return collection;
        }
    }
}