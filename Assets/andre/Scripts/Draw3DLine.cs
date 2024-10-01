using UnityEngine;

public class Draw3DLine : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Set the number of points for the line
        lineRenderer.positionCount = 2;

        // Set the width of the line
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // Set the material for the line (you can customize this)
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    void Update()
    {
        // Set the positions of the line
        lineRenderer.SetPosition(0, pointA.position);
        lineRenderer.SetPosition(1, pointB.position);
    }
}
