using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class Bullets : MonoBehaviour
{
    public ProjectileManager _myManager;

    private Rigidbody2D _myRB;

    [SerializeField]
    private BulletsInformation _info;

    private bool _active;
    private float _startTime;

    private void Awake()
    {
        _myRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_active)
            return;

        CheckLifeTime();
    }

    public void Spawn(Vector3 at, Vector3 direction, float speed)
    {
        this.transform.position = at;
        //this.gameObject.SetActive(true);
        _startTime = Time.time;
        _myRB.velocity = direction * speed;
        _active = true;
    }

    private void CheckLifeTime()
    {
        if (Time.time > _startTime * _info.BulletLifeTime)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Debug.Log("Player");
            HitPlayer(col.GetComponent<PlayerHealth>());
            return;
        }

        if(col.tag == "Enemy")
        {
            Debug.Log("Enemy");

            HitEnemy(col.GetComponent<EnemyBehaviour>());
            return;
        }
    }

    private void HitPlayer(PlayerHealth player)
    {
        player.TakeDamage(_info.BulletDamage);

        Die();
    }

    private void HitEnemy(EnemyBehaviour Enemy)
    {
        //TODO:
        //Debug.LogError("Not implented");

        Enemy.EnemyIsColliding();

        Die();
    }

    private void Die()
    {
        _active = false;
        _myManager.ReturnBullet(this);
    }

}
