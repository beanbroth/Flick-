using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBodyNodeController : MonoBehaviour
{
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Player.OnFlick += GetFlicked;
    }

    private void OnDestroy()
    {
        Player.OnFlick -= GetFlicked;
    }

    void GetFlicked(Vector2 vec)
    {
        _rb.velocity = vec;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
