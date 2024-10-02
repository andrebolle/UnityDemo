using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Camera mainCamera;      // Reference to the main player camera
    public Camera reflectionCamera; // Reference to the reflection (mirror) camera
    public Transform mirrorPlane;   // Reference to the mirror surface (plane)

    void LateUpdate()
    {
        Vector3 cameraDirection = mainCamera.transform.position - mirrorPlane.position;
        cameraDirection = Vector3.Reflect(cameraDirection, mirrorPlane.forward);

        reflectionCamera.transform.position = mirrorPlane.position + cameraDirection;
        reflectionCamera.transform.forward = Vector3.Reflect(mainCamera.transform.forward, mirrorPlane.forward);
    }
}
