using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] float maxTimeBetweenSpawns;
    [SerializeField] private float minTimeBetweenSpawns;
    private float currentTime;

    [SerializeField] private GameObject pickUpPrefab;

    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        currentTime = maxTimeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            SpawnPickUp();
            currentTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        }
    }

    private void SpawnPickUp()
    {
        Vector3 positionToSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        Instantiate(pickUpPrefab, positionToSpawn, Quaternion.identity, this.transform);
    }
}
