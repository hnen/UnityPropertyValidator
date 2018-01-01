using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System;

namespace PropertyChecker {

    [CustomEditor(typeof(MonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class PropertyCheckedEditor : Editor {

        public override void OnInspectorGUI() {
            if (IsEnabledForObject()) {
                DrawPropertyCheckerHeader();
                DrawInspector();
            } else {
                base.DrawDefaultInspector();
            }
        }

        bool IsEnabledForObject() {
            var type = PrefabUtility.GetPrefabType(this.target);
            return type == PrefabType.None || type == PrefabType.PrefabInstance || type == PrefabType.DisconnectedPrefabInstance;
        }

        void DrawPropertyCheckerHeader() {
            var style = new GUIStyle(EditorStyles.label);
            style.fontSize = 7;
            style.normal.textColor = Color.gray;
            GUILayout.Label("Property checker enabled.", style);            
        }

        void DrawInspector() {
            var obj = base.serializedObject;
            //var mb = (MonoBehaviour)base.target;
            var mbs = this.targets.Select(a => (MonoBehaviour)a).ToArray();

            EditorGUI.BeginChangeCheck();
                obj.Update();
                SerializedProperty iterator = obj.GetIterator();
                bool enterChildren = true;
                while (iterator.NextVisible(enterChildren)) {
                    using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath)) {
                        var oval = GetPropertyValue(iterator);
                        DrawProperty(this.target.GetType(), iterator, mbs);
                        var nval = GetPropertyValue(iterator);
                        var propertyInfo = PropertyInfo.GetPropertyInfo(obj.GetType(), iterator, mbs);
                        foreach (var mb in mbs) {
                            PropertyMonitor.OnPropertyUpdated(mb, iterator, propertyInfo, oval != nval);
                        }
                    }
                    enterChildren = false;  
                }
                obj.ApplyModifiedProperties();  
            EditorGUI.EndChangeCheck();               
        }

        static object GetPropertyValue(SerializedProperty sp) {
            if (sp.propertyType == SerializedPropertyType.ObjectReference) {
                return sp.objectReferenceValue;
            } else {
                return null;
            }
        }

        void DrawProperty(System.Type componentType, SerializedProperty property, MonoBehaviour [] instances) {
            var propertyInfo = PropertyInfo.GetPropertyInfo(componentType, property, instances);
            if (propertyInfo.IsOptional) {
                DrawOptionalProperty(property);
            } else {
                if(propertyInfo.HasAssignedValue) {
                    DrawPropertyRequiredValueAssigned(property);
                } else {
                    DrawPropertyRequiredValueMissing(property);
                }
            }            
        }

        void DrawOptionalProperty(SerializedProperty property) {
            EditorGUILayout.PropertyField(property, true, new GUILayoutOption[0]);
        }

        void DrawPropertyRequiredValueMissing(SerializedProperty property) {
            var col = GUI.color;
            GUI.color = Color.red;
            EditorGUILayout.PropertyField(property, true, new GUILayoutOption[0]);
            GUI.color = col;
        }

        void DrawPropertyRequiredValueAssigned(SerializedProperty property) {
            EditorGUILayout.PropertyField(property, true, new GUILayoutOption[0]);
        }
    }
}