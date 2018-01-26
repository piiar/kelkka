﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour {

    public PlayerListItem playerListItemPrefab;
    private List<string> players;
	// Use this for initialization
	void Start () {
        players = new List<string>();
        for (int i = 0; i < 20; i++)
        {
            AddPlayer("sami" + i);
        }

        UpdatePlayerList();
	}

    public void AddPlayer(string playerName){
        players.Add(playerName);
    }


    void UpdatePlayerList(){
        //Remove all components.
        KillAllChild();

        //Recreate All based on List of players. 
        foreach (string playerName in players)
        {
            AddPlayerListItem(playerName);
        }
    }



    private void AddPlayerListItem(string playerName)
    {
        PlayerListItem nameLabel = Instantiate(playerListItemPrefab) as PlayerListItem;
        nameLabel.gameObject.SetActive(true);
        nameLabel.transform.SetParent(this.transform, false);
        nameLabel.SetName(playerName);
    }

    private void KillAllChild(){
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
