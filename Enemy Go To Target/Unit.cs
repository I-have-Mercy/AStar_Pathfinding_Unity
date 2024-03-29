﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    float speed = 0.5f;
    Vector3[] path;
    int targetIndex;

    void Start()
    {
        PathRequestManager.instance.RequestPath(transform.position, target.position, OnPathFound);
    }
    private void FixedUpdate()
    {
        PathRequestManager.instance.RequestPath(transform.position, target.position, OnPathFound);
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if(pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while(true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed*0.05f);
            yield return null;
        }
    }
}
