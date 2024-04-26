using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class Spawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float minCooldown;
    public float maxCooldown;
    private float cooldown;
    private List<GameObject> spawnedObstacles = new List<GameObject>();

    void Start()
    {
        cooldown = Random.Range(minCooldown, maxCooldown);
    }

    void Update()
    {
        if (Academy.IsInitialized) // Check if ML Agents is running
        {
            if (cooldown <= 0)
            {
                // Instantiate obstacle
                var obstacleGo = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
                spawnedObstacles.Add(obstacleGo); // Add to list of spawned obstacles

                cooldown = Random.Range(minCooldown, maxCooldown);
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    private void OnEnvironmentReset()
    {
        // Clean up spawned obstacles at the end of each episode
        foreach (var obstacle in spawnedObstacles)
        {
            Destroy(obstacle);
        }
        spawnedObstacles.Clear();
    }
}
