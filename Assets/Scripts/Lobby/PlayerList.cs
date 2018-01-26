using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour {

    public PlayerListItem playerListItemPrefab;
    private List<string> players;
	// Use this for initialization
	void Start () {

        for (int i = 0; i < 20; i++)
        {
            AddPlayer("sami" + i);
        }

	}

    public void AddPlayer(string playerName){
        PlayerListItem nameLabel = Instantiate(playerListItemPrefab) as PlayerListItem;
        nameLabel.gameObject.SetActive(true);
        nameLabel.transform.SetParent(this.transform, false);
        nameLabel.SetName(playerName);
    }

}
