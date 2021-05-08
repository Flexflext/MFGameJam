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
    public float Speed;

    [SerializeField]
    private int mThrowAmount;

    private Vector2 mBoardSize;

    [SerializeField]
    private GameObject mPlayer;

    [SerializeField]
    private float mAttackRange;

    private Vector3 RndPos;

    private bool RndPosFound = false;

    private void Start()
    {
        mBoardSize = SpawnManager.Instance.BoardSize;

        mPlayer = SpawnManager.Instance.Player;

        mEnemyAttackTimer = mMaxEnemyAttackTimer;
        mThrowTimer = mMaxThrowTimer;
    }

    private void Update()
    {
        if (mRndMovePosTimer <= 0 && this.isActiveAndEnabled && !RndPosFound)
        {
            RndMovePosition();
        }
        else
            mRndMovePosTimer -= Time.deltaTime;

        if (mEnemyAttackTimer <= 0 && transform.position == RndPos)
        {
            MoveToPlayer();
        }
        else
            mEnemyAttackTimer -= Time.deltaTime;

        if (mThrowTimer <= 0)
        {
            EnemyThrowsGrenade();
            mThrowTimer = mMaxThrowTimer;
        }
        else
            mThrowTimer -= Time.deltaTime;


        if (Vector3.Magnitude(mPlayer.transform.position - this.transform.position) <= mAttackRange)
        {
            // TODO: Call AttackPlayer from Felix
        }
    }

    private void RndMovePosition()
    {
        if (!RndPosFound)
        {
            float rndPosX = Random.Range(mBoardSize.x - mBoardSize.x / 2, mBoardSize.x);
            float rndPosY = Random.Range(mBoardSize.y - mBoardSize.y / 2, mBoardSize.y);
            RndPos = new Vector2(rndPosX, rndPosY);
            RndPosFound = true;
        }
        

        transform.position = Vector2.MoveTowards(transform.position, RndPos, Speed * Time.deltaTime);

        if (transform.position == RndPos)
        {
            RndPosFound = false;
            mRndMovePosTimer = mMaxRndMovePosTimer;
        }
    }

    private void MoveToPlayer()
    {

        Vector2 playerPos = mPlayer.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPos, Speed * Time.deltaTime);

        if (transform.position == (Vector3)playerPos)
        {
            mEnemyAttackTimer = mMaxEnemyAttackTimer;
        }
    }

    private void EnemyThrowsGrenade()
    {
        for (int i = 0; i < mThrowAmount; i++)
        {
            float rndPosX = Random.Range(mBoardSize.x - mBoardSize.x, mBoardSize.x);
            float rndPosY = Random.Range(mBoardSize.y - mBoardSize.y, mBoardSize.y);
            Vector2 rndPos = new Vector2(rndPosX, rndPosY);

            GrenadeBehaviour grenade = SpawnManager.Instance.GrenadesToSpawn[i];

            grenade.transform.position = Vector2.MoveTowards(transform.position, rndPos, Speed * Time.deltaTime);
            SpawnManager.Instance.SubscribeGrenadeToField(grenade);
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
        Gizmos.DrawWireSphere(RndPos, 0.5f);
    }

}
