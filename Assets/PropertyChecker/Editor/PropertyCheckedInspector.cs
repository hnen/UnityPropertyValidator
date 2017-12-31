using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System;

namespace PropertyChecker {

    [CustomEditor(typeof(MonoBehaviour), true)]
    public class PropertyCheckedEditor : Editor {

        public override void OnInspectorGUI() {
            var style = new GUIStyle(EditorStyles.label);
            style.fontSize = 7;
            style.normal.textColor = Color.gray;
            GUILayout.Label("Property checker enabled.", style);            

            var obj = base.serializedObject;

            EditorGUI.BeginChangeCheck();
                obj.Update();
                SerializedProperty iterator = obj.GetIterator();
                bool enterChildren = true;
                while (iterator.NextVisible(enterChildren)) {
                    using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath)) {
                        DrawProperty(this.target, iterator);
                    }
                    enterChildren = false;  
                }
                obj.ApplyModifiedProperties();  
            EditorGUI.EndChangeCheck();   
        }

        void DrawProperty(object instance, SerializedProperty property) {
            var propertyInfo = PropertyChecker.GetPropertyInfo(instance, property);
            //EditorGUILayout.LabelField(string.Format("isOptional: {0}, hasAssignedValue: {1}", propertyInfo.IsOptional, propertyInfo.HasAssignedValue));
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