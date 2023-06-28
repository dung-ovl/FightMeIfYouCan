using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashLerpEffecr : GameMonoBehaviour
{
    [SerializeField] private TMP_Text MainText;

    public List<Color> Colors = new List<Color>() { Color.white, Color.yellow, Color.red, Color.green, Color.cyan, Color.magenta};

    private float targetPoint;

    private int currentColorIndex = 0;

    protected override void LoadComponents()
    {
        MainText = GetComponent<TMP_Text>();

    }

    private void Update()
    {
        Transition();
    }

    void Transition()
    {
        targetPoint += Time.deltaTime;
        MainText.color = Color.Lerp(Colors[currentColorIndex], Colors[currentColorIndex + 1], targetPoint);
        if (targetPoint >= 1)
        {
            targetPoint = 0;
            currentColorIndex++;
            if (currentColorIndex >= Colors.Count - 1)
            {
                currentColorIndex = 0;
            }
        }
    }
}
