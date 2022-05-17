using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using System;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool _isSwiping = false;

    [SerializeField] private float swipeRange;
    [SerializeField] private float tapRange;
    [SerializeField] private float _forceMult;
    [SerializeField] private float _minTimeMult;
    private float _swipeTimer;


    private bool _isGrounded;
    [SerializeField] private float _groundedSlowDownPercent;
    [SerializeField] private float _distMult;

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

    private void FixedUpdate()
    {
        HorizontalFrictionWhenGrounded();

    }

    private void HorizontalFrictionWhenGrounded()
    {
        if (_isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x * _groundedSlowDownPercent, _rb.velocity.y); 
        }
    }

    public void SwipeCheck()
    {
        Vector2 distance;

        if (!(Touch.activeTouches.Count > 0))
            return;

        if (Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            startTouchPosition = Touch.activeTouches[0].screenPosition;
            _swipeTimer = 0;
            _isSwiping = true;
        }

        if (!_isSwiping)
            return;
      
        _swipeTimer += Time.deltaTime;

        //check if swipe ended early from finger being lifted
        if (Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            _isSwiping = false;
            endTouchPosition = Touch.activeTouches[0].screenPosition;
            distance = endTouchPosition - startTouchPosition;

            if (distance.magnitude < tapRange)
            {
                Debug.Log("tap");
            }
            else
            {
                _rb.velocity = (distance / _distMult * _forceMult * (Mathf.Max(_minTimeMult, 1 - _swipeTimer)));
            }
        }

        //check if swipe completed due to distance
        currentPosition = Touch.activeTouches[0].screenPosition;
        distance = currentPosition - startTouchPosition;

        if (distance.magnitude > swipeRange)
        {
            Debug.Log(distance);
            _rb.velocity = (distance / _distMult * _forceMult * (Mathf.Max(_minTimeMult, 1 - _swipeTimer)));

            _isSwiping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isGrounded = true;


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = false;

    }
}
