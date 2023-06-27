using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    public float horizontalInput = 0;
    public float verticalInput = 0;
    public bool jumpingInput = false;
    public bool attackPunchInput = false;
    public bool attackKickInput = false;
    public bool pickUpInput = false;

    private void Awake()
    {
        InputManager.instance = this;
    }

    private void Update()
    {
        this.HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        attackPunchInput = Input.GetKeyDown(KeyCode.J);
        pickUpInput = Input.GetKeyDown(KeyCode.J);
        attackKickInput = Input.GetKeyDown(KeyCode.K);
    }
}
