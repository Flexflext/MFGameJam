using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    //private 

    private float _score;

    [SerializeField]
    private int _bonusForEnemyKilled;

    [SerializeField]
    private int _amountPerMeter;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public void EnemyDied()
    {
        _score += _bonusForEnemyKilled;
    }

    public void AddScore(float meters)
    {
        _score += meters * _amountPerMeter;
    }

}
