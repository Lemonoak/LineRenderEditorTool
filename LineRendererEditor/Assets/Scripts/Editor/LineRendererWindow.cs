using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class LineRendererWindow : EditorWindow
{
    LineRenderer MyLineRenderer;

    [MenuItem("Window/Custom/Shape Editor")]
    static void Init()
    {
        LineRendererWindow window = (LineRendererWindow)GetWindow(typeof(LineRendererWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        MyLineRenderer = (LineRenderer)EditorGUILayout.ObjectField(MyLineRenderer, typeof(LineRenderer), true);
        if(MyLineRenderer)
        {
            CustomLineRenderer Selected = MyLineRenderer.GetComponent<CustomLineRenderer>();
            Assert.IsNotNull(Selected, "Object have no Custom Line Renderer");

            EditorGUILayout.Separator();

            EditorGUI.BeginChangeCheck();
            Selected.Sides = EditorGUILayout.IntSlider("Sides", Selected.Sides, 3, 100);
            Selected.Radius = EditorGUILayout.Slider("Radius", Selected.Radius, 0, 100);
            Selected.Width = EditorGUILayout.Slider("Width", Selected.Width, 0, 100);
            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(Selected, "Changed LineRenderer Variables through scene");
                if (EditorApplication.isPlaying)
                {
                    Selected.CreatePoints();
                }
            }
            //This is just for updating the actual variables in the editor window (should be more optimized for actual use)
            Repaint();
        }

    }
}
