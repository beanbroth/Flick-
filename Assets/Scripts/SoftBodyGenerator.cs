using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoftBodyGenerator : MonoBehaviour
{
    [Header("Softbody Generator Settings")]
    [Tooltip("Radius of squishy ball")]
    [SerializeField] private float _radius;
    [SerializeField] private int _amountToSpawn;
    [SerializeField] private GameObject _nodePrefab;
    [SerializeField] private GameObject _center;
    [SerializeField] private float _nodeScale;

    [SerializeField] private float _springStiffness;

    [SerializeField] private float _springDampening;

    [SerializeField] List<GameObject> _nodes = new List<GameObject>();
    [SerializeField] private float _edgeAngleMin;
    [SerializeField] private float _edgeAngleMax;

    public float Radius { get => _radius; }
    public GameObject CenterNode { get => _center; }
    public int NodeCount { get => _amountToSpawn; }

    public float NodeRadius { get => _nodeScale; }
    public GameObject[] Nodes { get => _nodes.ToArray(); }

    void Start()
    {
       // DontDestroyOnLoad(gameObject);
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
            ob.transform.localPosition = new Vector3(x, y, 0);
        }

        //_center.GetComponent<CircleCollider2D>().radius = .5f * _nodeScale;

        for (int i = 0; i < _amountToSpawn; i++)
        {
            //connect right
            //SpringJoint2D tempJoint = _nodes[i].AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;
            //if (i == _amountToSpawn - 1)
            //{
            //    tempJoint.connectedBody = _nodes[0].GetComponent<Rigidbody2D>();
            //}
            //else
            //{
            //    tempJoint.connectedBody = _nodes[i + 1].GetComponent<Rigidbody2D>();
            //}
            //tempJoint.frequency = _springStiffness;
            //tempJoint.dampingRatio = _springDampening;

            ////connect left
            //tempJoint = _nodes[i].AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;
            //if (i == 0)
            //{
            //    tempJoint.connectedBody = _nodes[_amountToSpawn - 1].GetComponent<Rigidbody2D>();
            //}
            //else
            //{
            //    tempJoint.connectedBody = _nodes[i - 1].GetComponent<Rigidbody2D>();
            //}
            //tempJoint.frequency = _springStiffness;
            //tempJoint.dampingRatio = _springDampening;

            //connect center
            SpringJoint2D tempJoint = _nodes[i].AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;
            tempJoint.connectedBody = _center.GetComponent<Rigidbody2D>();
            tempJoint.frequency = _springStiffness;
            tempJoint.dampingRatio = _springDampening;



            // DO HTIS 
            //DistanceJoint2D distJoint = _nodes[i].AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
            //distJoint.connectedBody = _center.GetComponent<Rigidbody2D>();




            //connect hinges
            HingeJoint2D tempHinge = _nodes[i].AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;

            if (i == _amountToSpawn - 1)
            {
                tempHinge.connectedBody = _nodes[0].GetComponent<Rigidbody2D>();
            }
            else
            {
                tempHinge.connectedBody = _nodes[i + 1].GetComponent<Rigidbody2D>();
            }

            //tempHinge.connectedBody = _center.GetComponent<Rigidbody2D>();

            JointAngleLimits2D limits = tempHinge.limits;
            limits.min = _edgeAngleMin;
            limits.max = _edgeAngleMax;
            tempHinge.limits = limits;
            tempHinge.useLimits = true;

        }
    }
    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }
}
