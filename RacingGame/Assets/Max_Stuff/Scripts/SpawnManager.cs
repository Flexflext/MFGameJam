using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public List<GameObject> GrenadesToSpawn => mGrenadesToSpawn;
    public PlayerHealth PlayerHealth => mPlayerHealth;
    public int CurrentEnemiesOnField { get => mCurrentEnemiesOnField; set => mCurrentEnemiesOnField = value; }
    public GameObject Board => mBoard;

    public GameObject Player => mPlayer;

    [SerializeField]
    private EnemyBehaviour mEnemyPrefab;

    [SerializeField]
    private GameObject mSpawnLine;

    [SerializeField]
    private float mMaxSpawnTimer;
    private float mSpawnTimer;

    [SerializeField]
    private int mEnemiesToInstantiate;

    [SerializeField]
    private int mMaxEnemiesOnField;
    private int mCurrentEnemiesOnField;

    [SerializeField]
    private GameObject mPlayer;
    [SerializeField]
    private PlayerHealth mPlayerHealth;

    private List<EnemyBehaviour> mEnemiesToSpawn;
    private List<EnemyBehaviour> mEnemiesOnTheField;

    private List<GameObject> mGrenadesToSpawn;
    private List<GameObject> mGrenadesOnTheField;

    [SerializeField]
    private GameObject mBoard;

    [SerializeField]
    private GameObject mNormalGrenadePrefab;
    [SerializeField]
    private GameObject mSpiralGrenadePrefab;
    [SerializeField]
    private float mGrenadeAmount;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        if (mPlayer != null)
        {
            mPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        if (mPlayerHealth != null)
        {
            mPlayerHealth = FindObjectOfType<PlayerHealth>();
        }


        mPlayerHealth = FindObjectOfType<PlayerHealth>();

        mEnemiesToSpawn = new List<EnemyBehaviour>();
        mEnemiesOnTheField = new List<EnemyBehaviour>();
        mGrenadesToSpawn = new List<GameObject>();
        mGrenadesOnTheField = new List<GameObject>();

        mSpawnTimer = mMaxSpawnTimer;

        for (int i = 0; i < mEnemiesToInstantiate; i++)
        {
            EnemyBehaviour enemy = Instantiate(mEnemyPrefab, transform.position, transform.rotation);
            enemy.transform.position = transform.position;
            enemy.transform.SetParent(this.transform);
            enemy.gameObject.SetActive(false);

            UnsubscribeEnemyFromField(enemy);
        }

        for (int i = 0; i < mGrenadeAmount; i++)
        {
            int rndNade = Random.Range(0, 2);

            if (rndNade == 0)
            {
                GameObject grenade = Instantiate(mNormalGrenadePrefab, transform.position, transform.rotation);
                grenade.transform.position = transform.position;
                grenade.transform.SetParent(this.transform);
                grenade.gameObject.SetActive(false);

                UnsubscribeGrenadeFromField(grenade);
            }
            else
            {

                GameObject grenade = Instantiate(mSpiralGrenadePrefab, transform.position, transform.rotation);
                grenade.transform.position = transform.position;
                grenade.transform.SetParent(this.transform);
                grenade.gameObject.SetActive(false);

                UnsubscribeGrenadeFromField(grenade);
            }
        }
    }

    private void Update()
    {
        if (mCurrentEnemiesOnField < mMaxEnemiesOnField)
        {
            for (int i = 0; i < mMaxEnemiesOnField; i++)
            {
                if (mSpawnTimer <= 0)
                {
                    SpawnEnemy(mEnemiesToSpawn[i]);
                    mSpawnTimer = mMaxSpawnTimer;
                    mCurrentEnemiesOnField++;
                }
                else
                {
                    mSpawnTimer -= Time.deltaTime;
                }
            }
        }

    }

    private void SpawnEnemy(EnemyBehaviour _enemy)
    {
        float rndPosX = Random.Range(mSpawnLine.transform.position.x - mSpawnLine.transform.localScale.x / 2, mSpawnLine.transform.position.x + mSpawnLine.transform.localScale.x / 2);
        Vector2 rndPos = new Vector2(rndPosX, mSpawnLine.transform.position.y);

        _enemy.transform.position = rndPos;

        SubscribeEnemyToField(_enemy);
    }

    public void SubscribeEnemyToField(EnemyBehaviour _enemyToAdd)
    {
        if (!mEnemiesOnTheField.Contains(_enemyToAdd))
        {
            mEnemiesOnTheField.Add(_enemyToAdd);
            _enemyToAdd.gameObject.SetActive(true);
        }
        if (mEnemiesToSpawn.Contains(_enemyToAdd))
        {
            mEnemiesToSpawn.Remove(_enemyToAdd);
        }
    }

    public void UnsubscribeEnemyFromField(EnemyBehaviour _enemyToRemove)
    {
        if (mEnemiesOnTheField.Contains(_enemyToRemove))
        {
            mEnemiesOnTheField.Remove(_enemyToRemove);
            _enemyToRemove.transform.position = transform.position;
            _enemyToRemove.gameObject.SetActive(false);
        }
        if (!mEnemiesToSpawn.Contains(_enemyToRemove))
        {
            mEnemiesToSpawn.Add(_enemyToRemove);
        }
    }

    public void SubscribeGrenadeToField(GameObject _grenadeToAdd)
    {
        if (!mGrenadesOnTheField.Contains(_grenadeToAdd))
        {
            mGrenadesOnTheField.Add(_grenadeToAdd);
            _grenadeToAdd.gameObject.SetActive(true);
        }
        if (mGrenadesToSpawn.Contains(_grenadeToAdd))
        {
            mGrenadesToSpawn.Remove(_grenadeToAdd);
        }
    }

    public void UnsubscribeGrenadeFromField(GameObject _grenadeToRemove)
    {
        if (mGrenadesOnTheField.Contains(_grenadeToRemove))
        {
            mGrenadesOnTheField.Remove(_grenadeToRemove);
            _grenadeToRemove.transform.position = transform.position;
            _grenadeToRemove.gameObject.SetActive(false);
        }
        if (!mGrenadesToSpawn.Contains(_grenadeToRemove))
        {
            mGrenadesToSpawn.Add(_grenadeToRemove);
        }
    }
}
