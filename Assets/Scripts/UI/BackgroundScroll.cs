using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : GameMonoBehaviour
{
    [SerializeField] protected float scrollSpeed = 100f;
    [SerializeField] protected Vector3 startPos = Vector3.zero;
    [SerializeField] protected RectTransform rectTransform;
    [SerializeField] protected float offSet = -1920f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMeshRenderer();
        this.LoadStartPoint();
    }

    private void LoadStartPoint()
    {
        this.startPos = this.rectTransform.localPosition;
    }

    private void LoadMeshRenderer()
    {
        if (this.rectTransform != null) return;
        this.rectTransform = transform.GetComponent<RectTransform>();
        Debug.Log(transform.name + ": LoadRectTransform", gameObject);
    }

    private void Update()
    {
        this.Scroll();
    }

    protected virtual void Scroll()
    {
        this.rectTransform.localPosition += Vector3.left * scrollSpeed * Time.deltaTime;
        if (this.rectTransform.localPosition.x <= offSet)
        {
            this.rectTransform.localPosition = Vector3.zero + this.startPos;
        }
    }

    /*private void MoveOnTop()
    {
        if (transform.position.y <= -4)
        {
            transform.position = new Vector3(0, 3.99f, 0);
        }
    }*/
}
