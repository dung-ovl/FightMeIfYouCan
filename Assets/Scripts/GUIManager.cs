using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GUIManager : GameMonoBehaviour
{

    [SerializeField] private LevelLoad endLevel;
    // Start is called before the first frame update
    override protected void LoadComponents()
    {
        endLevel = transform.GetComponentInChildren<LevelLoad>();
    }

    protected override void Start()
    {
        endLevel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckOnLose();
        CheckOnWin();
    }

    private void CheckOnWin()
    {
        if (LevelManager.Instance.CurrentState == LevelState.Completed)
        {
            endLevel.SetGamePlayStatus(GamePlayStatus.Win);
            endLevel.gameObject.SetActive(true);
            SoundManager.Instance.TurnOffMusic();
        }
    }

    private void CheckOnLose()
    {
        if (GameManager.Instance.IsPlayerDead)
        {
            endLevel.SetGamePlayStatus(GamePlayStatus.Lose);
            endLevel.gameObject.SetActive(true);
            SoundManager.Instance.TurnOffMusic();
        }
    }

    private void SetTextActive(string text)
    {
        endLevel.gameObject.SetActive(true);
        endLevel.SetText(text);
        
    }
}
