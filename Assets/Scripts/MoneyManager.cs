using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class MoneyManager : MonoBehaviour
{
    public int moneyTotal { get; private set; }

    public GameObject suitcasePrefab;

    public static MoneyManager instance = null;

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
    void Start()
    {
        Debug.Log("MoneyManager start");
        if (suitcasePrefab == null)
        {
            throw new UnityException("Suitcase prefab is missing!");
        }
        moneyTotal = 0;
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("suitcase");
        foreach (GameObject obj in spawns) {
            CreateSuitcase(obj.transform.position);
        }
        if (Game.instance.Hud) {
            Game.instance.Hud.SetMoney(moneyTotal);
        }
    }

    public void CreateSuitcase(Vector3 spawnPosition)
    {
        GameObject obj = Instantiate(suitcasePrefab) as GameObject;
        MoneyCase suitcase = obj.GetComponent<MoneyCase>();
        suitcase.transform.position = spawnPosition;
        suitcase.InitMoneyCase(250);
        moneyTotal += 250;
    }

    public void SpendMoney(int amount) {
        moneyTotal -= amount;
        Game.instance.Hud.SetMoney(moneyTotal);
    }

}
