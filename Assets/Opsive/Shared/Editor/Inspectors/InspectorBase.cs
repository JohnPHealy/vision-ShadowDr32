/// ---------------------------------------------
/// Opsive Shared
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.Shared.Editor.Inspectors
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Base class for all IMGUI inspectors.
    /// </summary>
    public abstract class InspectorBase : UnityEditor.Editor
    {
        private Dictionary<string, SerializedProperty> m_PropertyStringMap = new Dictionary<string, SerializedProperty>();

        /// <summary>
        /// Draws the custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Show the script field.
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(PropertyFromName("m_Script"));
            if (EditorGUI.EndChangeCheck()) {
                Shared.Editor.Utility.EditorUtility.RecordUndoDirtyObject(target, "Change Value");
                serializedObject.ApplyModifiedProperties();
            }
        }

        /// <summary>
        /// Uses a dictionary to lookup a property from a string key.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The found SerializedProperty.</returns>
        public SerializedProperty PropertyFromName(string name)
        {
            return PropertyFromName(serializedObject, name);
        }

        /// <summary>
        /// Uses a dictionary to lookup a property from a string key.
        /// </summary>
        /// <param name="serializedObject">The object which contains the property.</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>The found SerializedProperty.</returns>
        public SerializedProperty PropertyFromName(SerializedObject serializedObject, string name)
        {
            SerializedProperty property = null;
            if (m_PropertyStringMap.TryGetValue(name, out property)) {
                return property;
            }

            property = serializedObject.FindProperty(name);
            if (property == null) {
                Debug.LogError("Unable to find property " + name);
                return null;
            }
            m_PropertyStringMap.Add(name, property);
            return property;
        }

        /// <summary>
        /// Shortcut for drawing a foldout on the current target.
        /// </summary>
        /// <param name="name">The name of the foldout.</param>
        /// <returns>True if the foldout is expanded.</returns></param>
        protected bool Foldout(string name)
        {
            return Foldout(name, true, string.Empty);
        }

        /// <summary>
        /// Shortcut for drawing a foldout on the current target.
        /// </summary>
        /// <param name="name">The name of the foldout.</param>
        /// <param name="defaultExpanded">The default value if the foldout is expanded.</param>
        /// <returns>True if the foldout is expanded.</returns></param>
        protected bool Foldout(string name, bool defaultExpanded)
        {
            return Foldout(name, defaultExpanded, string.Empty);
        }

        /// <summary>
        /// Shortcut for drawing a foldout on the current target.
        /// </summary>
        /// <param name="name">The name of the foldout.</param>
        /// <param name="identifyingString">A string that can be used to help identify the foldout key.</param>
        /// <returns>True if the foldout is expanded.</returns></param>
        protected bool Foldout(string name, string identifyingString)
        {
            return Foldout(name, true, identifyingString);
        }

        /// <summary>
        /// Shortcut for drawing a foldout on the current target.
        /// </summary>
        /// <param name="name">The name of the foldout.</param>
        /// <param name="defaultExpanded">The default value if the foldout is expanded.</param>
        /// <param name="identifyingString">A string that can be used to help identify the foldout key.</param>
        /// <returns>True if the foldout is expanded.</returns></param>
        protected bool Foldout(string name, bool defaultExpanded, string identifyingString)
        {
            return Utility.InspectorUtility.Foldout(target, new GUIContent(name), defaultExpanded, identifyingString);
        }
    }
}