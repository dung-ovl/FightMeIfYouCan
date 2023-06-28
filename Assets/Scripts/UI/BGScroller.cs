using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField] private int x_AmountInit = 2;
    [SerializeField] private float x_OffSet = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateBackground();
    }

    private void UpdateBackground()
    {
        
    }
}
