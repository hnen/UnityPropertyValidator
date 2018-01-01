using UnityEditor;
using UnityEngine;

namespace PropertyChecker {

    [InitializeOnLoad]
    internal class PropertyMonitor {
        static PropertyMonitor() {
            EditorApplication.hierarchyWindowChanged += OnUnityHierarchyWindowChanged;
        }

        static void OnUnityHierarchyWindowChanged() {
            Debug.LogWarning("HIERARCHY WINDOW CHANGED");
        }

        public static void OnPropertyChanged(object instance, SerializedProperty property) {
            Debug.LogWarning("PROPERTY " + property.name + " CHANGED");
        }

    }
}

