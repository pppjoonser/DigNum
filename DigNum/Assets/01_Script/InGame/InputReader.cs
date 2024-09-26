using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    PlayerControl inputActions;
    public static InputReader Instance { get; private set; }

    public event Action<Vector2> OnMove;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있으면 중복된 오브젝트를 제거
        }
    
        if (inputActions == null)
        {
            inputActions = new PlayerControl();
            inputActions.Player.PlayerMovement.Enable();
            inputActions.Player.PlayerMovement.performed += OnMovePressed;
        }
    }

    private void OnMovePressed(InputAction.CallbackContext context)
    {
        OnMove.Invoke(context.ReadValue<Vector2>());
    }
}
