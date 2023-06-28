using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : FollowTarget
{
    [SerializeField] private List<Transform> spawnPoints;

    public List<Transform> SpawnPoints => spawnPoints;

    private static SpawnPoint instance;

    public static SpawnPoint Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpawnPoint>();
            }
            return instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnPoint();
    }

    private void LoadSpawnPoint()
    {
        foreach (Transform t in transform)
        {
            this.spawnPoints.Add(t);
        }
    }
}
