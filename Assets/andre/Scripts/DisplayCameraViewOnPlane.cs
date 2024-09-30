using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DisplayCameraViewOnPlane : MonoBehaviour
{
    [Header("Assign the Camera and Plane")]
    [Tooltip("The Camera whose view will be displayed on the Plane.")]
    public Camera sourceCamera;

    [Tooltip("The Plane GameObject where the Camera's view will be displayed.")]
    public GameObject targetPlane;

    [Header("Render Texture Settings")]
    [Tooltip("Width of the RenderTexture.")]
    public int renderTextureWidth = 1920;

    [Tooltip("Height of the RenderTexture.")]
    public int renderTextureHeight = 1080;

    // Private RenderTexture to hold the Camera's output
    private RenderTexture renderTex;

    void Start()
    {
        // Validate assignments
        if (sourceCamera == null)
        {
            Debug.LogError("Source Camera is not assigned in the Inspector.");
            return;
        }

        if (targetPlane == null)
        {
            Debug.LogError("Target Plane is not assigned in the Inspector.");
            return;
        }

        // Create RenderTexture
        renderTex = new RenderTexture(renderTextureWidth, renderTextureHeight, 24);
        renderTex.Create();

        // Assign RenderTexture to the Camera
        sourceCamera.targetTexture = renderTex;

        // Assign RenderTexture to the Plane's material
        Renderer planeRenderer = targetPlane.GetComponent<Renderer>();
        if (planeRenderer != null)
        {
            // Option 1: Use an Unlit Shader to display the texture without lighting effects
            Material planeMaterial = new Material(Shader.Find("Unlit/Texture"));
            planeMaterial.mainTexture = renderTex;
            planeRenderer.material = planeMaterial;

            // Option 2: Alternatively, use the Standard Shader if you want lighting effects
            // Material planeMaterial = new Material(Shader.Find("Standard"));
            // planeMaterial.mainTexture = renderTex;
            // planeRenderer.material = planeMaterial;
        }
        else
        {
            Debug.LogError("Target Plane does not have a Renderer component.");
        }
    }

    void OnDestroy()
    {
        // Clean up the RenderTexture when the object is destroyed
        if (renderTex != null)
        {
            renderTex.Release();
            Destroy(renderTex);
        }
    }
}
