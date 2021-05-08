using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{
    [SerializeField]
    private float mMaxExplosionTimer;
    private float mExplosionTimer;

    [SerializeField]
    private int BulletAmount;


    private void Start()
    {
        mExplosionTimer = mMaxExplosionTimer;
    }

    private void Update()
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

    public void Expolde()
    {
        SpawnManager.Instance.UnsubscribeGrenadeFromField(this);

    }
}
