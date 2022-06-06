using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyObjectController : MonoBehaviour
{
    bool _wasActivated;
    private void Start()
    {
        _wasActivated = false;
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
       // Debug.Log("beeb");
        if (col.collider.tag == "Player" && !_wasActivated)
        {
            Debug.Log("dead");
            _wasActivated = true;

            GameManager.Instance.SetNextGameState(GameManager.GameState.Dead);
        }

    }
}
