using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private float mMaxThrowTimer;
    private float mThrowTimer;

    [SerializeField]
    private float mMaxEnemyAttackTimer;
    private float mEnemyAttackTimer;

    [SerializeField]
    private float mMaxRndMovePosTimer;
    private float mRndMovePosTimer;

    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private float mDamage;


    [SerializeField]
    private int mMaxThrowAmount;
    private int mThrowAmount;


    private GameObject mBoardSize;

    [SerializeField]
    private GameObject mPlayer;
    private PlayerHealth mPlayerHealth;

    [SerializeField]
    private float mAttackRange;

    private Vector3 mRndPos;

    private bool mRndPosFound = false;
    private bool mIsAttackingPlayer = false;
    private bool mGrenadeHit = false;
    private bool mGrenadeSpawned = false;
    private bool mFoundPlayer;

    private bool IsGettingHit = false;

    private bool IsHittingPlayer = false;

    [SerializeField]
    private float BorderThickness;

    private Vector2 GrenadeRndPos;

    private Vector3 PlayerPos;

    GameObject mGrenade;

    Animator Anim;
    private float AttackAnimTimer;

    Rigidbody2D Rb;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();

        Anim = GetComponent<Animator>();
        mPlayerHealth = SpawnManager.Instance.PlayerHealth;

        mThrowAmount = mMaxThrowAmount;

        mBoardSize = SpawnManager.Instance.Board;

        mPlayer = SpawnManager.Instance.Player;

       

        mEnemyAttackTimer = Random.Range(mMaxEnemyAttackTimer / 2, mMaxEnemyAttackTimer);
        mThrowTimer = Random.Range(mMaxThrowTimer / 2, mMaxThrowTimer);
    }

    private void Update()
    {
        Anim.SetBool("IsMoving", true);

        if (Rb.velocity == Vector2.zero)
        {
            IsGettingHit = false;
        }
        if (Rb.velocity.x > 0)
        {

            IsGettingHit = true;
        }

        if (mRndMovePosTimer <= 0 && this.isActiveAndEnabled && !mRndPosFound && !IsHittingPlayer && !IsGettingHit)
        {
            RndMovePosition();
        }
        else
            mRndMovePosTimer -= Time.deltaTime;

        if (mEnemyAttackTimer <= 0 && !IsHittingPlayer && !IsGettingHit)
        {
            MoveToPlayer();
            mIsAttackingPlayer = true;
        }
        else
        { 
            mFoundPlayer = false;
            mEnemyAttackTimer -= Time.deltaTime;
        }
            

        if (mThrowTimer <= 0 && !IsHittingPlayer)
        {
            EnemyThrowsGrenade();    
        }
        else
            mThrowTimer -= Time.deltaTime;

        if (transform.position != mRndPos && !mIsAttackingPlayer && !IsGettingHit)
        {
            transform.position = Vector2.MoveTowards(transform.position, mRndPos, mSpeed * Time.deltaTime);
        }

        if (Vector3.Magnitude(mPlayer.transform.position - this.transform.position) <= mAttackRange && !IsGettingHit)
        {
            IsHittingPlayer = true;

            mEnemyAttackTimer = Random.Range(mMaxEnemyAttackTimer / 2, mMaxEnemyAttackTimer);
            mThrowTimer = Random.Range(mMaxThrowTimer / 2, mMaxThrowTimer);
            Anim.SetTrigger("IsAttacking");
            AttackAnimTimer = Anim.GetCurrentAnimatorClipInfo(0).Length;
            if (AttackAnimTimer <= 0)
            {
                mIsAttackingPlayer = false;
                mRndMovePosTimer -= mRndMovePosTimer;
                IsHittingPlayer = false;
                mRndPosFound = false;
            }
            else
                AttackAnimTimer -= Time.deltaTime;

            mPlayerHealth.StunPlayer();
            mPlayerHealth.TakeDamage(mDamage);
        }
        else
            IsHittingPlayer = false;


    }

    private void RndMovePosition()
    {
        if (!mRndPosFound)
        {
            float rndPosX = Random.Range(mBoardSize.transform.position.x - mBoardSize.transform.localScale.x / 2, mBoardSize.transform.position.x + mBoardSize.transform.localScale.x / 2);
            float rndPosY = Random.Range(mBoardSize.transform.position.y - mBoardSize.transform.localScale.y / 2 + BorderThickness, mBoardSize.transform.position.y + mBoardSize.transform.localScale.y / 2 - BorderThickness);
            Debug.Log("DDDDD");
            mRndPos = new Vector2(rndPosX, rndPosY);
            mRndPosFound = true;
        }
        
        if (transform.position == mRndPos)
        {
            mRndPosFound = false;
            mRndMovePosTimer = mMaxRndMovePosTimer;
        }
    }

    private void MoveToPlayer()
    {
        if (!mFoundPlayer)
        {
            PlayerPos = mPlayer.transform.position;
            mFoundPlayer = true;
        }


        mRndMovePosTimer = mMaxRndMovePosTimer;
        this.transform.position = Vector2.MoveTowards(this.transform.position, PlayerPos, mSpeed * Time.deltaTime);

        if (transform.position == PlayerPos)
        {
            mEnemyAttackTimer = Random.Range(mMaxEnemyAttackTimer / 2, mMaxEnemyAttackTimer);
            mIsAttackingPlayer = false;
            mFoundPlayer = false;
        }
    }

    private void EnemyThrowsGrenade()
    {
        if (!mGrenadeSpawned)
        {
            mGrenadeSpawned = true;
            int rndG = Random.Range(SpawnManager.Instance.GrenadesToSpawn.Count - SpawnManager.Instance.GrenadesToSpawn.Count, SpawnManager.Instance.GrenadesToSpawn.Count);

            float rndPosX = Random.Range(mBoardSize.transform.position.x - mBoardSize.transform.localScale.x / 2, mBoardSize.transform.position.x + mBoardSize.transform.localScale.x / 2);
            float rndPosY = Random.Range(mBoardSize.transform.position.y - mBoardSize.transform.localScale.y / 2, mBoardSize.transform.position.y + mBoardSize.transform.localScale.y / 2);
            GrenadeRndPos = new Vector2(rndPosX, rndPosY);
            Debug.Log(GrenadeRndPos);
            mGrenade = SpawnManager.Instance.GrenadesToSpawn[rndG];

            mGrenade.transform.position = transform.position;
            SpawnManager.Instance.SubscribeGrenadeToField(mGrenade);
        }

        mGrenade.transform.position = Vector2.MoveTowards(mGrenade.transform.position, GrenadeRndPos, mSpeed * Time.deltaTime);

        if (mGrenade.transform.position == (Vector3)GrenadeRndPos || !mGrenade.activeSelf)
        {
            mGrenadeSpawned = false;
            mThrowTimer = Random.Range(mMaxThrowTimer / 2, mMaxThrowTimer);
        }
    }


    public void EnemyIsColliding()
    {
        SpawnManager.Instance.UnsubscribeEnemyFromField(this);

        SpawnManager.Instance.CurrentEnemiesOnField--;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(mRndPos, 0.5f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            mEnemyAttackTimer = Random.Range(mMaxEnemyAttackTimer / 2, mMaxEnemyAttackTimer);
            mPlayerHealth.StunPlayer();
            mPlayerHealth.TakeDamage(mDamage);
        }

        if (collision.collider.CompareTag("EndBorder"))
        {
            SpawnManager.Instance.UnsubscribeEnemyFromField(this);



            SpawnManager.Instance.CurrentEnemiesOnField--;
        }
    }
}
