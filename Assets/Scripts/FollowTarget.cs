using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : GameMonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 basePos;

    protected override void Awake()
    {
        this.basePos = this.transform.position;
    }

    protected override void Start()
    {
        base.Start();
        this.LoadTarget();
    }

    private void LoadTarget()
    {
        this.target = GameManager.Instance.MainCamera.transform;
    }

    private void Update()
    {
        this.Follow();
        
    }

    private void Follow()
    {
        this.transform.position = basePos + new Vector3(target.position.x, target.position.y);
    }
}
