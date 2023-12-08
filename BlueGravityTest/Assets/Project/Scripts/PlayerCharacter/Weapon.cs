using UnityEngine;

/// <summary>
/// Represents a weapon that can perform attacks and deal damage.
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject gunOwner = null;

    [Header("Attack Parameters")]
    [SerializeField] private Transform  _attackOrigin   = null;
    [SerializeField] private float      _attackRadius   = 3f;
    [SerializeField] private string     _targetTag      = "Enemy";

    [HideInInspector] public int Damage = 10;

    public int killValues = 0;

    /// <summary>
    /// Triggers the attack, dealing damage to entities within the attack radius.
    /// </summary>
    public void TriggerAttack()
    {
        // Iterate through all colliders within the attack radius
        foreach (Collider2D col in Physics2D.OverlapCircleAll(_attackOrigin.position, _attackRadius))
        {
            // Check if the collider has the specified target tag and is not a trigger
            if (col.CompareTag(_targetTag) && !col.isTrigger)
            {
                // Deal damage to EnemyAI or ControllerTopDown components
                if (col.GetComponent<EnemyAI>())
                {
                    EnemyAI enemy = col.GetComponent<EnemyAI>();
                    if (enemy.TakeDamage(Damage, gunOwner.gameObject))
                        killValues += enemy._moneyValue;
                }
                else if (col.GetComponent<ControllerTopDown>())
                    col.GetComponent<ControllerTopDown>().aspects.TakeDamage(Damage);
            }
        }

        // If the weapon owner is a ControllerTopDown, update money based on kill values
        if (gunOwner.GetComponent<ControllerTopDown>())
        {
            gunOwner.GetComponent<ControllerTopDown>().aspects.MoneyTransaction(killValues, true);
            killValues = 0;
        }
    }
}