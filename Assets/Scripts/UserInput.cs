using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;
    public bool UpJustPressed { get; private set; }
    public bool DownJustPressed { get; private set; }
    public bool LeftJustPressed { get; private set; }
    public bool RightJustPressed { get; private set; }

    private PlayerInput _playerInput;
    private InputAction upAction;
    private InputAction downAction;
    private InputAction leftAction;
    private InputAction rightAction;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _playerInput = GetComponent<PlayerInput>();
        SetUpInputActions();
    }

    private void SetUpInputActions()
    {
        upAction = _playerInput.actions["Up"];
        downAction = _playerInput.actions["Down"];
        leftAction = _playerInput.actions["Left"];
        rightAction = _playerInput.actions["Right"];
    }

    private void UpdateInput()
    {
        UpJustPressed = upAction.WasPressedThisFrame();
        DownJustPressed = downAction.WasPressedThisFrame();
        LeftJustPressed = leftAction.WasPressedThisFrame();
        RightJustPressed = rightAction.WasPressedThisFrame();
    }

    void Update()
    {
        UpdateInput();
    }
}
