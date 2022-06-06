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
        //transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), 0.001f).SetUpdate(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GameStateChanged(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.PreLoadingLevel)
        {
            StartCoroutine(DelayedSceneTransition());
        }

        if (newState == GameManager.GameState.PlayingLevel)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -46, transform.localPosition.z);

            transform.DOLocalMove(new Vector3(transform.localPosition.x, -80, transform.localPosition.z), 1f).SetUpdate(true);
        }
    }

    private IEnumerator DelayedSceneTransition()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        transform.localPosition = new Vector3(transform.localPosition.x, 13, transform.localPosition.z);
        transform.DOLocalMove(new Vector3(transform.localPosition.x, -13, transform.localPosition.z), 1f).SetUpdate(true);
    }
}
