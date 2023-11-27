using System;
using System.Collections.Generic;
using System.Linq;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Dependency Injection
    /// </summary>
    internal static class DependencyInjection
    {
        /// <summary>
        /// IoC Container
        /// </summary>
        internal static class Container
        {
            private static readonly Dictionary<Type, Func<object>> _container = new Dictionary<Type, Func<object>>();
            private static readonly Dictionary<Type, object> _singleton = new Dictionary<Type, object>();

            private static object Create(Type type)
            {
                return Activator.CreateInstance(type, type.GetConstructors().Single().GetParameters().Select(p => Resolve(p.ParameterType)).ToArray());
            }

            public static void Register<TImplementation>(TImplementation instance)
            {
                var key = typeof(TImplementation);

                if (_container.ContainsKey(key))
                    throw new InvalidOperationException(string.Format(Localizer.Culture, "{0} is already registered.", key.Name));

                _singleton.Add(typeof(TImplementation), instance);
            }

            public static void Register<TInterface, TImplementation>(bool singleton = false) where TImplementation : TInterface
            {
                var key = typeof(TInterface);

                if (_container.ContainsKey(key))
                    throw new InvalidOperationException(string.Format(Localizer.Culture, "{0} is already registered.", key.Name));

                if (singleton)
                    _singleton.Add(typeof(TImplementation), null);

                _container.Add(key, () => Resolve<TImplementation>());
            }

            private static object Resolve(Type type)
            {
                if (_singleton.ContainsKey(type) && _singleton[type] != null)
                    return _singleton[type];

                Func<object> func;
                object instance = null;

                if (type.IsInterface && _container.TryGetValue(type, out func))
                    instance = func();

                if (!type.IsInterface && instance == null)
                    instance = Create(type);

                if (instance == null)
                    throw new InvalidOperationException(string.Format(Localizer.Culture, "{0} is not registered.", type.Name));

                if (_singleton.ContainsKey(type))
                    _singleton[type] = instance;

                return instance;
            }

            public static T Resolve<T>()
            {
                return (T)Resolve(typeof(T));
            }
        }
    }
}
