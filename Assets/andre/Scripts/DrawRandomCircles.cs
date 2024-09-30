//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DrawRandomCircles : MonoBehaviour
//{
//    // Public variable to assign the plane from the Inspector
//    public GameObject plane;

//    // The texture dimensions
//    public int textureWidth = 512;
//    public int textureHeight = 512;

//    // Number of circles to draw
//    public int numberOfCircles = 10;

//    void Start()
//    {
//        // Create a new texture with the correct format
//        Texture2D texture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);

//        // Fill the texture with a grey background
//        Color[] backgroundPixels = new Color[textureWidth * textureHeight];
//        for (int i = 0; i < backgroundPixels.Length; i++)
//        {
//            backgroundPixels[i] = new Color(0.5f, 0.5f, 0.5f, 1.0f); // Set the background to grey
//        }
//        texture.SetPixels(backgroundPixels);

//        // Draw random circles on the texture
//        for (int i = 0; i < numberOfCircles; i++)
//        {
//            DrawCircle(texture);
//        }

//        // Apply the changes to the texture
//        texture.Apply();

//        // Check if the plane is assigned
//        if (plane != null)
//        {
//            // Ensure the plane has a material that can handle the texture
//            Renderer planeRenderer = plane.GetComponent<Renderer>();
//            if (planeRenderer.material == null || planeRenderer.material.mainTexture == null)
//            {
//                // Create a new material using URP's Unlit shader
//                planeRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
//            }

//            // Assign the texture to the plane's material
//            planeRenderer.material.mainTexture = texture;
//        }
//        else
//        {
//            Debug.LogError("Plane GameObject is not assigned.");
//        }
//    }

//    void DrawCircle(Texture2D texture)
//    {
//        // Randomize circle properties
//        int radius = Random.Range(20, 100);
//        int centerX = Random.Range(radius, textureWidth - radius);
//        int centerY = Random.Range(radius, textureHeight - radius);
//        Color circleColor = new Color(Random.value, Random.value, Random.value, 1.0f); // Ensure the color is fully opaque

//        // Loop through the texture and set pixels to draw the circle
//        for (int y = -radius; y < radius; y++)
//        {
//            for (int x = -radius; x < radius; x++)
//            {
//                if (x * x + y * y <= radius * radius)
//                {
//                    int pixelX = centerX + x;
//                    int pixelY = centerY + y;

//                    // Ensure we don't go outside the texture boundaries
//                    if (pixelX >= 0 && pixelX < textureWidth && pixelY >= 0 && pixelY < textureHeight)
//                    {
//                        texture.SetPixel(pixelX, pixelY, circleColor);
//                    }
//                }
//            }
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOscillatingCircles : MonoBehaviour
{
    // Public variable to assign the plane from the Inspector
    public GameObject plane;

    // The texture dimensions
    public int textureWidth = 512;
    public int textureHeight = 512;

    // Number of circles to draw
    public int numberOfCircles = 10;

    // Struct to hold circle data
    private struct Circle
    {
        public Vector2 center;
        public float baseRadius;
        public float phaseShift;
        public Color color;
    }

    private Circle[] circles;  // Array to hold all circles' data
    private Texture2D texture;  // Texture to update dynamically

    void Start()
    {
        // Initialize texture
        texture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);

        // Initialize array to store circles
        circles = new Circle[numberOfCircles];

        // Generate random circles
        for (int i = 0; i < numberOfCircles; i++)
        {
            Circle circle = new Circle();
            circle.center = new Vector2(Random.Range(50, textureWidth - 50), Random.Range(50, textureHeight - 50));
            circle.baseRadius = Random.Range(20, 50);  // Base radius
            circle.phaseShift = Random.Range(0f, Mathf.PI * 2);  // Different phase for each circle
            circle.color = new Color(Random.value, Random.value, Random.value, 1.0f);  // Random color
            circles[i] = circle;
        }

        // Set initial texture and apply it to the plane
        if (plane != null)
        {
            Renderer planeRenderer = plane.GetComponent<Renderer>();
            if (planeRenderer.material == null || planeRenderer.material.mainTexture == null)
            {
                planeRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            }
            planeRenderer.material.mainTexture = texture;
        }
    }

    void Update()
    {
        // Clear texture (grey background)
        Color[] backgroundPixels = new Color[textureWidth * textureHeight];
        for (int i = 0; i < backgroundPixels.Length; i++)
        {
            backgroundPixels[i] = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        texture.SetPixels(backgroundPixels);

        // Get time value for oscillation
        float time = Time.time;

        // Draw each circle with an oscillating radius
        for (int i = 0; i < numberOfCircles; i++)
        {
            float radius = circles[i].baseRadius + Mathf.Sin(time + circles[i].phaseShift) * 15f;  // Oscillate radius
            DrawCircle(texture, circles[i].center, radius, circles[i].color);
        }

        // Apply the texture changes
        texture.Apply();
    }

    void DrawCircle(Texture2D texture, Vector2 center, float radius, Color color)
    {
        // Loop through the texture and set pixels to draw the circle
        for (int y = -(int)radius; y < (int)radius; y++)
        {
            for (int x = -(int)radius; x < (int)radius; x++)
            {
                if (x * x + y * y <= radius * radius)
                {
                    int pixelX = (int)(center.x + x);
                    int pixelY = (int)(center.y + y);

                    // Ensure we don't go outside the texture boundaries
                    if (pixelX >= 0 && pixelX < textureWidth && pixelY >= 0 && pixelY < textureHeight)
                    {
                        texture.SetPixel(pixelX, pixelY, color);
                    }
                }
            }
        }
    }
}
