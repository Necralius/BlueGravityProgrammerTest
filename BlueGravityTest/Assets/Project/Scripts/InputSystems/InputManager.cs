using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    //Direct Dependencies
    private PlayerInput     _inputAsset         => GetComponent<PlayerInput>();
    private InputActionMap  _currentActionMap   => _inputAsset.currentActionMap;

    [Header("Input Values")]
    public Vector2  Move            = new Vector2(0, 0);
    public bool     Running         = false;
    public bool     Attack          = false;
    public bool     HeavyAttack     = false;
    public bool     Dash            = false;

    [HideInInspector] public InputAction MoveAction         = null;
    [HideInInspector] public InputAction AttackAction       = null;
    [HideInInspector] public InputAction DashAction         = null;
    [HideInInspector] public InputAction HeavyAttackAction  = null;
    [HideInInspector] public InputAction RunAction          = null;

    private void Awake()
    {
        MoveAction          = _currentActionMap.FindAction("Move");
        RunAction           = _currentActionMap.FindAction("Run");
        DashAction          = _currentActionMap.FindAction("Dash");
        AttackAction        = _currentActionMap.FindAction("Attack");
        HeavyAttackAction   = _currentActionMap.FindAction("HeavyAttack");

        MoveAction.performed        += onMove;
        RunAction.performed         += onRun;
        DashAction.performed        += onDash;
        AttackAction.performed      += onAttack;
        HeavyAttackAction.performed += onHeavyAttack;

        MoveAction.canceled         += onMove;
        RunAction.canceled          += onRun;
        DashAction.canceled         += onDash;
        AttackAction.canceled       += onAttack;
        HeavyAttackAction.canceled  += onHeavyAttack;
    }

    private void onMove(InputAction.CallbackContext context)            => Move         = context.ReadValue<Vector2>();
    private void onRun(InputAction.CallbackContext context)             => Running      = context.ReadValueAsButton();
    private void onAttack(InputAction.CallbackContext context)          => Attack       = context.ReadValueAsButton();
    private void onHeavyAttack(InputAction.CallbackContext context)     => HeavyAttack  = context.ReadValueAsButton();
    private void onDash(InputAction.CallbackContext context)            => Dash         = context.ReadValueAsButton();

    private void OnDisable()
    {
        _currentActionMap.Disable();
    }
    private void OnEnable()
    {
        _currentActionMap.Enable();
    }
}