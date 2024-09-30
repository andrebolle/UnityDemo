using UnityEngine;

public class CreateGameBoard : MonoBehaviour
{
    // Public variables to set in the Unity Inspector
    public int gridSize = 5; // Size of the grid (5x5)
    public float tileSize = 1.0f; // Size of each tile
    public GameObject tilePrefab; // Prefab for the tiles

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Offset to center the grid in the scene
        float offset = (gridSize - 1) * tileSize / 2;

        // Create a parent object to organize tiles in the hierarchy
        GameObject gridParent = new GameObject("GameBoard");

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // Calculate the position of each tile
                Vector3 tilePosition = new Vector3(x * tileSize - offset, 0, y * tileSize - offset);

                // Instantiate the tile prefab at the calculated position
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);

                // Set the tile's parent to keep the hierarchy organized
                tile.transform.parent = gridParent.transform;

                // (Optional) Name the tile for easier identification
                tile.name = $"Tile_{x}_{y}";

                // (Optional) Add coordinates to the tile's script (if any)
                // Tile tileScript = tile.GetComponent<Tile>();
                // if (tileScript != null)
                // {
                //     tileScript.SetCoordinates(x, y);
                // }
            }
        }
    }
}
