using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoalController : MonoBehaviour
{
    bool _wasActivated;

    private void Start()
    {
        _wasActivated = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !_wasActivated)
        {
            Debug.Log("reached goal");
            _wasActivated = true;

            GameManager.Instance.SetNextGameState(GameManager.GameState.PreLoadingLevel);
        }

    }



}
