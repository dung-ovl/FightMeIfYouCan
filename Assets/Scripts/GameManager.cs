using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GameMonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    [SerializeField] protected Camera mainCamera;
    public Camera MainCamera { get => mainCamera; }

    [SerializeField] protected Transform currentShip;
    public Transform CurrentShip { get => currentShip; }

    [SerializeField] protected Transform currentEnemy;
    public Transform CurrentEnemy { get => currentEnemy; }

    public  bool IsPlayerDead { get => this.currentShip == null; }

    public float m_MaxX { get; private set; }

    public float m_MinX { get; private set; }

    public float m_MaxY { get; private set; }

    public float m_MinY { get; private set; }

    [SerializeField] protected float limitOffsetX = 1f;
    [SerializeField] protected float limitOffsetY = 0.4f;

    protected override void Awake()
    {
        base.Awake();
        if (GameManager.instance != null) Debug.LogError("Only 1 GameCtrl allow to exist");
        GameManager.instance = this;
        SoundManager.Instance.TurnOnMusic();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCurrentShip();
        this.LoadCamera();
        this.LoadLimitScreen();
    }

    private void Update()
    {
        LoadLimitScreen();
    }
    protected virtual void LoadCamera()
    {
        if (this.mainCamera != null) return;
        this.mainCamera = GameManager.FindObjectOfType<Camera>();
        Debug.Log(transform.name + ": LoadCamera", gameObject);
    }

    private void LoadLimitScreen()
    {
        this.m_MinX = this.mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + limitOffsetX;
        this.m_MaxX = this.mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - limitOffsetX;
        this.m_MinY = this.mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + limitOffsetY;
        this.m_MaxY = this.mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y - limitOffsetY;
    }

    protected virtual void LoadCurrentShip()
    {
        if (this.currentShip != null) return;
        this.currentShip = GameObject.Find("Player").transform;
        Debug.Log(transform.name + ": LoadPlayer", gameObject);
    }

    public virtual void SetCurrentEnemy(Transform enemy)
    {
        this.currentEnemy = enemy;
    }

    public virtual void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
