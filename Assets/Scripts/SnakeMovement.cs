using System;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private Transform segmentPrefab;
    private Vector2 _direction;
    private Vector2 _nextDirection;
    private float _fixedTimestep = 0.1f;
    private List<Transform> _segments;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _segments = new List<Transform>();
    }

    void Start()
    {
        ResetState();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (UserInput.instance.UpJustPressed)
        {
            _nextDirection = Vector2.up;
        }
        else if (UserInput.instance.DownJustPressed)
        {
            _nextDirection = Vector2.down;
        }
        else if (UserInput.instance.LeftJustPressed)
        {
            _nextDirection = Vector2.left;
        }
        else if (UserInput.instance.RightJustPressed)
        {
            _nextDirection = Vector2.right;
        }
        _direction = isMovingBack(_nextDirection) ? _direction : _nextDirection;
    }

    private void MoveForward()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x + _direction.x),
            Mathf.Round(this.transform.position.y + _direction.y),
            0.0f
        );
    }

    private bool isMovingBack(Vector2 nextDirection)
    {
        bool isOppositeDirection = (nextDirection + _direction) == Vector2.zero;
        Vector3 nextDirectionV3 = nextDirection;
        bool isFracture = _segments[0].position + nextDirectionV3 == _segments[1].position;
        if (isFracture)
        {
            Debug.Log(isFracture);
        }
        return isOppositeDirection || isFracture;
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        Vector3 _newLastPos = _direction;
        segment.position = _segments[_segments.Count - 1].position - _newLastPos;
        _segments.Add(segment);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
        }
        if (collision.tag == "Obstacle")
        {
            ResetState();
        }
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        this.transform.position = Vector3.zero;
        _direction = Vector2.right;
        _nextDirection = _direction;
        _fixedTimestep = 0.1f;
        Time.fixedDeltaTime = _fixedTimestep;
        _segments.Clear();
        _segments.Add(this.transform);
        Grow();
    }

    public List<Vector3> GetSegments()
    {
        var positions = new List<Vector3>();
        foreach (Transform t in _segments)
        {
            positions.Add(t.position);
        }
        return positions;
    }

}
