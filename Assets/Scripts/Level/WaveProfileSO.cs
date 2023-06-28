using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveProfile", menuName = "ScriptableObject/WaveProfile")]
public class WaveProfileSO : ScriptableObject
{
    public List<WaveProfileUnit> waves;

}
