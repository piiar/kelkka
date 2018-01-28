using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointGenerator : MonoBehaviour {
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(RandomSpawnPoint());
        }      
    }
    public Vector3 RandomSpawnPoint()
    {
        GameObject[] areas = GameObject.FindGameObjectsWithTag("SpawnArea");
        return areas[Random.Range(0, areas.Length)].GetComponent<SpawnArea>().GenerateRandomPoint();
    }
}
