using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameMonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    [SerializeField] protected Transform currentShip;
    public Transform CurrentShip { get => currentShip; }

    protected override void Awake()
    {
        base.Awake();
        if (GameManager.instance != null) Debug.LogError("Only 1 GameCtrl allow to exist");
        GameManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCurrentShip();
    }

    protected virtual void LoadCurrentShip()
    {
        if (this.currentShip != null) return;
        this.currentShip = GameObject.Find("Player").transform;
        Debug.Log(transform.name + ": LoadPlayer", gameObject);
    }
}
