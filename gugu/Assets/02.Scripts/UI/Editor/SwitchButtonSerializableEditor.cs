using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(SwitchButton))]
[CanEditMultipleObjects]
public class SwitchButtonSerializableEditor : UnityEditor.UI.ButtonEditor
{
    SwitchButton _switchButton;

    SerializedProperty _onImgProperty;
    SerializedProperty _offImgProperty;
    SerializedProperty _targetGraphicProperty;
    SerializedProperty _onSoundProperty;
    SerializedProperty _offSoundProperty;
    protected override void OnEnable()
    {
        base.OnEnable();

        _onImgProperty = serializedObject.FindProperty("_onImage");
        _offImgProperty = serializedObject.FindProperty("_offImage");
        _onSoundProperty = serializedObject.FindProperty("_onClickSoundKey");
        _offSoundProperty = serializedObject.FindProperty("_offClickSoundKey");
        _targetGraphicProperty = serializedObject.FindProperty("_targetGraphic");
        if (_switchButton == null)
        {
            _switchButton = (SwitchButton)target;
            if (_switchButton.TargetGraphic == null)
            {
                _switchButton.TargetGraphic = _switchButton.GetComponent<UnityEngine.UI.Image>();
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(_onImgProperty, true);
        EditorGUILayout.PropertyField(_onSoundProperty, true);
        EditorGUILayout.PropertyField(_offImgProperty, true);
        EditorGUILayout.PropertyField(_offSoundProperty, true);
        EditorGUILayout.PropertyField(_targetGraphicProperty, true);
        EditorGUILayout.EndVertical();
        if(EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
}
