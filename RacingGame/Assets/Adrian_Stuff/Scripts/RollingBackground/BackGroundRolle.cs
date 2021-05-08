using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRolle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _RoadPrefabs;

    [SerializeField]
    private float _tileSize;

    [SerializeField]
    private int _roadTileAmount;

    [SerializeField]
    private float _scrollSpeed;

    [SerializeField]
    private Transform _startPos;

    [SerializeField]
    private Transform _endPos;

    private List<GameObject> _activeRoads = new List<GameObject>();

    private List<GameObject> _inacitveRoads = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _roadTileAmount; i++)
        {
            int id = Random.Range(0, _RoadPrefabs.Length);

            GameObject go = Instantiate(_RoadPrefabs[id], this.transform);
            go.transform.position = _startPos.position;
            go.SetActive(false);
            _inacitveRoads.Add(go);
        }

        StartSetUp();
    }

    private void Update()
    {
        RollMap();
        AddScore();
    }

    private void AddScore()
    {
        ScoreManager.Instance.AddScore(Time.deltaTime * _scrollSpeed);
    }

    private void RollMap()
    {
        float shortestPos = float.PositiveInfinity;
        GameObject g = null;
        Vector3 dir = _endPos.position - _startPos.position;
        float length = dir.sqrMagnitude;

        List<GameObject> turnOff = new List<GameObject>();

        foreach (GameObject go in _activeRoads)
        {
            go.transform.position += dir.normalized * (_scrollSpeed * Time.deltaTime);

            float dist = (go.transform.position - _startPos.position).sqrMagnitude;

            if (dist < shortestPos)
            {
                shortestPos = dist;
                g = go;
            }

            if (dist >= length)
            {
                turnOff.Add(go);
            }
        }

        if (turnOff.Count > 0)
        {
            float dif = Mathf.Sqrt(shortestPos) - _tileSize;

            SpawnNewRoadTileAt(_startPos.position + (dir.normalized * dif));

            foreach (GameObject go in turnOff)
            {
                InActivateTile(go);
            }
        }

    }

    private void StartSetUp()
    {
        Vector3 dir = _endPos.position - _startPos.position;

        float length = dir.magnitude;

        float AmountOfTIles = (length / _tileSize);

        for (int i = 0; i < AmountOfTIles; i++)
        {
            Vector3 pos = _startPos.position + (dir.normalized * (i * _tileSize));
            SpawnNewRoadTileAt(pos);
        }
    }

    private void SpawnNewRoadTileAt(Vector3 pos)
    {
        GameObject spawn = GetRandomTileFromInActive();
        spawn.transform.position = pos;
        _activeRoads.Add(spawn);
        spawn.SetActive(true);
    }

    private GameObject GetRandomTileFromInActive()
    {

        int id = Random.Range(0, _inacitveRoads.Count);

        GameObject tile = _inacitveRoads[id];

        _inacitveRoads.Remove(tile);

        return tile;
    }

    private void InActivateTile(GameObject tile)
    {
        _activeRoads.Remove(tile);
        tile.SetActive(false);
        tile.transform.position = _startPos.position;
        _inacitveRoads.Add(tile);
    }



}
