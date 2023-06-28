using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateManager : ObjectAbstract
{
    [SerializeField] protected ObjectState objectState = ObjectState.Idle;

    public ObjectState ObjectState => objectState;

    public bool isInteracting = false;
    public float interactTimer = 0;

    protected override void OnEnable()
    {
        this.UpdateInteractTimer(0f);
    }

    public void SetState(ObjectState state)
    {
        this.objectState = state;
        this.HandleState();
    }

    private void Update()
    {
        if (this.interactTimer > 0)
        {
            this.interactTimer -= Time.deltaTime;
        }
        else
        {
            this.isInteracting = false;
            this.SetState(ObjectState.Idle);
        }
    }

    private void HandleState()
    {
        if (this.objectState == ObjectState.Attack)
        {
            this.UpdateInteractTimer(0.2f);
        }
        else if (this.objectState == ObjectState.Pickup)
        {
            this.UpdateInteractTimer(0.6f);
        }
        else if (this.objectState == ObjectState.Die)
        {
            this.UpdateInteractTimer(10f);
        }
        else if (this.objectState == ObjectState.Hurt)
        {
            this.UpdateInteractTimer(0.3f);
        }
        else if (this.objectState == ObjectState.KnockDown)
        {
            this.UpdateInteractTimer(2f);
        }
        else if (this.objectState == ObjectState.StandUp)
        {
            this.UpdateInteractTimer(1f);
        }
    }

    public void UpdateInteractTimer(float time)
    {
        isInteracting = true;
        this.interactTimer = time;
    }
}
