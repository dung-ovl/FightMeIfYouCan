using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponents();
    }
    protected virtual void Reset()
    {
        this.LoadComponents();
    }
    protected virtual void LoadComponents()
    {

    }
    protected virtual void Start()
    {
    }

    protected virtual void ResetValue()
    {
    }

    protected virtual void OnEnable()
    {
    }

}
