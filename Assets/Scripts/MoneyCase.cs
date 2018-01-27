using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCase : MonoBehaviour {

    private int startMoneyTotal = 2000;

    public void InitMoneyCase()
    {
        startMoneyTotal = 2000;
    }

    public void MakeTransactionTo(string playerId, int amount)
    {
        if (startMoneyTotal - amount < 0) {
            Debug.Log("Not enough money left");
            return;
        }
        NetworkEnemyData player = Game.instance.GetEnemy(playerId);
        if (player != null) {
            string msg = "{'money':" + amount + "}";
            SocketServer.instance.SendMessage(player.sessionId, msg);
            startMoneyTotal -= amount;
        }
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Enemy")) {
            // TODO: do something?
            EnemyController enemy = collider.transform.GetComponent<EnemyController>();
            //string id = enemy.userId;
            Destroy(gameObject);
        }
    }
}
