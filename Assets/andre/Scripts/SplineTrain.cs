using UnityEngine;
using UnityEngine.Splines;

public class SplineTrain : MonoBehaviour
{
    // Assign this via the inspector or dynamically
    public SplineContainer splineContainer;

    // The GameObject you want to move along the spline
    public GameObject trainObject;

    // Speed at which the object moves along the spline
    public float speed = 2.0f;

    // Private variables to track progress along the spline
    private float t = 0f;

    void Update()
    {
        if (splineContainer != null && trainObject != null)
        {
            // Increase the progress value 't' by speed, adjust by time to be frame-rate independent
            t += speed * Time.deltaTime;

            // Keep t between 0 and 1 for looping
            t %= 1.0f; // If t exceeds 1, reset it back to 0 for looping

            // Calculate the position and rotation on the spline at time t
            Spline spline = splineContainer.Spline;
            Vector3 position = spline.EvaluatePosition(t);
            //Quaternion rotation = spline.EvaluateRotation(t);

            // Move the trainObject to the new position and rotation
            trainObject.transform.position = position;
            //trainObject.transform.rotation = rotation;
        }
    }
}
