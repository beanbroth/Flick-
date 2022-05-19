using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailController : MonoBehaviour
{
    [SerializeField] private TrailRenderer _tr;
    [SerializeField] private SoftBodyGenerator _softBody;
    void Start()
    {
        _tr.startWidth = _softBody.Radius + _softBody.NodeRadius * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
