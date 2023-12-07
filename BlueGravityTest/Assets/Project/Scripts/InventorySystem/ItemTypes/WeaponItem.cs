using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon Item", menuName = "NekraByte/Item/Weapon/New Weapon Item")]
public class WeaponItem : MonoBehaviour
{
    [SerializeField, Range(10f, 200f)]  float Damage        = 10f;
    [SerializeField, Range(1f, 10f)]    float AttackSpeed   = 2f;

    public GameObject bodyPartPrefab = null;
}