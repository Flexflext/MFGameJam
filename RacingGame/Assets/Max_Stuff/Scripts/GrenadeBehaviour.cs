using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float mMaxExplosionTimer;
    protected float mExplosionTimer;

    [SerializeField]
    protected int BulletAmount;


    protected virtual void Start()
    {
        mExplosionTimer = mMaxExplosionTimer;
    }

    protected virtual void Update()
    {
        if (gameObject.activeSelf)
        {
            if (mExplosionTimer <= 0)
            {
                Expolde();
                mExplosionTimer = mMaxExplosionTimer;
            }
            else
                mExplosionTimer -= Time.deltaTime;

        }
    }

    protected virtual void Expolde()
    {
        SpawnManager.Instance.UnsubscribeGrenadeFromField(this.gameObject);

    }
}
