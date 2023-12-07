using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    #region - Singleton Pattern -
    public static InputManager Instance;
    #endregion

    //Direct Dependencies
    private PlayerInput     _inputAsset         => GetComponent<PlayerInput>();
    private InputActionMap  _currentActionMap   = null;

    [Header("Input Values")]
    public Vector2  Move            = new Vector2(0, 0);
    public Vector2  PointerPos      = new Vector2(0, 0);
    public bool     Running         = false;
    public bool     Attack          = false;
    public bool     HeavyAttack     = false;
    public bool     Dash            = false;
    public bool     Interacted      = false;
    public bool     PausedGame      = false;
    public bool     InventoryOpen   = false;

    [HideInInspector] public InputAction MoveAction         = null;
    [HideInInspector] public InputAction PointerAction      = null;
    [HideInInspector] public InputAction AttackAction       = null;
    [HideInInspector] public InputAction DashAction         = null;

    [HideInInspector] public InputAction HeavyAttackAction  = null;
    [HideInInspector] public InputAction RunAction          = null;

    [HideInInspector] public InputAction InteractAction     = null;
    [HideInInspector] public InputAction PauseMenuAction    = null;
    [HideInInspector] public InputAction InventoryAction    = null;

    private void Awake()
    {
        Instance = this;

        _currentActionMap = _inputAsset.currentActionMap;

        MoveAction          = _currentActionMap.FindAction("Move");
        PointerAction       = _currentActionMap.FindAction("Pointer");
        RunAction           = _currentActionMap.FindAction("Run");
        DashAction          = _currentActionMap.FindAction("Dash");
        AttackAction        = _currentActionMap.FindAction("Attack");
        HeavyAttackAction   = _currentActionMap.FindAction("HeavyAttack");
        InteractAction      = _currentActionMap.FindAction("Interact");
        PauseMenuAction     = _currentActionMap.FindAction("PauseInteraction");
        InventoryAction     = _currentActionMap.FindAction("InventoryInteraction");

        MoveAction.performed        += onMove;
        PointerAction.performed     += onPointerMoved;
        RunAction.performed         += onRun;
        DashAction.performed        += onDash;
        AttackAction.performed      += onAttack;
        HeavyAttackAction.performed += onHeavyAttack;
        InteractAction.performed    += onInteract;
        PauseMenuAction.performed   += onPause;
        InventoryAction.performed   += onInventoryOpen;

        MoveAction.canceled         += onMove;
        PointerAction.canceled      += onPointerMoved;
        RunAction.canceled          += onRun;
        DashAction.canceled         += onDash;
        AttackAction.canceled       += onAttack;
        HeavyAttackAction.canceled  += onHeavyAttack;
        InteractAction.canceled     += onInteract;
        PauseMenuAction.canceled    += onPause;
        InventoryAction.canceled    += onInventoryOpen;
    }

    private void onMove(InputAction.CallbackContext context)            => Move             = context.ReadValue<Vector2>();
    private void onPointerMoved(InputAction.CallbackContext context)    => PointerPos       = context.ReadValue<Vector2>();
    private void onRun(InputAction.CallbackContext context)             => Running          = context.ReadValueAsButton();
    private void onAttack(InputAction.CallbackContext context)          => Attack           = context.ReadValueAsButton();
    private void onHeavyAttack(InputAction.CallbackContext context)     => HeavyAttack      = context.ReadValueAsButton();
    private void onDash(InputAction.CallbackContext context)            => Dash             = context.ReadValueAsButton();
    private void onInteract(InputAction.CallbackContext context)        => Interacted       = context.ReadValueAsButton();
    private void onPause(InputAction.CallbackContext context)           => PausedGame       = context.ReadValueAsButton();
    private void onInventoryOpen(InputAction.CallbackContext context)   => InventoryOpen    = context.ReadValueAsButton();

    private void OnDisable()
    {
        _currentActionMap.Disable();
    }
    private void OnEnable()
    {
        _currentActionMap.Enable();
    }
}