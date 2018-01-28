using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour {

    public GameObject playerList;

    public void Start() {
        Game.instance.playerListChanged.AddListener(PlayerListChanged);
    }

    public void ClickHelp()
    {

    }

    public void ClickBegin()
    {
        Game.instance.StartGame();
    }

    public void PlayerListChanged(List<NetworkEnemyData> newList) {
        Debug.Log("PlayerListChanged");

        if (playerList == null)
        {
            throw new UnityException("player list is missing!");
        }

        List<string> names = new List<string>();
        foreach (NetworkEnemyData d in newList) {
            names.Add(d.name);
        }
        PlayerList list = playerList.GetComponent<PlayerList>();
        if (list) {
            list.UpdatePlayerList(names);
        }
    }
 }
