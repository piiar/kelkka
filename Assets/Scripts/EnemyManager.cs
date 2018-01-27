using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
        Debug.Log("EnemyManager start");
        List<NetworkEnemyData> robots = Game.instance.GetEnemies();
        foreach (NetworkEnemyData enemy in robots) {
            createPlayer(enemy.ip, enemy.name);
        }
	}

	// Update is called once per frame
	void Update () {
	}

    private void createPlayer(string ip, string name)
    {
        if (enemyPrefab == null)
        {
            throw new UnityException("Enemy prefab is missing!");
        }
        Debug.Log("creating player " + name);
        GameObject obj = Instantiate(enemyPrefab) as GameObject;
        EnemyController player = obj.GetComponent<EnemyController>();
        player.SetUserId(ip);
        player.SetName(name);
    }

}
