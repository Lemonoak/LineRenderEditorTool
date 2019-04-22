using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomLineRenderer))]
public class LineRenderEditor : Editor
{
    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        CustomLineRenderer Selected = (CustomLineRenderer)target;

        EditorGUI.BeginChangeCheck();
        Selected.Sides = (int)EditorGUILayout.Slider("Sides", Selected.Sides, 0, 100);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(Selected, "Changed Vertex amount");
            Selected.CreatePoints();
        }

        Selected.Radius = EditorGUILayout.Slider("Radius", Selected.Radius, 0, 100);
        Selected.Width = EditorGUILayout.Slider("Width", Selected.Width, 0, 100);
        if (GUILayout.Button("Refresh"))
        {
            Selected.CreatePoints();
        }
    }

    //Will be used for task 2
    public void OnSceneGUI()
    {
        CustomLineRenderer Selected = (CustomLineRenderer)target;
        Handles.color = Color.blue;
        Selected.Radius = Handles.RadiusHandle(Quaternion.identity, Selected.transform.position, Selected.Radius);
        Handles.ScaleSlider(Selected.Sides, Selected.transform.position, Selected.transform.right, Quaternion.identity, 1f, 1f);
    }

}
