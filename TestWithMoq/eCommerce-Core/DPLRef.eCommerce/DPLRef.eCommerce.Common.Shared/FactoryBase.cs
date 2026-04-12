using DPLRef.eCommerce.Common.Contracts;
using System;
using System.Collections.Generic;

namespace DPLRef.eCommerce.Common.Shared
{
    public abstract class FactoryBase
    {
        // contains the dictionary of types supported by this factory (Singleton)
        private static readonly Dictionary<string, object> _singletons = new Dictionary<string, object>();

        // contains the dictionary of overrides provided for this factory instance
        private readonly Dictionary<string, object> _overrides = new Dictionary<string, object>();

        // contains the dictionary of types supported by this factory
        private readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();

        protected FactoryBase(AmbientContext context) =>
            // context is provided to enable attachment to each instance provided by the factory
            Context = context;

        public AmbientContext Context { get; }

        /// <summary>
        /// Provides mock override objects for testing purposes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void AddOverride<T>(T obj)
        {
            if(_overrides.ContainsKey(typeof(T).Name))
            {
                _ = _overrides.Remove(typeof(T).Name);
            }

            _overrides.Add(typeof(T).Name, obj);
        }

        /// <summary>
        /// Configure the types supported by this factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void AddType<T>(Type obj)
        {
            if(_types.ContainsKey(typeof(T).Name))
            {
                _ = _types.Remove(typeof(T).Name);
            }

            _types.Add(typeof(T).Name, obj);
        }

        /// <summary>
        /// Configure the types supported by this factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void AddSingleton<T>(T obj)
        {
            if(_singletons.ContainsKey(typeof(T).Name))
            {
                _ = _singletons.Remove(typeof(T).Name);
            }

            _singletons.Add(typeof(T).Name, obj);
        }

        protected T GetInstanceForType<T>() where T : class
        {
            // Return the override, if one exists for the type T
            if(_overrides.ContainsKey(typeof(T).Name))
            {
                return _overrides[typeof(T).Name] as T;
            }

            // Return the singleon, if one exists for the type T
            if(_singletons.ContainsKey(typeof(T).Name))
            {
                return _singletons[typeof(T).Name] as T;
            }

            // No override, so return an instance of the type from the configured types
            if(_types.ContainsKey(typeof(T).Name))
            {
                var type = _types[typeof(T).Name];
                if(type != null)
                {
                    return Activator.CreateInstance(type) as T;
                }
            }

            // Oops, no override OR configuration for this type
            throw new ArgumentException($"{typeof(T).Name} is not supported by this factory");
        }
    }
}