using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;

namespace PropertyChecker {

    internal class PropertyInfo {
        public bool IsOptional { get; private set; }
        public bool HasAssignedValue { get; private set; }

        public PropertyInfo(bool isOptional, bool hasAssignedValue) {
            this.IsOptional = isOptional;
            this.HasAssignedValue = hasAssignedValue;
        }
    }

   internal class PropertyChecker {

        public static PropertyInfo GetPropertyInfo(object instance, SerializedProperty property) {
            var type = instance.GetType();
            var field = type.GetField(property.name, 
                            BindingFlags.Instance | 
                            BindingFlags.Public | 
                            BindingFlags.NonPublic);            

            var isOptional = field == null || field.GetCustomAttributes(true).Any(a => a is OptionalAttribute);
            if (property.propertyType == SerializedPropertyType.ObjectReference) {
                var value = property.objectReferenceValue;
                return new PropertyInfo(isOptional, value != null);
            } else if (property.propertyType == SerializedPropertyType.String) {
                var value = property.stringValue;
                return new PropertyInfo(isOptional, !string.IsNullOrEmpty(value));
            } else {
                return new PropertyInfo(isOptional, true);
            }
        }

    }

}
