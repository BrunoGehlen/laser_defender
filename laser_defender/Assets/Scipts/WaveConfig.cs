using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class WaveConfig : ScriptableObject
{
    [Header("Game Objects")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [Header("Wave Config")]
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] float moveSpeed;
    [SerializeField] int numberOfEnemies = 5;

    public GameObject GetEnemyPrefab()       { return enemyPrefab; }
    public List<Transform>GetWayPoints()        
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform) 
        { waveWaypoints.Add(child); }
        return waveWaypoints; 
    }
    public float GettimeBeteenSpawns()       { return timeBetweenSpawns; }
    public float GetspawnRandomFactor()      { return spawnRandomFactor; }
    public float GetmoveSpeed()              { return moveSpeed; }
    public int GetnumberOfEnemies()          { return numberOfEnemies; }
}
