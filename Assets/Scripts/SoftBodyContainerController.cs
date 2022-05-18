using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoftBodyContainerController : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private int _amountToSpawn;
    [SerializeField] private GameObject _nodePrefab;
    [SerializeField] private GameObject _center;
    [SerializeField] private float _nodeScale;

    [SerializeField] private float _springStiffness;
    [SerializeField] private float _springDampening;

    [SerializeField] List<GameObject> _nodes = new List<GameObject>();

    void Start()
    {

    }

    void OnValidate() { UnityEditor.EditorApplication.delayCall += _OnValidate; }
    private void _OnValidate()
    {
        if (this == null) return;
        foreach (GameObject node in _nodes)
        {
            StartCoroutine(Destroy(node));
        }

        _nodes.Clear();

        for (int i = 0; i < _amountToSpawn; i++)
        {
            float theta = i * 2 * Mathf.PI / _amountToSpawn;
            float x = Mathf.Sin(theta) * _radius;
            float y = Mathf.Cos(theta) * _radius;

            GameObject ob = Instantiate(_nodePrefab);
            _nodes.Add(ob);
            ob.transform.parent = transform;
            ob.transform.localScale = Vector3.one * _nodeScale;
            ob.transform.position = new Vector3(x, y, 0);
        }

        _center.transform.localScale = Vector3.one * _nodeScale;

        for (int i = 0; i < _amountToSpawn; i++)
        {

            //connect right
            SpringJoint2D tempJoint = _nodes[i].AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;
            if (i == _amountToSpawn - 1)
            {
                tempJoint.connectedBody = _nodes[0].GetComponent<Rigidbody2D>();
            }
            else
            {
                tempJoint.connectedBody = _nodes[i + 1].GetComponent<Rigidbody2D>();
            }
            tempJoint.frequency = _springStiffness;
            tempJoint.dampingRatio = _springDampening;

            //connect left
            tempJoint = _nodes[i].AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;
            if (i == 0)
            {
                tempJoint.connectedBody = _nodes[_amountToSpawn - 1].GetComponent<Rigidbody2D>();
            }
            else
            {
                tempJoint.connectedBody = _nodes[i - 1].GetComponent<Rigidbody2D>();
            }
            tempJoint.frequency = _springStiffness;
            tempJoint.dampingRatio = _springDampening;

            //connect center
            tempJoint = _nodes[i].AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;
            //tempJoint.connectedBody = _nodes[(i + _amountToSpawn/2)%_amountToSpawn].GetComponent<Rigidbody2D>();
            tempJoint.connectedBody = _center.GetComponent<Rigidbody2D>();
            tempJoint.frequency = _springStiffness;
            tempJoint.dampingRatio = _springDampening;
        }
    }
    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }
}
