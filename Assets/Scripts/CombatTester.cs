using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class CombatTester : PlayerAbstract
{
    [SerializeField] private bool canAttack = true;

    [SerializeField] private Collider2D inLineCollider;

    [SerializeField] private LayerMask enemyLayer;

    public List<Collider2D> cols = new List<Collider2D>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInlineCollider();
    }

    private void LoadInlineCollider()
    {
        inLineCollider = transform.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetContactCollider();
    }

    private void GetContactCollider()
    {
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(enemyLayer);
        inLineCollider.OverlapCollider(contactFilter2D, cols);
    }
}
