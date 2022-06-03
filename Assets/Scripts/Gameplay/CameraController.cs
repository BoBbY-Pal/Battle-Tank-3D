using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class CameraController : MonoSingletonGeneric<CameraController>
{
    public float dampTime = 0.2f;
    public float screenEdgeBuffer = 4f;
    public float minSize = 6.5f;
    /*[HideInInspector]*/ public Transform[] targets;

    private Camera _camera;
    private float zoomSpeed;
    private Vector3 moveVelocity;
    private Vector3 desiredPosition;
    
    protected override void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        Move();
        Zoom();
    }
    
    private void Move()
    {
        FindAvgPosition();
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);
    }

    private void FindAvgPosition()  // Find the average position b/w two targets. 
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            if(!targets[i].gameObject.activeSelf) 
                continue;
            averagePos += targets[i].position;
            numTargets++;
        }

        if (numTargets > 0)
            averagePos /= numTargets;

        averagePos.y = transform.position.y;
        desiredPosition = averagePos;
    }

    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, requiredSize, ref zoomSpeed, dampTime);
    }

    private float FindRequiredSize()
    {
        // When we increases the camera size it will zoom in & when we decrease it will zoom out.
        // and Distance from center to the right edge is size * aspect, i.e Distance in x axis = size * aspect.
        // Distance in y axis is equals to the size, i.e size = Distance in y axis
        // we'll get the size from x axis & y axis. we'll pick the largest one.. Then tanks will be on the screen.  
        // We just need to reverse the equation. i.e distance/aspect = size, and this will give us the size..
        
        Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPosition);
        float size = 0f;
        for (int i = 0; i < targets.Length; i++)
        {
            if(!targets[i].gameObject.activeSelf)
                continue;
            Vector3 targetLocalPos = transform.InverseTransformPoint(targets[i].position);
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / _camera.aspect);
        }

        size += screenEdgeBuffer;
        size = Mathf.Max(size, minSize);
        return size;
    }

    public void SetCameraPosAndSize()
    {
        FindAvgPosition();
        transform.position = desiredPosition;
        _camera.orthographicSize = FindRequiredSize();
    }
}
