using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum LevelState
{
    NotStarted = 0,
    Ready = 1,
    Started = 2,
    Completed = 3,
}

public class LevelManager : GameMonoBehaviour
{
    private LevelState currentState = LevelState.NotStarted;

    public LevelState CurrentState
    {
        get { return currentState; }
    }

    private int currentWaveIndex = 0;

    [SerializeField] private WaveNextPoint waveNextPoint;

    private static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }

    protected override void Awake()
    {
        instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaveNextPoint();
    }

    private void LoadWaveNextPoint()
    {
        this.waveNextPoint = GameObject.FindAnyObjectByType<WaveNextPoint>();
    }

    protected override void Start()
    {
        base.Start();
        this.StartLevel();
    }

    private void Update()
    {
        WaveProgress();
        if (waveNextPoint.isCollison == true)
        {
            if (!CameraMove.Instance.Move()) return;
            WaveManager.Instance.StartWave();
            CameraMove.Instance.MoveTo(GameManager.Instance.MainCamera.transform.position + new Vector3(12f, 0, 0));
            waveNextPoint.TurnOffPoint();
        }
    }

    private void WaveProgress()
    {
        if (currentState != LevelState.Started) return;
        /*if (waves[currentWaveIndex].CurrentState != State.Completed) return;*/
        if (WaveManager.Instance.CurrentWaveIndex < WaveManager.Instance.WaveProfile.Count)
        {
            // Check if the current wave has been completed
            if (WaveManager.Instance.CurrentState == State.Completed)
            {
                if (WaveManager.Instance.LoadNextWave())
                {
                    waveNextPoint.TurnOnPoint();
                }
            }
        }
        // End the level if all waves have been completed
        else if (currentState == LevelState.Started && WaveManager.Instance.CurrentWaveIndex == WaveManager.Instance.WaveProfile.Count)
        {
            EndLevel();
        }
    }

    private void StartLevel()
    {
        if (currentState == LevelState.NotStarted)
        {
            if (WaveManager.Instance.WaveProfile.Count <= 0) return;
            WaveManager.Instance.LoadCurrentWave();
            WaveManager.Instance.StartWave();
            CameraMove.Instance.MoveTo(GameManager.Instance.MainCamera.transform.position + new Vector3(12f, 0, 0));
            currentState = LevelState.Started;
        }
    }

    private void EndLevel()
    {
        currentState = LevelState.Completed;
    }
}
