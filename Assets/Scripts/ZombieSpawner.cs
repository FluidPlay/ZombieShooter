using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    public GameObject ZombiePrefab;
    public float spawnDelay = 5f;
    public List<Transform> SpawnPoints;
    public List<GameObject> SpawnedZombies;

    void Start()
    {
        InvokeRepeating(nameof(SpawnZombie), spawnDelay, spawnDelay);
    }

    void SpawnZombie()
    {
        Vector3 spawnPos = PickSpawnPosition();
        var spawnedZombie = Instantiate(ZombiePrefab, spawnPos, Quaternion.identity);
        spawnedZombie.transform.SetParent(Game.Manager.SceneRoot);
        SpawnedZombies.Add(spawnedZombie);
    }

    private Vector3 PickSpawnPosition()
    {
        int idx = Random.Range(0, SpawnPoints.Count);
        return SpawnPoints[idx].position;
    }
}
