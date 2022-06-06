using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailController : MonoBehaviour
{
    [SerializeField] private TrailRenderer _tr;
    [SerializeField] private SoftBodyGenerator _softBody;
    void Start()
    {
        GameManager.OnGameStateChanged += GameStateChanged;
        _tr.startWidth = _softBody.Radius + _softBody.NodeRadius * 2;
    }

    void GameStateChanged(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.PlayingLevel)
        {
            _tr.Clear();
        }
    }

    void Update()
    {

    }
}
