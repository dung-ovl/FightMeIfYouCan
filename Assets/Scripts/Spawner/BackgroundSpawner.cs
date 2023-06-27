using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : Spawner
{
    private static BackgroundSpawner instance;
    public static BackgroundSpawner Instance { get => instance; }

    public string Background_1 = "Background_1";
    protected override void Awake()
    {
        base.Awake();
        BackgroundSpawner.instance = this;
    }
}
