using UnityEngine;
using System.Collections.Generic;

public class InstancedSphereSpawner : MonoBehaviour
{
    [Header("Sphere Settings")]
    public int numberOfSpheres = 1000;
    public float sphereRadius = 0.1f;

    [Header("Spawn Area Settings")]
    public float xmin = -5f;
    public float xmax = 5f;
    public float ymin = 0f;
    public float ymax = 10f;
    public float zmin = -5f;
    public float zmax = 5f;

    [Header("Rendering Settings")]
    public Material sphereMaterial;

    private Mesh sphereMesh;
    private Matrix4x4[] matrices;

    void Start()
    {
        InitializeSpheres();
    }

    void InitializeSpheres()
    {
        // Create sphere mesh
        GameObject tempSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereMesh = tempSphere.GetComponent<MeshFilter>().mesh;
        Destroy(tempSphere);

        // Scale the mesh to the desired radius
        Vector3 sphereScale = Vector3.one * sphereRadius * 2f; // Diameter = radius * 2

        // Initialize transformation matrices
        matrices = new Matrix4x4[numberOfSpheres];

        for (int i = 0; i < numberOfSpheres; i++)
        {
            // Generate a random position within the specified ranges
            Vector3 randomPosition = new Vector3(
                Random.Range(xmin, xmax),
                Random.Range(ymin, ymax),
                Random.Range(zmin, zmax)
            );

            // Create transformation matrix for each instance
            Matrix4x4 matrix = Matrix4x4.TRS(
                randomPosition,            // Position
                Quaternion.identity,       // Rotation
                sphereScale                // Scale
            );

            matrices[i] = matrix;
        }
    }

    void Update()
    {
        // Draw all instances using GPU instancing
        Graphics.DrawMeshInstanced(sphereMesh, 0, sphereMaterial, matrices);
    }

    //void Update()
    //{
    //    for (int i = 0; i < matrices.Length; i++)
    //    {
    //        Graphics.DrawMesh(
    //            sphereMesh,
    //            matrices[i],
    //            sphereMaterial,
    //            0,
    //            null,
    //            0,
    //            null,
    //            false, // Set this to false to disable instancing
    //            false
    //        );
    //    }
    //}
}
