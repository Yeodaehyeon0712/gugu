using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.UIElements;

[System.Serializable]
[CustomEditor(typeof(SlideToggle))]
[CanEditMultipleObjects]
public class SlideToggleSerializableEditor : UnityEditor.UI.ButtonEditor
{
    SlideToggle _slideToggle => (SlideToggle)target;
    SerializedProperty serializedProperty;
    protected override void OnEnable()
    {
        base.OnEnable();

        serializedProperty = serializedObject.FindProperty("sliderColorArr");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Dynamic Sprite Properties");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginVertical();

        using (var check = new EditorGUI.ChangeCheckScope())
        {
            EditorGUILayout.PropertyField(serializedProperty, true);

            if (check.changed)
                serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.EndVertical();
    }
}
