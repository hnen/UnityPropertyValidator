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

        public static PropertyInfo GetPropertyInfo(System.Type type, SerializedProperty property, MonoBehaviour [] componentInstances) {
            var field = type.GetField(property.name, 
                            BindingFlags.Instance | 
                            BindingFlags.Public | 
                            BindingFlags.NonPublic);            

            var isOptional = field == null || field.GetCustomAttributes(true).Any(a => a is OptionalAttribute);
            var isAssigned = field == null || AreAllValuesAssigned(field, componentInstances);
            return new PropertyInfo(isOptional, isAssigned);
        }

        static bool AreAllValuesAssigned(FieldInfo field, MonoBehaviour [] instances) {
            foreach(var instance in instances) {
                var value = field.GetValue(instance);
                if (value == null || value.ToString() == "null") {
                    return false;
                }
            }
            return true;
        }

    }

}
