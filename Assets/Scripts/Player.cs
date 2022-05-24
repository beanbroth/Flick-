using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private SoftBodyGenerator _softBody;
    [SerializeField] private Rigidbody2D _centerRb;


    public static event Action<Vector2> OnFlick;

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool _isSwiping = false;
    [Header("Edit Flick Force")]
    [SerializeField] private float _forceMult;
    [Header("Tweak Deatails of Flick Detection (No reason to)")]
    [SerializeField] private float swipeRange;
    [SerializeField] private float tapRange;

    [SerializeField] private float _minTimeMult;
    private float _swipeTimer;


    private bool _isGrounded;
    [SerializeField] private float _groundedSlowDownPercent;
    [SerializeField] private float _distMult;

    private void Start()
    {
        GameManager.OnGameStateChanged += GameStateChanged;
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
        //ConstrainPos();
    }

    void ConstrainPos()
    {
        float minX = _softBody.CenterNode.transform.position.x;
        float minY = _softBody.CenterNode.transform.position.y;

        float maxX = _softBody.CenterNode.transform.position.x;
        float maxY = _softBody.CenterNode.transform.position.y;

        for (int i = 0; i < _softBody.NodeCount; i++)
        {
            if (_softBody.Nodes[i].transform.position.y > maxY)
            {
                maxY = _softBody.Nodes[i].transform.position.y;
            }

            if (_softBody.Nodes[i].transform.position.y < minY)
            {
                minY = _softBody.Nodes[i].transform.position.y;
            }

            if (_softBody.Nodes[i].transform.position.x > maxX)
            {
                maxX = _softBody.Nodes[i].transform.position.x;
            }

            if (_softBody.Nodes[i].transform.position.x < minX)
            {
                minX = _softBody.Nodes[i].transform.position.x;
            }
        }

        minX += _softBody.NodeRadius / 2;
        minY += _softBody.NodeRadius / 2;

        maxX -= _softBody.NodeRadius / 2;
        maxY -= _softBody.NodeRadius / 2;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
    }

    public void SetVelToZero()
    {
        _centerRb.velocity = Vector3.zero;
        for (int i = 0; i < _softBody.Nodes.Length; i++)
        {
            _softBody.Nodes[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

    }

    void GameStateChanged(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.PlayingLevel)
        {
            SetVelToZero();
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
                ApplyFlickForce(distance);
            }
        }

        //check if swipe completed due to distance
        currentPosition = Touch.activeTouches[0].screenPosition;
        distance = currentPosition - startTouchPosition;

        if (distance.magnitude > swipeRange)
        {
            //Debug.Log(distance);
            ApplyFlickForce(distance);

            _isSwiping = false;
        }
    }
    private void ApplyFlickForce(Vector2 distance)
    {
        Vector2 flickVec = distance / _distMult * _forceMult * (Mathf.Max(_minTimeMult, 1 - _swipeTimer));
        _centerRb.velocity = flickVec;
        OnFlick?.Invoke(flickVec);
    }
}
