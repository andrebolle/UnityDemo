using UnityEngine;

public class StadiumLightingRig : MonoBehaviour
{
    // Parameters for the structure
    public int numberOfPoles = 4;
    public float poleHeight = 20f;
    public float poleRadius = 0.5f;
    public Vector3 fieldSize = new Vector3(50f, 0f, 30f); // Size of the field (width, height, depth)
    public int floodlightRows = 2;
    public int floodlightsPerRow = 4;
    public float floodlightSpacing = 1.5f;
    public float floodlightSize = 0.5f;

    // Emissive Materials for the floodlights
    public Material floodlightMaterialCyan;
    public Material floodlightMaterialMagenta;
    public Material floodlightMaterialYellow;
    public Material floodlightMaterialWhite;

    // Field center position
    private Vector3 fieldCenter;

    void Start()
    {
        // Calculate the center of the field
        fieldCenter = new Vector3(0, 0, 0); // Assuming the field is centered at (0,0,0)
        CreateLightingRig();
    }

    void CreateLightingRig()
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

    GameObject CreatePole(Vector3 position)
    {
        // Create a pole using a cylinder
        GameObject pole = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        pole.transform.position = position + new Vector3(0, poleHeight / 2, 0); // Center the pole vertically
        pole.transform.localScale = new Vector3(poleRadius, poleHeight / 2, poleRadius); // Scale pole
        pole.name = "Pole";

        return pole;
    }

    void CreateFloodlights(Transform pole, Material floodlightMaterial)
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

    GameObject CreateFloodlight(Transform pole, float heightOffset, float widthOffset, Material floodlightMaterial)
    {
        // Create floodlight using a cube
        GameObject floodlight = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floodlight.transform.localScale = new Vector3(floodlightSize, floodlightSize, floodlightSize);

        // Position floodlight near the top of the pole with the calculated width offset
        floodlight.transform.position = pole.position + new Vector3(widthOffset, heightOffset, 0);

        // Rotate the floodlight to face the center of the field
        floodlight.transform.LookAt(fieldCenter);

        floodlight.name = "Floodlight";

        // Apply emissive material with the given color
        if (floodlightMaterial != null)
        {
            floodlight.GetComponent<Renderer>().material = floodlightMaterial;
        }

        return floodlight;
    }
}
