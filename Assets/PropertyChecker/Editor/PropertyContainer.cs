using UnityEngine;
using System;
using System.Collections.Generic;

namespace PropertyChecker {

    class PropertyKey {
        public int InstanceId { get; private set; }
        public string PropertyName { get; private set; }

        public static PropertyKey Create(MonoBehaviour mb, string propertyName) {
            var key = new PropertyKey();
            key.InstanceId = mb.GetInstanceID();
            key.PropertyName = propertyName;
            return key;
        }
    }

    internal class PropertyContainerEntry {
        public MonoBehaviour Instance { get; private set; }
        public string PropertyName { get; private set; }
        public PropertyInfo PropertyInfo { get; private set; }

        public PropertyContainerEntry(MonoBehaviour instance, string propertyName, PropertyInfo propertyInfo) {
            this.Instance = instance;
            this.PropertyName = propertyName;
            this.PropertyInfo = propertyInfo;
        }

    }

    internal class PropertyContainer {

        static PropertyContainer instance;

        public static PropertyContainer Instance {
            get {
                if (instance == null) {
                    instance = new PropertyContainer();
                }
                return instance;
            }
        }

        Dictionary<PropertyKey, PropertyContainerEntry> container;

        PropertyContainer() {
            this.container = new Dictionary<PropertyKey, PropertyContainerEntry>();
        }

        public void Reset() {
            this.container.Clear();
        }

        public void Register(PropertyInfo info, MonoBehaviour instance, string propertyName) {
            this.container[PropertyKey.Create(instance, propertyName)] = new PropertyContainerEntry(instance, propertyName, info);
        }

    }

}

