using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : GameMonoBehaviour
{
    [SerializeField] Transform target;

    protected override void Start()
    {
        this.LoadTarget();
    }

    private void LoadTarget()
    {
        this.target = GameManager.Instance.MainCamera.transform;
    }

    private void Update()
    {
        this.FollowTarget();
    }

    private void FollowTarget()
    {
        this.transform.position = this.target.position;
    }
}
