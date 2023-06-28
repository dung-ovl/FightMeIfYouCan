using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelLoad : LevelLoad
{

    protected override IEnumerator LoadLevelCoroutine()
    {
        yield return base.LoadLevelCoroutine();
        GameManager.Instance.RestartLevel();
    }
}
