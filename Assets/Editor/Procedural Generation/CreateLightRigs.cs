using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CreateLightRigs : EditorWindow
{
    private static Vector3 fieldCenter;
    private static Vector3 fieldSize = new(50f, 0f, 30f); // Size of the field (width, height, depth)

    // Parameters for the structure
    //static int numberOfPoles = 4;
    static readonly float poleHeight = 20f;
    static readonly float poleRadius = 0.5f;
    static readonly int floodlightRows = 2;
    static readonly int floodlightsPerRow = 4;
    static readonly float floodlightSpacing = 1.5f;
    static readonly float floodlightSize = 0.5f;

    // Emissive Materials for the floodlights
    private readonly static Material floodlightMaterialCyan;
    private static Material floodlightMaterialMagenta;
    private readonly static Material floodlightMaterialYellow;
    private readonly static Material floodlightMaterialWhite;


    // Create a menu item in the Unity Editor toolbar
    [MenuItem("Tools/Create Light Rigs")]
    static void Create()
    {
        // Calculate the center of the field
        fieldCenter = new Vector3(0, 0, 0); // Assuming the field is centered at (0,0,0)

        // Create a new instance of a material using URP/Lit shader
        floodlightMaterialMagenta = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        // Ensure the material has the Emission property enabled
        floodlightMaterialMagenta.EnableKeyword("_EMISSION");

        // Set the emission color to cyan (R = 0, G = 1, B = 1)
        floodlightMaterialMagenta.SetColor("_EmissionColor", Color.cyan);

        // Set Emission strength (optional if you want to control intensity)
        floodlightMaterialMagenta.SetFloat("_EmissiveIntensity", 1.0f);


        CreateLightRigs.FourCorners();

        Debug.Log("CreateLightRigs.");
    }

    static void FourCorners()
    {
        // Define colors for each pole (cyan, magenta, yellow, and white)
        Material[] poleMaterials = { floodlightMaterialCyan, floodlightMaterialMagenta, floodlightMaterialYellow, floodlightMaterialWhite };

        // Create poles at the corners of the field
        Vector3[] polePositions = {
            new Vector3(-fieldSize.x / 2, 0, -fieldSize.z / 2),
            new Vector3(fieldSize.x / 2, 0, -fieldSize.z / 2),
            new Vector3(-fieldSize.x / 2, 0, fieldSize.z / 2),
            new Vector3(fieldSize.x / 2, 0, fieldSize.z / 2)
        };

        for (int i = 0; i < polePositions.Length; i++)
        {
            GameObject pole = CreatePole(polePositions[i]);
            // Use the corresponding material for the pole's lights
            CreateFloodlights(pole.transform, poleMaterials[i]);
        }
    }

    static GameObject CreatePole(Vector3 position)
    {
        // Create a pole using a cylinder
        GameObject pole = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        pole.transform.position = position + new Vector3(0, poleHeight / 2, 0); // Center the pole vertically
        pole.transform.localScale = new Vector3(poleRadius, poleHeight / 2, poleRadius); // Scale pole
        //pole.name = "Pole";
        pole.name = "LightObject";

        return pole;
    }
    static void CreateFloodlights(Transform pole, Material floodlightMaterial)
    {
        // Create floodlight rows at the top of each pole
        for (int row = 0; row < floodlightRows; row++)
        {
            float heightOffset = poleHeight - (row * floodlightSpacing);
            for (int i = 0; i < floodlightsPerRow; i++)
            {
                // Calculate the horizontal offset based on floodlight position in the row
                float widthOffset = (i - (floodlightsPerRow - 1) / 2.0f) * floodlightSpacing;

                GameObject floodlight = CreateFloodlight(pole, heightOffset, widthOffset, floodlightMaterial);
                floodlight.transform.parent = pole;
            }
        }
    }

    static GameObject CreateFloodlight(Transform pole, float heightOffset, float widthOffset, Material floodlightMaterial)
    {
        // Create floodlight using a cube
        GameObject floodlight = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floodlight.transform.localScale = new Vector3(floodlightSize, floodlightSize, floodlightSize);

        // Position floodlight near the top of the pole with the calculated width offset
        floodlight.transform.position = pole.position + new Vector3(widthOffset, heightOffset, 0);

        // Rotate the floodlight to face the center of the field
        floodlight.transform.LookAt(fieldCenter);

        //floodlight.name = "Floodlight";
        floodlight.name = "LightObject";

        // Apply emissive material with the given color
        if (floodlightMaterial != null)
        {
            floodlight.GetComponent<Renderer>().material = floodlightMaterial;
        }

        return floodlight;
    }

}
