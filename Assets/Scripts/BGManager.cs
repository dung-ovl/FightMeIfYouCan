using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BGManager : GameMonoBehaviour
{
    [SerializeField] private float x_OffSet = 20f;
    [SerializeField] private float lastDistance = 20f;


    [SerializeField] private Transform lastObject;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        lastObject = BackgroundSpawner.Instance.Spawn("Background_1", transform.position, transform.rotation);
        if (lastObject == null) return;
        lastObject.gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        UpdateBackground();
    }

    private void UpdateBackground()
    {
        while ((GameManager.Instance.MainCamera.transform.position - lastObject.position).magnitude < lastDistance)
        {
            this.SpawnBackground();
        }
    }

    private void SpawnBackground()
    {
        Vector2 pos = lastObject.position;
        pos.x += x_OffSet;
        lastObject = BackgroundSpawner.Instance.Spawn("Background_1", pos, Quaternion.identity);
        if (lastObject == null) return;
        lastObject.gameObject.SetActive(true);
    }

}
