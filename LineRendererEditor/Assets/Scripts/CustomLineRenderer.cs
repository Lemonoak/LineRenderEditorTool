using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CustomLineRenderer : MonoBehaviour
{
    LineRenderer Render;

    public bool DrawEditorCircle = true;

    public int Sides = 4;
    public float Radius = 1;
    public float Width = 1;

    void Start()
    {
        //caching the renderer
        Render = GetComponent<LineRenderer>();
        //always makes sure looping is true
        Render.loop = true;

        CreatePoints();
    }

    //Draws the circle during runtime (also refreshes)
    public void CreatePoints()
    {
        Render.widthMultiplier = Width;

        float DeltaTheta = (Mathf.PI * 2.0f) / Sides;
        float Theta = 0.0f;

        Render.positionCount = Sides;
        for (int i = 0; i < Render.positionCount; i++)
        {
            Vector3 SidesPosition = new Vector3(Radius * Mathf.Cos(Theta), Radius * Mathf.Sin(Theta), 0.0f);
            Render.SetPosition(i, SidesPosition + transform.position);
            Theta += DeltaTheta;
        }
    }

#if UNITY_EDITOR
    //Draws in editor how the circle should look
    private void OnDrawGizmos()
    {
        if(DrawEditorCircle)
        {
            float DeltaTheta = (Mathf.PI * 2.0f) / Sides;

            float Theta = 0.0f;

            Vector3 OldPos = Vector3.zero;
            for (int i = 0; i < Sides + 1; i++)
            {
                Vector3 SidesPosition = new Vector3(Radius * Mathf.Cos(Theta), Radius * Mathf.Sin(Theta), 0.0f);
                Gizmos.DrawLine(OldPos, transform.position + SidesPosition);
                OldPos = transform.position + SidesPosition;
                Theta += DeltaTheta;
            }
        }
    }
#endif
}
