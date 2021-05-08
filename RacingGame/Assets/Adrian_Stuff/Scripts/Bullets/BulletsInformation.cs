using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBulletInformation", menuName = "Data/BulletInformation")]
public class BulletsInformation : ScriptableObject
{
    public float BulletLifeTime => _bulletLifeTime;

    public float BulletDamage => _bulletDamage;

    [SerializeField]
    private float _bulletLifeTime = 10.0f;

    [SerializeField]
    private float _bulletDamage = 10.0f;

}
