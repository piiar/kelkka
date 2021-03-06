﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCase : MonoBehaviour {

    private int money = 0;

    public void InitMoneyCase(int amount)
    {
        money = amount;
    }

    public void MakeTransactionTo(string playerId, int amount)
    {
        NetworkEnemyData player = Game.instance.GetEnemy(playerId);
        if (player != null) {
            string msg = "{'money':" + amount + "}";
            SocketServer.instance.SendMessage(player.sessionId, msg);
            MoneyManager.instance.SpendMoney(amount);
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Enemy")) {
            EnemyController enemy = collider.transform.GetComponent<EnemyController>();
            MakeTransactionTo(enemy.userId, money);
            Destroy(gameObject);
        }
    }
}
