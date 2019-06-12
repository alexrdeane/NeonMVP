using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(AnalogueGlitch))]
public class AnalogueGlitchEditor : Editor
{
    //ANALOGUE GLITCH CS SCRIPT
    SerializedProperty _scanLineJitter;
    SerializedProperty _verticalJump;
    SerializedProperty _horizontalShake;
    SerializedProperty _colourDrift;

    private void OnEnable()
    {
        //ANALOGUE GLITCH CS SCRIPT
        _scanLineJitter = serializedObject.FindProperty("_scanLineJitter");
        _verticalJump = serializedObject.FindProperty("_verticalJump");
        _horizontalShake = serializedObject.FindProperty("_horizontalShake");
        _colourDrift = serializedObject.FindProperty("_colourDrift");
    }
    public override void OnInspectorGUI()
    {
        //ANALOGUE GLITCH CS SCRIPT
        serializedObject.Update();
        EditorGUILayout.PropertyField(_scanLineJitter);
        EditorGUILayout.PropertyField(_verticalJump);
        EditorGUILayout.PropertyField(_horizontalShake);
        EditorGUILayout.PropertyField(_colourDrift);
        serializedObject.ApplyModifiedProperties();
    }

}
