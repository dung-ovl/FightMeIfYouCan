using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartLevelLoad : LevelLoad
{
    protected override IEnumerator LoadLevelCoroutine()
    {
        yield return base.LoadLevelCoroutine();
        SceneManager.LoadScene("GamePlayScene");
    }
}
