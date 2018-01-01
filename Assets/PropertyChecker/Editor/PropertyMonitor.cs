using UnityEditor;
using UnityEngine;

namespace PropertyChecker {

    [InitializeOnLoad]
    internal class PropertyMonitor {
        static PropertyMonitor() {
            EditorApplication.hierarchyWindowChanged += OnUnityHierarchyWindowChanged;
        }

        static void OnUnityHierarchyWindowChanged() {
            //Debug.LogWarning("HIERARCHY WINDOW CHANGED");
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded() {
            //Debug.LogWarning("SCRIPTS RELOADED");
            PropertyContainer.Instance.Reset();
        }

        public static void OnPropertyUpdated(MonoBehaviour instance, SerializedProperty property, PropertyInfo info, bool valueChanged) {
            //Debug.LogWarning("PROPERTY " + property.name + " CHANGED");            
            PropertyContainer.Instance.Register(info, instance, property.name);
        }

    }
}

