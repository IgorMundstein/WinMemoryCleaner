using System;
using System.Collections.Generic;
using System.Linq;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Dependency Injection
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// IoC Container
        /// </summary>
        public static class Container
        {
            private static readonly Dictionary<Type, Func<object>> _container = new Dictionary<Type, Func<object>>();
            private static readonly Dictionary<Type, object> _singleton = new Dictionary<Type, object>();

            private static object Create(Type type)
            {
                return Activator.CreateInstance(type, type.GetConstructors().Single().GetParameters().Select(p => Resolve(p.ParameterType)).ToArray());
            }

            /// <summary>
            /// Registers the specified instance.
            /// </summary>
            /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
            /// <param name="instance">The instance.</param>
            /// <exception cref="InvalidOperationException"></exception>
            public static void Register<TImplementation>(TImplementation instance)
            {
                var key = typeof(TImplementation);

                if (_container.ContainsKey(key))
                    throw new InvalidOperationException(string.Format(Localizer.Culture, "{0} is already registered.", key.Name));

                _singleton.Add(typeof(TImplementation), instance);
            }

            /// <summary>
            /// Registers the specified singleton.
            /// </summary>
            /// <typeparam name="TInterface">The type of the interface.</typeparam>
            /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
            /// <param name="singleton">if set to <c>true</c> [singleton].</param>
            /// <exception cref="InvalidOperationException"></exception>
            public static void Register<TInterface, TImplementation>(bool singleton = false) where TImplementation : TInterface
            {
                var key = typeof(TInterface);

                if (_container.ContainsKey(key))
                    throw new InvalidOperationException(string.Format(Localizer.Culture, "{0} is already registered.", key.Name));

                if (singleton)
                    _singleton.Add(typeof(TImplementation), null);

                _container.Add(key, () => Resolve<TImplementation>());
            }

            /// <summary>
            /// Resolves the specified type.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            /// <exception cref="InvalidOperationException"></exception>
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

            /// <summary>
            /// Resolves this instance.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static T Resolve<T>()
            {
                return (T)Resolve(typeof(T));
            }
        }
    }
}
