using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneTransitionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += GameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GameStateChanged(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.LoadingLevel)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 13, transform.localPosition.z);
            transform.DOLocalMove(new Vector3(transform.localPosition.x, -13, transform.localPosition.z), 1f).SetUpdate(true);
        }

        if(newState == GameManager.GameState.PlayingLevel)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -54, transform.localPosition.z);

            transform.DOLocalMove(new Vector3(transform.localPosition.x, -80, transform.localPosition.z), 0.5f).SetUpdate(true);
        }
    }
}
