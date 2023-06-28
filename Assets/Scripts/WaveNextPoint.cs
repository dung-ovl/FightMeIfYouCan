using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveNextPoint : FollowTarget
{
    public bool isCollison = false;
    protected override void Start()
    {
        base.Start();
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.gameObject.CompareTag("Player"))
        {
            isCollison = true;
        }
    }

    public void TurnOffPoint()
    {
        this.isCollison = false;
        this.gameObject.SetActive(false);
    }

    public void TurnOnPoint()
    {
        this.isCollison = false;
        this.gameObject.SetActive(true);
    }
}
