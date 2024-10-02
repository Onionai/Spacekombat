using Onion_AI;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathController))]
public class PathControllerEditor : Editor
{
    SerializedProperty pathWay1Property;
    SerializedProperty pathWay2Property;
    SerializedProperty pathWay3Property;
    SerializedProperty pathWay4Property;
    SerializedProperty allPathWaysProperty;

    public override void OnInspectorGUI()
    {
        PathController pathWayController = (PathController)target;

        pathWayController.enemyTypePath = (EnemyType)EditorGUILayout.EnumPopup("EnemyType", pathWayController.enemyTypePath);

        if(pathWayController.enemyTypePath == EnemyType.FreeRoam)
        {
            SetProperties("Pathway 01", "wayPoints01Nodes", pathWay1Property);
            SetProperties("Pathway 02", "wayPoints02Nodes", pathWay2Property);
            SetProperties("Pathway 03", "wayPoints03Nodes", pathWay3Property);
            SetProperties("Pathway 04", "wayPoints04Nodes", pathWay4Property);
        }
        else
        {
            SetProperties("Pathways", "allWayPointsNodes", allPathWaysProperty);
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void SetProperties(string label, string property, SerializedProperty serializedProperty)
    {
        serializedProperty = serializedObject.FindProperty(property);
        EditorGUILayout.PropertyField(serializedProperty, new GUIContent(label), true);
    }
}
