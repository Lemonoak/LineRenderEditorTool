using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomLineRenderer))]
public class LineRenderEditor : Editor
{
    //For task 1 (NOT USED ANYMORE SINCE I NOW HAVE A SEPERATE EDITOR WINDOW)
    public override void OnInspectorGUI()
    {
        CustomLineRenderer Selected = (CustomLineRenderer)target;

        EditorGUI.BeginChangeCheck();
        Selected.Sides = (int)EditorGUILayout.Slider("Sides", Selected.Sides, 3, 100);
        Selected.Radius = EditorGUILayout.Slider("Radius", Selected.Radius, 0, 100);
        Selected.Width = EditorGUILayout.Slider("Width", Selected.Width, 0, 100);
        Selected.DrawEditorCircle = EditorGUILayout.Toggle("Show Gizmo Circle", Selected.DrawEditorCircle);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(Selected, "Changed LineRenderer Variables");
            if (EditorApplication.isPlaying)
            {
                Selected.CreatePoints();
            }
        }
        //This is to update the scene view without having to hover to mouse over it
        SceneView.RepaintAll();
    }
    //For task 2 (IN SCENE VIEW HANDLES)
    public void OnSceneGUI()
    {
        CustomLineRenderer Selected = (CustomLineRenderer)target;

        Color OldColor = Handles.color;
        Handles.color = Color.blue;

        EditorGUI.BeginChangeCheck();
        Selected.Radius = Handles.RadiusHandle(Quaternion.identity, Selected.transform.position, Selected.Radius);
        Selected.Sides = (int)Handles.ScaleSlider(Selected.Sides, Selected.transform.position, Selected.transform.right, Quaternion.identity, HandleUtility.GetHandleSize(new Vector3(1,1,1)), 1f);
        //This is just to limit the handles from going below 3 and over 100 (the limits i put on the sliders)
        Selected.Sides = Mathf.Max(Selected.Sides, 3);
        Selected.Sides = Mathf.Min(Selected.Sides, 100);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(Selected, "Changed LineRenderer Variables through scene");
            if (EditorApplication.isPlaying)
            {
                Selected.CreatePoints();
            }
        }

        Handles.color = OldColor;
    }
}
