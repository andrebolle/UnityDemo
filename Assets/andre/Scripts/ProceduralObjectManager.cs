using UnityEngine;
using System.IO;
using System.Collections.Generic;
using static UnityEditor.FilePathAttribute;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.CoreUtils;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class ProceduralObjectManager : MonoBehaviour
{
/*
    The ProceduralObjectManager class in Unity is responsible for procedurally generating GameObjects, 
    saving their data for persistence, and reloading them in subsequent game sessions. Here’s a summary of its functionality:

    Procedural Object Creation:

    It generates a specified number of GameObjects (e.g., cubes) with random positions and scales.
    This happens only on the first run, when no saved data exists.
    Data Persistence:

    The GameObjects' data (position, scale, etc.) is stored in a SaveData structure, which is serialized into a JSON file.
    This JSON file is saved to a persistent location so the object states can be retrieved in future sessions.
    Data Loading:

    On subsequent game starts, the class checks if the saved data file exists.
    If the file is found, it reads the data, deserializes it, and recreates the previously saved GameObjects with the same properties.
    In short, this class ensures that procedural GameObjects are created only once and persist across different game sessions by saving and loading their state from a file.
*/

    public int numberOfObjects = 10;  // Number of procedural objects to create
    private string filePath;          // Path to save and load the persistent data

    // Class to represent the data of each object (position, scale, prefab type)
    [System.Serializable]
    public class ObjectData
    {
        public Vector3 position;      // Position of the object
        public Vector3 scale;         // Scale of the object
        public string prefabName;     // Name of the prefab (for more complex objects, not used here)
    }

    // Class to represent the entire save data, which is a collection of ObjectData
    [System.Serializable]
    public class SaveData
    {
        public List<ObjectData> objects = new List<ObjectData>(); // List of all object data
    }

    private SaveData saveData = new SaveData(); // Create an instance of SaveData

    // Runs at the start of the game
    void Start()
    {
        Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
        // Define the file path to store data (this works across different platforms)
        filePath = Application.persistentDataPath + "/gameData.json";

        // If save data exists, load it; otherwise, create new procedural objects
        if (File.Exists(filePath))
        {
            LoadData(); // Load the saved procedural objects
        }
        else
        {
            // If no save data, create the procedural objects for the first time
            CreateProceduralObjects();
            MySaveData(); // Save the data after creating the objects
        }
    }

    // Method to create the procedural objects for the first time
    void CreateProceduralObjects()
    {
        // Example: create a number of objects procedurally
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Create a basic primitive object (a cube in this case)
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            // Randomize the position and scale of the cube
            obj.transform.position = new Vector3(Random.Range(-10f, 10f), 1, Random.Range(-10f, 10f));
            obj.transform.localScale = new Vector3(Random.Range(0.5f, 2f), Random.Range(0.5f, 2f), Random.Range(0.5f, 2f));

            // Create an ObjectData instance to store the object's data (position, scale, prefab type)
            ObjectData objData = new ObjectData
            {
                position = obj.transform.position,
                scale = obj.transform.localScale,
                prefabName = "Cube" // In this example, we are using a cube
            };

            // Add the object's data to the list of objects to save
            saveData.objects.Add(objData);
        }
    }

    // Method to save data to a file
    void MySaveData()
    {
        // Convert the object data to JSON format
        string json = JsonUtility.ToJson(saveData, true); // "true" for pretty printing (optional)

        // Write the JSON data to the specified file path
        File.WriteAllText(filePath, json);
    }

    // Method to load data from a file
    void LoadData()
    {
            //{
            //    "objects": [
            //        {
            //        "position": {
            //            "x": -7.421390533447266,
            //        "y": 1.0,
            //        "z": 8.884057998657227
            //        },
            //    "scale": {
            //            "x": 1.839268684387207,
            //        "y": 1.686809778213501,
            //        "z": 1.0630708932876588
            //    },
            //    "prefabName": "Cube"
            //        },
            //{
            //        "position": {

                    // Read the JSON data from the file
                    string json = File.ReadAllText(filePath);

        // Convert the JSON data back into a SaveData object
        saveData = JsonUtility.FromJson<SaveData>(json);

        // Loop through each saved object and recreate them in the game
        foreach (ObjectData objData in saveData.objects)
        {
            // Create a new cube object (or a prefab if needed)
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            // Set the position and scale from the saved data
            obj.transform.position = objData.position;
            obj.transform.localScale = objData.scale;
        }
    }
}
