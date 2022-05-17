using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    [SerializeField] private float swipeRange;
    [SerializeField] private float tapRange;
    [SerializeField] private float _forceMult;
    [SerializeField] private float _minTimeMult;
    private float _swipeTimer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Touch.activeTouches.Count > 0)
        //{
        //    Debug.Log("working");
        //}

        SwipeCheck();
    }

    public void SwipeCheck()
    {
        if (!(Touch.activeTouches.Count > 0))
            return;

        _swipeTimer += Time.deltaTime;

        if (Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            startTouchPosition = Touch.activeTouches[0].screenPosition;
            _swipeTimer = 0;
        }
        else if (Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Moved)
        {
            currentPosition = Touch.activeTouches[0].screenPosition;
            Vector2 distance = currentPosition - startTouchPosition;

            if (!stopTouch)
            {
                if (distance.magnitude > swipeRange)
                {
                    Debug.Log(_swipeTimer);
                    _rb.AddForce(distance.normalized * _forceMult * (Mathf.Max(_minTimeMult, 1 - _swipeTimer)));

                    stopTouch = true;
                }
            }
        }
        else if (Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Touch.activeTouches[0].screenPosition;

            Vector2 distance = endTouchPosition - startTouchPosition;


            if (distance.magnitude < tapRange)
            {
                Debug.Log("tap");
            }
            else
            {
                _rb.AddForce(distance.normalized * _forceMult * (Mathf.Max(_minTimeMult, 1 - _swipeTimer)));
            }
        }
    }
}
