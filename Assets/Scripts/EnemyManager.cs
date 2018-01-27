using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyPrefab;

    public static EnemyManager instance = null;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        Debug.Log("EnemyManager start");
    }

    // Update is called once per frame
    void Update () {
    }

    public void CreatePlayer(string ip, string name, JsonData robotStructure)
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

        int top = Int32.Parse(robotStructure["TOP"].ToString());
        int bottom = Int32.Parse(robotStructure["BOTTOM"].ToString());
        int left = Int32.Parse(robotStructure["LEFT"].ToString());
        int right = Int32.Parse(robotStructure["RIGHT"].ToString());
        Debug.Log("top " + top + " bottom " + bottom + " left " + left + " right " + right);
        player.InitEquipment(top, bottom, left, right);
    }

}
