using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : Spawner
{
    private static FXSpawner instance;
    public static FXSpawner Instance { get => instance; }

    public string Impact1 = "Impact_1";

    protected override void Awake()
    {
        base.Awake();
        FXSpawner.instance = this;
    }
}
