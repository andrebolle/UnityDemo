using UnityEngine;

public class FootballPitch : MonoBehaviour
{
    public Texture2D footballPitchTexture;

    void Start()
    {
        // Create a new plane object
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.name = "Football Pitch";

        // Get the mesh renderer component of the plane
        MeshRenderer meshRenderer = plane.GetComponent<MeshRenderer>();

        // Assign the football pitch texture to the mesh renderer
        meshRenderer.material.mainTexture = footballPitchTexture;

        // Adjust the plane's size and position to fit the football pitch
        plane.transform.localScale = new Vector3(10, 1, 10); // Adjust the scale as needed
        plane.transform.position = Vector3.zero;
    }
}