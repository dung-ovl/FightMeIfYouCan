using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HandNextWaveUI : GameMonoBehaviour
{
    [SerializeField] private Image handUI;

    [SerializeField] private float flashDelay = 0f;
    [SerializeField] private float flashTimer = 0f;

    bool isVisible = true;

    protected override void Start()
    {
        base.Start();
        this.SetImageActive(false);
    }

    private void Update()
    {
        if (WaveManager.Instance.CurrentState == State.NotStarted)
        {
            ShowHandFlash();
            
        }
        else
        {
            StopHandFlash();
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImage();
    }

    private void LoadImage()
    {
        this.handUI = GetComponentInChildren<Image>();
    }

    protected virtual void SetImageActive(bool active)
    {
        this.handUI.gameObject.SetActive(active);
    }

    public void StopHandFlash()
    {
        this.SetImageActive(false);
    }

    private void ShowHandFlash()
    {
        flashTimer += Time.deltaTime;
        if (flashTimer < flashDelay) return;
        SetImageActive(isVisible);
        isVisible = !isVisible;
        if (isVisible)
        {
            
            flashDelay = 0.2f;
        }
        else
        {
            SoundManager.Instance.PlaySound("HandPointer");
            flashDelay = 1f;
        }
        flashTimer = 0f;
    }
}

