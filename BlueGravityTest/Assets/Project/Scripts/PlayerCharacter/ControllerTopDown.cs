using NekraliusDevelopmentStudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputManager), typeof(Animator))]
[RequireComponent(typeof(CharacterAspects), typeof(PaperdollController))]
public class ControllerTopDown : MonoBehaviour
{
    //Direct Dependencies
    private Rigidbody2D             _rigidbody          => GetComponent<Rigidbody2D>();
    private Animator                _animator           => GetComponent<Animator>();
    private InputManager            _inputManager       => GetComponent<InputManager>();
    private GameController          _gameController     => GetComponentInChildren<GameController>();
    public  CharacterAspects        aspects             => GetComponent<CharacterAspects>();
    public  PaperdollController     paperDoll           => GetComponent<PaperdollController>();
    private InteractionController   _interacController  = null;

    [HideInInspector] public  InventoryController     invController       = null;

    //Animator Hashes
    private int isMovingHash        = Animator.StringToHash("IsMoving");
    private int isRunningHash       = Animator.StringToHash("IsRunning");
    private int isAttackingHash     = Animator.StringToHash("Attack");
    private int attackTypeHash      = Animator.StringToHash("AttackType");

    //Private Data
    private Vector2 _lastInput              = new Vector2(0, -1);
    private Vector3 _startScale             = new Vector3(1, 1, 1);
    private float   _targetMovementSpeed    = 5f;

    //Inspector Assigned Data
    [Header("Player Settings")]
    [SerializeField, Range(1f, 20f)]  private float walkSpeed   = 5f;
    [SerializeField, Range(1f, 20f)]  private float runSpeed    = 5f;
    [SerializeField, Range(1f, 100f)] private float dashSpeed   = 10f;

    [Space, Header("Player State")]
    [SerializeField] private bool isMoving      = false;
    [SerializeField] private bool isRunning     = false;
    [SerializeField] private bool isAttacking   = false;
    [SerializeField] private bool canAttack     = true;

    //Attack System
    [Space, Header("Combat System Settings")]
    [SerializeField] private float _rateOfAttack = 2f;
    [SerializeField] private float _attackDamage = 10f;

    // ----------------------------------------------------------------------
    // Name: Awake (Method)
    // Desc: This method is called on the very scene start.
    // ----------------------------------------------------------------------
    private void Awake()
    {
        _startScale         = transform.localScale; //Saves the player local scale.
        invController       = GetComponentInChildren<InventoryController>(true);
        _interacController  = GetComponent<InteractionController>();
    }

    // ----------------------------------------------------------------------
    // Name: Update (Method)
    // Desc: This method is called every frame of the game.
    // ----------------------------------------------------------------------
    private void Update()
    {
        PlayerStateManagement();
    }

    // ----------------------------------------------------------------------
    // Name: FixedUpdate (Method)
    // Desc: This method is called an fixed quantity of times per sec,
    //       normally used to physics calls.
    // ----------------------------------------------------------------------
    private void FixedUpdate()
    {
        if (_gameController.inventoryOpen || _gameController.isPaused) return;
        MovementCalculation();
    }

     //----------------------------------------------------------------------
     // Name: MovementCalculation (Method)
     // Desc: This method calculates the player movement and applies it on the
     //       character rigidbody.
     //----------------------------------------------------------------------
    private void MovementCalculation()
    {
        // If the character is attacking the movement is limited.
        if (isAttacking) return;

        //Select the correct speed, based on the player state. (if is dashing, walking or running)
        _targetMovementSpeed = _inputManager.Dash ? dashSpeed : isRunning ? runSpeed : walkSpeed;

        //Set the movement to the rigidbody, considering the actual position and the current target speed.
        _rigidbody.MovePosition(_rigidbody.position + _inputManager.Move.normalized * _targetMovementSpeed * Time.fixedDeltaTime);
    }

     //----------------------------------------------------------------------
     // Name: Animation Manager (Method)
     // Desc: This method manages all the animation functions, setting the
     //       animator values and mading the needed changes in the character
     //       sprite.
     //----------------------------------------------------------------------
    private void PlayerStateManagement()
    {
        // Input Management
        if (_inputManager.InventoryAction.WasPressedThisFrame())    _gameController.InventoryInteraction();
        if (_inputManager.PauseMenuAction.WasPressedThisFrame())    _gameController.PauseInteraction();
        if (_inputManager.InteractAction.WasPressedThisFrame())     _interacController.Interact();

        // Animation Management

        //Set the animator values, according with the current player state.
        _animator.SetBool(isMovingHash, isMoving);
        _animator.SetBool(isRunningHash, isRunning);

        //Stop the player if the game is paused or if the inventory is open.
        if (_gameController.isPaused || _gameController.inventoryOpen)
        {
            isMoving    = false;
            isRunning   = false;
            return;
        }

        //Sets the player current behavior state, using as base mainly the input.
        isMoving    = _inputManager.Move != Vector2.zero;
        isRunning   = isMoving && _inputManager.Running;

        if (canAttack)
        {
            if (_inputManager.Attack)           Attack(0);
            else if (_inputManager.HeavyAttack) Attack(1);
        }

        //Detects and save the last position that the player was moving to.
        if (isMoving)
            _lastInput = _inputManager.Move;

        //Mirror the character sprites horizontally, simulation an flip on the character sprite.
        if (!isAttacking) 
            CharacterFlipHandler();
    }

    // ----------------------------------------------------------------------
    // Name: CharacterFlipHandler (Method)
    // Desc: This method is used to flix de character horizontally, following
    //       their movement and the last movement input, to maintain the
    //       character in the wanted flip direction.
    // ----------------------------------------------------------------------
    private void CharacterFlipHandler()
    {
        Vector3 newScale        = _startScale;
        newScale.x              = _inputManager.Move.x > 0 || _lastInput.x > 0 ? _startScale.x : _startScale.x * -1;

        transform.localScale    = newScale;
    }

    // ----------------------------------------------------------------------
    // Name: Attack (Method)
    // Desc: This method starts an Attack animation, also, the method
    //       receives an attack type as an int, that is used to inflict
    //       diffrent attacks.
    // ----------------------------------------------------------------------
    private void Attack(int attackType)
    {
        isAttacking = true;
        _animator.SetBool(isAttackingHash, true);
        _animator.SetInteger(attackTypeHash, attackType);
    }

    // ----------------------------------------------------------------------
    // Name: CancelAttack (Method)
    // Desc: This method is used as an animation event, mainly the method
    //       deactivates the attack system, when the attack animation has
    //       ended.
    // ----------------------------------------------------------------------
    public void CancelAttack()
    {
        _animator.SetBool(isAttackingHash, false);

        isAttacking     = false;
        canAttack       = true;
    }
}