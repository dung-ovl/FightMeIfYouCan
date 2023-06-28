using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashingEffect : GameMonoBehaviour
{
    [SerializeField] private TMP_Text MainText;



    protected override void LoadComponents()
    {
        MainText = GetComponent<TMP_Text>();

    }

    private void Update()
    {
        MainText.color = LerpColor(Color.yellow, Color.red, 30f);
    }

    public Color LerpColor(Color a, Color b, float speed)
    {
        return Color.Lerp(a, b, Mathf.Sin(Time.time * speed));
    }
}
