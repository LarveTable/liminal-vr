using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CapsulePlane : MonoBehaviour
{
    [Range(4, 64)] public int segments = 32;
    public float length = 2f;
    public float radius = 1f;

    void Start() => Generate();
    void OnValidate() => Generate();

    void Generate()
    {
        var mf = GetComponent<MeshFilter>();
        if (mf == null) return;
        mf.sharedMesh = CreateCapsulePlaneMesh();
    }

    Mesh CreateCapsulePlaneMesh()
    {
        Mesh mesh = new Mesh();
        var verts = new List<Vector3>();
        var tris = new List<int>();

        // --- Rectangle central (orienté dans XZ, allongé sur X) ---
        verts.Add(new Vector3(-length / 2, 0, -radius)); // 0
        verts.Add(new Vector3(length / 2, 0, -radius));  // 1
        verts.Add(new Vector3(length / 2, 0, radius));   // 2
        verts.Add(new Vector3(-length / 2, 0, radius));  // 3

        // Triangles vers Y positif
        tris.AddRange(new int[] { 0, 2, 1, 0, 3, 2 });

        // --- Demi-cercle gauche (convexe vers -X) ---
        int leftStart = verts.Count;
        verts.Add(new Vector3(-length / 2, 0, 0)); // centre gauche
        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.PI * i / segments;
            float x = -length / 2 - Mathf.Sin(angle) * radius;
            float z = -Mathf.Cos(angle) * radius; // inversion pour corriger la rotation
            verts.Add(new Vector3(x, 0, z));
        }

        for (int i = 1; i <= segments; i++)
        {
            tris.Add(leftStart);
            tris.Add(leftStart + i);
            tris.Add(leftStart + i + 1);
        }

        // --- Demi-cercle droite (convexe vers +X) ---
        int rightStart = verts.Count;
        verts.Add(new Vector3(length / 2, 0, 0)); // centre droite
        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.PI * i / segments;
            float x = length / 2 + Mathf.Sin(angle) * radius;
            float z = Mathf.Cos(angle) * radius;
            verts.Add(new Vector3(x, 0, z));
        }

        for (int i = 1; i <= segments; i++)
        {
            tris.Add(rightStart);
            tris.Add(rightStart + i);
            tris.Add(rightStart + i + 1);
        }

        mesh.SetVertices(verts);
        mesh.SetTriangles(tris, 0);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}