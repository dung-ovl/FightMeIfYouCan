using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum State
{
    NotStarted = 0,
    Started = 1,
    Completed = 2,
}

public class WaveManager : GameMonoBehaviour
{
    [SerializeField] protected float startDelay = 1f;
    public float StartDelay => startDelay;


    [SerializeField] protected State currentState;
    public State CurrentState => currentState;

    [SerializeField] protected List<Transform> _spawnedUnits;

    [SerializeField] protected List<Transform> spawnPoints;

    [SerializeField] protected List<WaveProfileSO> waveProfile;

    public List<WaveProfileSO> WaveProfile => waveProfile;

    [SerializeField] protected int currentWaveIndex;
    public int CurrentWaveIndex => currentWaveIndex;

    private static WaveManager instance;

    public static WaveManager Instance { get => instance; }

    public bool isWaveSpawnComplete = false;
    public bool isAllSpawnedUnitsDead = false;
    private int amountOfUnit;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    protected virtual void Update()
    {
        this.CheckOnWaveCompleted();
        this.CheckOnAllUnitDead();
        this.CheckIsWaveSpawnComplete();
    }

    protected override void Start()
    {
        base.Start();
        this.LoadSpawnPoint();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaveProfile();
    }

    public void LoadCurrentWave()
    {
        _spawnedUnits = new List<Transform>();
        this.isWaveSpawnComplete = false;
        this.isAllSpawnedUnitsDead = false;
        this.amountOfUnit = 0;
        foreach (var item in this.waveProfile[this.currentWaveIndex].waves)
        {
            this.amountOfUnit += item.amount;
        }
        
        this.currentState = State.NotStarted;
    }

    public bool LoadNextWave()
    {
        this.currentWaveIndex++;
        if (this.currentWaveIndex >= this.waveProfile.Count)
        {
            return false;
        }
        this.LoadCurrentWave();
        return true;
    }

    private void LoadWaveProfile()
    {
        if (this.waveProfile.Count > 0) return;
        string resPath = "Wave/";
        this.waveProfile = Resources.LoadAll<WaveProfileSO>(resPath).ToList();
        Debug.Log(transform.name + ": LoadShipProfile", gameObject);
        this.currentWaveIndex = 0;
    }

    private void LoadSpawnPoint()
    {
        this.spawnPoints = SpawnPoint.Instance.SpawnPoints;
    }

    protected virtual void CheckOnWaveCompleted()
    {
        if (this.currentState == State.NotStarted) return;
        if (!this.isWaveSpawnComplete) return;
        if (!this.isAllSpawnedUnitsDead) return;
        this.currentState = State.Completed;
    }

    public virtual void StartWave()
    {
        if (this.currentState == State.NotStarted)
        {
            this.currentState = State.Started;
            StartCoroutine(this.StartSpawn());
        }
    }

    protected virtual IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(this.startDelay);
        StartCoroutine(this.SpawnEnemyRandom());

    }

    protected virtual IEnumerator SpawnEnemyRandom()
    {
        foreach (var item in this.waveProfile[this.currentWaveIndex].waves)
        {
            int amount = item.amount;
            while (amount > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, this.spawnPoints.Count);
                Vector3 spawnPoint = this.spawnPoints[randomIndex].position;
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                WaveProfileUnit unit = item;
                Debug.Log(unit.name);
                Transform newEnemy = EnemySpawner.Instance.Spawn(unit.name, spawnPoint, rot);
                if (newEnemy == null) yield return null;
                if (!this._spawnedUnits.Contains(newEnemy))
                {
                    this._spawnedUnits.Add(newEnemy);
                }
                newEnemy.gameObject.SetActive(true);
                amount--;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void CheckOnAllUnitDead()
    {
        if (!this.isWaveSpawnComplete) return;
        foreach (var spawnedUnit in this._spawnedUnits)
        {
            if (!EnemySpawner.Instance.CheckObjectInPool(spawnedUnit))
                return;
        }
        this._spawnedUnits.Clear();
        this.isAllSpawnedUnitsDead = true;
    }

    protected void CheckIsWaveSpawnComplete()
    {
        if (this._spawnedUnits.Count == amountOfUnit)
        {
            isWaveSpawnComplete = true;
        }
    }
}
