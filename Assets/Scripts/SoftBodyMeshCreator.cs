using System.Collections.Generic;
using UnityEngine;

public class SoftBodyMeshCreator : MonoBehaviour
{
    [SerializeField] private float width = 1;
    [SerializeField] private float height = 1;
    [SerializeField] private Material _mat;
    [SerializeField] private MeshRenderer _meshRen;
    [SerializeField] private MeshFilter _meshFilt;
    private Mesh _mesh;

    [SerializeField] private SoftBodyContainerController _softBody;

    void OnValidate() { UnityEditor.EditorApplication.delayCall += _OnValidate; }
    private void _OnValidate()
    {
        if (this == null) return;
        GenerateInitalMesh();
    }

    private void Start()
    {
        GenerateInitalMesh();
    }

    private void Update()
    {
        UpdateMeshVerticies();
    }

    private void UpdateMeshVerticies()
    {
        List<Vector3> vertices = new List<Vector3>();
        for (int i = 0; i < _softBody.NodeCount; i++)
        {
            vertices.Add(_softBody.Nodes[i].transform.localPosition + ((_softBody.Nodes[i].transform.localPosition) - _softBody.CenterNode.transform.localPosition).normalized * _softBody.NodeRadius / 1.8f);
        }
        vertices.Add(_softBody.CenterNode.transform.localPosition);
        _mesh.vertices = vertices.ToArray();
    }

    private void GenerateInitalMesh()
    {
        _meshRen.sharedMaterial = _mat;
        _mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        for (int i = 0; i < _softBody.NodeCount; i++)
        {
            vertices.Add(_softBody.Nodes[i].transform.localPosition + ((_softBody.Nodes[i].transform.localPosition) - _softBody.CenterNode.transform.localPosition).normalized * _softBody.NodeRadius/1.8f);

        }
        vertices.Add(_softBody.CenterNode.transform.localPosition);

        //swuare shit
        //vertices.Add(new Vector3(0, 0, 0));
        //vertices.Add(new Vector3(width, 0, 0));
        //vertices.Add(new Vector3(0, height, 0));
        //vertices.Add(new Vector3(width, height, 0));


        List<int> tris = new List<int>();

        //TEMP MINUS ONE BECASUE I DON"T WANNA GUARD THE LAST ONE
        for (int i = 0; i < _softBody.NodeCount; i++)
        {
            tris.Add(i);
            tris.Add(i + 1);
            tris.Add(vertices.Count - 1);
        }
        tris.Add(_softBody.NodeCount - 1);
        tris.Add(0);
        tris.Add(vertices.Count - 1);
        //swuare shit
        //int[] tempTris = new int[6]
        //{
        //    // lower left triangle
        //    0, 2, 1,
        //    // upper right triangle
        //    2, 3, 1
        //};
        //tris.AddRange(tempTris);


        List<Vector3> normals = new List<Vector3>();
        for (int i = 0; i < vertices.Count; i++)
        {
            normals.Add(-Vector3.forward);
        }

        //swuare shit
        //Vector3[] tempNormals = new Vector3[4]
        //{
        //    -Vector3.forward,
        //    -Vector3.forward,
        //    -Vector3.forward,
        //    -Vector3.forward
        //};

        //normals.AddRange(tempNormals);

        float minX = _softBody.CenterNode.transform.localPosition.x;
        float minY = _softBody.CenterNode.transform.localPosition.y;

        float maxX = _softBody.CenterNode.transform.localPosition.x;
        float maxY = _softBody.CenterNode.transform.localPosition.y;

        List<Vector2> uv = new List<Vector2>();

        for (int i = 0; i < _softBody.NodeCount; i++)
        {
            if (_softBody.Nodes[i].transform.localPosition.y > maxY)
            {
                maxY = _softBody.Nodes[i].transform.localPosition.y;
            }

            if (_softBody.Nodes[i].transform.localPosition.y < minY)
            {
                minY = _softBody.Nodes[i].transform.localPosition.y;
            }

            if (_softBody.Nodes[i].transform.localPosition.x > maxX)
            {
                maxX = _softBody.Nodes[i].transform.localPosition.x;
            }

            if (_softBody.Nodes[i].transform.localPosition.x < minX)
            {
                minX = _softBody.Nodes[i].transform.localPosition.x;
            }
        }

        for (int i = 0; i < vertices.Count; i++)
        {
            uv.Add(new Vector2((vertices[i].x / (maxX - minX)) + .5f, (vertices[i].y / (maxY - minY)) + .5f));
        }




        //Vector2[] tempUV = new Vector2[4]
        //{
        //    new Vector2(0, 0),
        //    new Vector2(1, 0),
        //    new Vector2(0, 1),
        //    new Vector2(1, 1)
        //};

        //uv.AddRange(tempUV);


        _mesh.Clear();
        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = tris.ToArray();
        _mesh.normals = normals.ToArray();
        _mesh.uv = uv.ToArray();

        _meshFilt.mesh = _mesh;
    }
}
