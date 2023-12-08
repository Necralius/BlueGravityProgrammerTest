using UnityEngine;

/// <summary>
/// Controller for a top-down character.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(InputManager), typeof(Animator))]
[RequireComponent(typeof(CharacterAspects), typeof(PaperdollController))]
public class ControllerTopDown : MonoBehaviour
{
    // Direct Dependencies
    private     Rigidbody2D             _rigidbody          => GetComponent<Rigidbody2D>();
    private     Animator                _animator           => GetComponent<Animator>();
    private     InputManager            _inputManager       => GetComponent<InputManager>();
    private     GameController          _gameController     => GetComponentInChildren<GameController>();
    public      CharacterAspects        aspects             => GetComponent<CharacterAspects>();
    public      PaperdollController     paperDoll           => GetComponent<PaperdollController>();
    private     InteractionController   _interacController  = null;

    [HideInInspector] public InventoryController invController = null;

    // Animator Hashes
    private int isMovingHash    = Animator.StringToHash("IsMoving");
    private int isRunningHash   = Animator.StringToHash("IsRunning");
    private int isAttackingHash = Animator.StringToHash("Attack");
    private int attackTypeHash  = Animator.StringToHash("AttackType");
    private int deathHash       = Animator.StringToHash("Death");

    // Private Data
    private Vector2 _lastInput          = new Vector2(0, -1);
    private Vector3 _startScale         = new Vector3(1, 1, 1);
    private float _targetMovementSpeed  = 5f;

    // Inspector Assigned Data
    [Header("Player Settings")]
    [SerializeField, Range(1f, 20f)]  private float _walkSpeed  = 5f;
    [SerializeField, Range(1f, 20f)]  private float _runSpeed   = 5f;
    [SerializeField, Range(1f, 100f)] private float _dashSpeed  = 10f;

    [Space, Header("Player State")]
    [SerializeField] private bool _isMoving     = false;
    [SerializeField] private bool _isRunning    = false;
    [SerializeField] private bool _isAttacking  = false;
    [SerializeField] private bool _canAttack    = true;
    [SerializeField] private bool _isDashing    = false;
    [SerializeField] private bool _canDash      = true;

    [Header("Dash System")]
    [SerializeField, Range(1f, 10f)]    private float _dashDelay        = 2f;
    [SerializeField, Range(0.1f, 10f)]  private float _totalDashTime    = 2f;

    private float _dashTimer        = 0f;
    private float _dashDelayTimer   = 0f;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _startScale = transform.localScale; // Saves the player local scale.
        invController = GetComponentInChildren<InventoryController>(true);
        _interacController = GetComponent<InteractionController>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        PlayerStateManagement();
    }

    /// <summary>
    /// FixedUpdate is called every fixed frame-rate frame.
    /// </summary>
    private void FixedUpdate()
    {
        if (_gameController.inventoryOpen || _gameController.isPaused) return;
        MovementCalculation();
    }

    /// <summary>
    /// Calculates the player movement and applies it to the character rigidbody.
    /// </summary>
    private void MovementCalculation()
    {
        // If the character is attacking, the movement is limited.
        if (_isAttacking) return;

        // Set the movement to the rigidbody, considering the actual position and the current target speed.
        _rigidbody.MovePosition(_rigidbody.position + _inputManager.Move.normalized * _targetMovementSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Manages the player's state, input, and animations.
    /// </summary>
    private void PlayerStateManagement()
    {
        // Input Management
        if (_inputManager.InventoryAction.WasPressedThisFrame())    _gameController.InventoryInteraction();
        if (_inputManager.PauseMenuAction.WasPressedThisFrame())    _gameController.PauseInteraction();
        if (_inputManager.InteractAction.WasPressedThisFrame())     _interacController.Interact();

        if (_inputManager.DashAction.WasPressedThisFrame() && _canDash)
        {
            _canDash = false;
            _isDashing = true;
            _dashTimer = 0f;
        }

        if (!_canDash)
        {
            if (_dashTimer >= _totalDashTime) _isDashing = false;
            else _dashTimer += Time.deltaTime;

            if (!_isDashing)
            {
                if (_dashDelayTimer >= _dashDelay)
                {
                    _canDash = true;
                    _dashDelayTimer = 0f;
                }
                else _dashDelayTimer += Time.deltaTime;
            }
        }

        // Select the correct speed based on the player state (if dashing, walking, or running)
        _targetMovementSpeed = _isDashing ? _dashSpeed : _isRunning ? _runSpeed : _walkSpeed;

        // Animation Management

        // Set the animator values according to the current player state.
        _animator?.SetBool(isMovingHash, _isMoving);
        _animator?.SetBool(isRunningHash, _isRunning);

        // Stop the player if the game is paused or if the inventory is open.
        if (_gameController.isPaused || _gameController.inventoryOpen)
        {
            _isMoving = false;
            _isRunning = false;
            return;
        }

        // Set the player's current behavior state, mainly using the input.
        _isMoving = _inputManager.Move != Vector2.zero;
        _isRunning = _isMoving && _inputManager.Running;

        if (_canAttack)
        {
            if (_inputManager.Attack) Attack(0);
            else if (_inputManager.HeavyAttack) Attack(1);
        }

        // Detect and save the last position that the player was moving to.
        if (_isMoving)
            _lastInput = _inputManager.Move;

        // Mirror the character sprites horizontally, simulating a flip on the character sprite.
        if (!_isAttacking)
            CharacterFlipHandler();
    }

    /// <summary>
    /// Handles flipping the character horizontally based on the movement direction.
    /// </summary>
    private void CharacterFlipHandler()
    {
        Vector3 newScale = _startScale;
        newScale.x = _inputManager.Move.x > 0 || _lastInput.x > 0 ? _startScale.x : _startScale.x * -1;

        transform.localScale = newScale;
    }

    /// <summary>
    /// Initiates an attack animation with a specified attack type.
    /// </summary>
    /// <param name="attackType">The type of attack to perform.</param>
    private void Attack(int attackType)
    {
        _isAttacking = true;
        _animator?.SetBool(isAttackingHash, true);
        _animator?.SetInteger(attackTypeHash, attackType);
    }

    /// <summary>
    /// Cancels the ongoing attack animation.
    /// </summary>
    public void CancelAttack()
    {
        _animator?.SetBool(isAttackingHash, false);

        _isAttacking = false;
        _canAttack = true;
    }

    /// <summary>
    /// Handles the behavior of the character on death.
    /// </summary>
    public void DeathBehavior()
    {
        _animator.SetTrigger(deathHash);

        _gameController.OnDeath();
    }

    /// <summary>
    /// Deactivates the player GameObject. Typically called after the player's death to hide them from the scene.
    /// </summary>
    public void DeactivatePlayer() => gameObject.SetActive(false);
}