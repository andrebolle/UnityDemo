using UnityEngine;

public class GLKochRenderer : MonoBehaviour
{
    public Material lineMaterial;
    public Vector3 pointA = new Vector3(0, 0, 0);
    public Vector3 pointB = new Vector3(1, 1, 1);
    public int depth = 8;

    void OnRenderObject()
    {
        if (lineMaterial == null) return;

        // Apply the line material
        lineMaterial.SetPass(0);

        // Start drawing the Koch curve
        GL.Begin(GL.LINES);
        GL.Color(Color.red);

        // Draw Koch curve between pointA and pointB with a specified recursion depth
        DrawKochCurve(pointA, pointB, depth); // Change recursion depth for more detail

        GL.End(); // End drawing
    }

    // Function to draw the Koch curve
    void DrawKochCurve(Vector3 start, Vector3 end, int depth)
    {
        if (depth == 0)
        {
            // Base case: draw the line segment
            GL.Vertex3(start.x, start.y, start.z);
            GL.Vertex3(end.x, end.y, end.z);
        }
        else
        {
            // Calculate points for subdividing the line into a Koch curve
            Vector3 direction = (end - start) / 3.0f;
            Vector3 point1 = start + direction;
            Vector3 point2 = start + 2 * direction;

            // Find the peak of the equilateral triangle
            Vector3 mid = (point1 + point2) / 2;
            Vector3 normal = new Vector3(-direction.y, direction.x, 0).normalized;
            float height = Mathf.Sqrt(3.0f) / 6.0f * (end - start).magnitude;
            Vector3 peak = mid + normal * height;

            // Recursively draw each segment of the Koch curve
            DrawKochCurve(start, point1, depth - 1);
            DrawKochCurve(point1, peak, depth - 1);
            DrawKochCurve(peak, point2, depth - 1);
            DrawKochCurve(point2, end, depth - 1);
        }
    }

}

