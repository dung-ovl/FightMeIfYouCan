using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GamePlayStatus
{
    None,
    Win,
    Lose,
}

public class LevelLoad : GameMonoBehaviour
{
    [SerializeField] private TMP_Text MainText;
    [SerializeField] private TMP_Text SubText;

    private bool isPressing = false;

    private GamePlayStatus gamePlay = GamePlayStatus.None;

    override protected void LoadComponents()
    {
        MainText = transform.Find("MainText").GetComponent<TMP_Text>();
        SubText = transform.Find("SubText").GetComponent<TMP_Text>();
    }

    protected override void OnEnable()
    {
        if (gamePlay == GamePlayStatus.Win)
        {
            SoundManager.Instance.PlaySound("LevelWin");
        }
        else if (gamePlay == GamePlayStatus.Lose)
        {
            SoundManager.Instance.PlaySound("Gameover");
        }
    }

    public void SetGamePlayStatus(GamePlayStatus gamePlay)
    {
        this.gamePlay = gamePlay;
        if (gamePlay == GamePlayStatus.Win)
        {
            SetText("You Win!");
        }
        else if (gamePlay == GamePlayStatus.Lose)
        {
            SetText("Game Over!");
        }
    }

    public void SetText(string text)
    {
        MainText.text = text;
    }

    private void Update()
    {
        if (isPressing)
        {
            SubText.color = Color.Lerp(Color.white, Color.red, (float)Math.Sin(Time.time * 40f));
        }
        ;
        OnRestartPressed();
    }


    public void OnRestartPressed()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(LoadLevelCoroutine());
            // You can perform any desired action here
        }

    }

    protected virtual IEnumerator LoadLevelCoroutine()
    {
        isPressing = true;
        SoundManager.Instance.PlaySound("ButtonPress");
        yield return new WaitForSeconds(2f);
    }
}
