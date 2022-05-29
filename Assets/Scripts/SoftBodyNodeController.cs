using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBodyNodeController : MonoBehaviour
{
    private Rigidbody2D _rb;

    public static bool isGrounded = false;
    void Start()
    {
        GameManager.OnGameStateChanged += GameStateChanged;
        _rb = GetComponent<Rigidbody2D>();
        Player.OnFlick += GetFlicked;
    }

    //void in

    private void OnDestroy()
    {
        Player.OnFlick -= GetFlicked;
        GameManager.OnGameStateChanged -= GameStateChanged;
    }

    void GameStateChanged(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.Dead)
        {
            Debug.Log("shattering");
            Joint2D[] joints = GetComponents<Joint2D>();

            for (int i = 0; i < joints.Length; i++)
            {
                Destroy(joints[i]);
            }


        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded)
            return;

        if (collision.collider.tag == "Player")
            return;

        Debug.Log("Splat");
        isGrounded = true;
        JSAM.AudioManager.PlaySound(JSAM.Sounds.Squish);

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
