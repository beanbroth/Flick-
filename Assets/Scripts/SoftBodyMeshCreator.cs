using System.Collections.Generic;
using UnityEngine;

public class SoftBodyMeshCreator : MonoBehaviour
{
    [SerializeField] private float width = 1;
    [SerializeField] private float height = 1;
    [SerializeField] private Material _mat;
    [SerializeField] private MeshRenderer _meshRen;
    [SerializeField] private MeshFilter _meshFilt;

    [SerializeField] private SoftBodyContainerController _softBody;

    void OnValidate() { UnityEditor.EditorApplication.delayCall += _OnValidate; }
    private void _OnValidate()
    {
        if (this == null) return;

        _meshRen.sharedMaterial = _mat;
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        for (int i = 0; i < _softBody.NodeCount; i++)
        {
            vertices.Add(_softBody.Nodes[i].transform.position);
        }
        vertices.Add(_softBody.CenterNode.transform.position);

        //swuare shit
        //vertices.Add(new Vector3(0, 0, 0));
        //vertices.Add(new Vector3(width, 0, 0));
        //vertices.Add(new Vector3(0, height, 0));
        //vertices.Add(new Vector3(width, height, 0));


        List<int> tris = new List<int>();

        //TEMP MINUS ONE BECASUE I DON"T WANNA GUARD THE LAST ONE
        for (int i = 0; i < _softBody.NodeCount - 1; i++)
        {
            tris.Add(i);
            tris.Add(i + 1);
            tris.Add(vertices.Count - 1);
        }

        //swuare shit
        //int[] tempTris = new int[6]
        //{
        //    // lower left triangle
        //    0, 2, 1,
        //    // upper right triangle
        //    2, 3, 1
        //};
        //tris.AddRange(tempTris);


        Vector3[] tempNormals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };

        List<Vector3> normals = new List<Vector3>();
        normals.AddRange(tempNormals);

        Vector2[] tempUV = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        List<Vector2> uv = new List<Vector2>();
        uv.AddRange(tempUV);


        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uv.ToArray();

        _meshFilt.mesh = mesh;
    }
}
