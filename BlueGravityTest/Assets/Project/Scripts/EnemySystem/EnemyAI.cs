using System.Collections;
using UnityEngine;

/// <summary>
/// EnemyAI class controls the behavior of an enemy character.
/// </summary>
[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    // Direct Dependencies
    private Animator            _animator       => GetComponent<Animator>();
    private CapsuleCollider2D   _collider       => GetComponent<CapsuleCollider2D>();
    private SpriteRenderer      _spriteRenderer => GetComponent<SpriteRenderer>();
    private Rigidbody2D         _rigidbody      => GetComponent<Rigidbody2D>();

    // Animator Hashes
    private int xHash           = Animator.StringToHash("X");
    private int yHash           = Animator.StringToHash("Y");
    private int isMovingHash    = Animator.StringToHash("IsMoving");
    private int attackHash      = Animator.StringToHash("Attack");
    private int reviveHash      = Animator.StringToHash("Revive");
    private int deathHash       = Animator.StringToHash("Death");

    // Private Data
    private Vector2     _direction      = Vector2.zero;
    private bool        _isMoving       = false;
    private bool        _isDead         = false;
    private float       _attackTimer    = 1;
    private float       _reviveTimer    = 0f;
    private Transform   _playerTarget   = null;

    [Header("AI Settings")]
    [SerializeField, Range(10f, 150f)] private int _healthValue                 = 100;
    [SerializeField, Range(3f, 15f)]   private float _timeToRevive              = 5f;
    [SerializeField, Range(1f, 20f)]   private float _walkSpeed                 = 4f;
    [SerializeField, Range(1f, 15f)]   private float _chaseDistanceThreshold    = 3f;
    [SerializeField, Range(0.1f, 3f)]  private float _attackDistanceThreshold   = 0.8f;
    [SerializeField, Range(1f, 10f)]   private float _timeBetweenAttacks        = 1f;

    [Range(30, 250)] public int _moneyValue = 40;

    [Header("Knockback Settings")]
    [SerializeField, Range(0.1f, 1f)] private float _knockbackDelay     = 0.15f;
    [SerializeField, Range(1f, 100f)] private float _knockbackStrenth   = 10f;

    [SerializeField] private Weapon _equippedWeapon = null;

    /// <summary>
    /// Gets or sets the health of the enemy, used to limit the life, and also detect when the enemy dies.
    /// </summary>
    public int Health
    {
        get => _healthValue;
        set
        {
            if (value <= 0)
            {
                _healthValue = 0;
                Death();
            }
            else if (value > 150) _healthValue = 150;
            else _healthValue = value;
        }
    }

    /// <summary>
    /// Called every frame, manages mainly all persistent actions of the enemy.
    /// </summary>
    private void Update()
    {
        if (_playerTarget == null) //if the target is invalid the enemy stays on idle state.
            return;

        if (_isDead)
        {
            //If the enemy is dead, the revive behavior is started, countin an certain time before revive completly the enemy.
            if (_reviveTimer >= _timeToRevive)
            {
                ReviveEnemy();
                _reviveTimer = 0f;
            }
            else _reviveTimer += Time.deltaTime;

            _isMoving = false;

            return;
        }

        //Updates the movement and animation.
        MovementManagement();
        AnimationManagement();
    }

    /// <summary>
    /// Manages the enemy behavior like chase, idle, attack states.
    /// </summary>
    private void MovementManagement()
    {
        float distance = Vector2.Distance(_playerTarget.position, transform.position);

        if (distance < _chaseDistanceThreshold) //If the target is in the chase area, the enemy chase him.
        {
            if (distance <= _attackDistanceThreshold)
            {
                if (_attackTimer >= _timeBetweenAttacks) //Makes the enemy agent attack only after a certain time.
                {
                    _attackTimer = 0;
                    _equippedWeapon.GetComponent<Animator>().SetTrigger(attackHash);
                }
            }
            else
            {
                //Chasing behavior, calculates the direction to be followed, and make the enemy move to it, considering his speed.
                _isMoving           = true;
                Vector2 direction   = _playerTarget.position - transform.position;

                Vector2 currentPosition     = transform.position;
                Vector2 newPosition         = currentPosition + direction.normalized * _walkSpeed * Time.deltaTime;
                transform.position          = newPosition;

                _direction                  = direction.normalized;
            }
        }
        else _isMoving = false;

        //Count the time between the attacks
        if (_attackTimer < _timeBetweenAttacks)
            _attackTimer += Time.deltaTime;
    }

    /// <summary>
    /// Manages animations of the enemy.
    /// </summary>
    private void AnimationManagement()
    {
        _animator?.SetBool(isMovingHash, _isMoving);
        _animator?.SetFloat(xHash, _direction.x);
        _animator?.SetFloat(yHash, _direction.y);

        //Manages the objects sprite flip state, based on his direction values.
        _spriteRenderer.flipX = _direction.x > 0;
        _equippedWeapon.GetComponent<SpriteRenderer>().flipX = _direction.x > 0;
    }

    /// <summary>
    /// Handles the death behavior of the enemy.
    /// </summary>
    private void Death()
    {
        _direction = Vector2.zero;
        _isDead = true;
        _animator?.SetTrigger(deathHash);
    }

    /// <summary>
    /// Deactivates the enemy object, simulating his death.
    /// </summary>
    public void DeactivateObject()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        _equippedWeapon.gameObject.SetActive(false);

        _collider.enabled = false;
    }

    /// <summary>
    /// Revives the enemy, reseting his health value, money and visual elements.
    /// </summary>
    private void ReviveEnemy()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        _equippedWeapon.gameObject.SetActive(true);

        _moneyValue = Random.Range(30, 200); //Reset money

        _animator.SetTrigger(reviveHash);

        _collider.enabled = true;
        _healthValue = 100; //Reset life
        _isDead = false;
    }

    /// <summary>
    /// Takes damage from an external source.
    /// </summary>
    /// <param name="damageValue">Amount of damage to take.</param>
    /// <param name="damageSender">The GameObject causing the damage.</param>
    /// <returns>Returns true if the enemy is dead after taking damage.</returns>
    public bool TakeDamage(int damageValue, GameObject damageSender)
    {
        Health -= damageValue; //Decrease the health value.

        ReceiveKnockback(damageSender); //Apllies knockback.
        return _isDead;
    }

    /// <summary>
    /// Applies knockback force to the enemy, based on the sender position.
    /// </summary>
    /// <param name="sender">The GameObject causing the knockback.</param>
    private void ReceiveKnockback(GameObject sender)
    {
        StopAllCoroutines(); //Stop previous knockbacks

        Vector2 direction = (transform.position - sender.transform.position).normalized;//Calculates the force direction.

        GetComponent<SpriteRenderer>().color = Color.red;
        _rigidbody.AddForce(direction * _knockbackStrenth, ForceMode2D.Impulse); //Aplly the force
        StartCoroutine(ResetKnockback()); //Calls the knockback reset.
    }

    /// <summary>
    /// Resets the enemy velocity after a certain delay, canceling the knockback.
    /// </summary>
    /// <returns></returns>
    IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(_knockbackDelay);
        GetComponent<SpriteRenderer>().color = Color.white;

        _rigidbody.velocity = Vector2.zero;
    }

    /// <summary>
    /// Handles the trigger event when another collider enters the trigger zone.
    /// </summary>
    /// <param name="other">The Collider2D of the other GameObject.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering GameObject has the "Player" tag
        if (other.CompareTag("Player"))
            _playerTarget = other.transform;  // Set the player as the target
        else
            _playerTarget = null;  // Clear the target if the entering GameObject is not the player
    }

    /// <summary>
    /// Handles the trigger event while another collider stays within the trigger zone.
    /// </summary>
    /// <param name="other">The Collider2D of the other GameObject.</param>
    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the staying GameObject has the "Player" tag
        if (other.CompareTag("Player"))
            _playerTarget = other.transform;  // Set the player as the target
        else
            _playerTarget = null;  // Clear the target if the staying GameObject is not the player
    }

    /// <summary>
    /// Handles the trigger event when another collider exits the trigger zone.
    /// </summary>
    /// <param name="other">The Collider2D of the other GameObject.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        _playerTarget = null;  // Clear the target when the GameObject exits the trigger zone
    }

}