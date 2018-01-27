using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour {

    public PlayerListItem playerListItemPrefab;
    private List<string> players;
	// Use this for initialization
	void Start () {
        players = new List<string>();
        UpdatePlayerList(players);
	}

    public void AddPlayer(string playerName){
        players.Add(playerName);
    }

    public void UpdatePlayerList(List<string> _players) {
        players = _players;
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
        if (!playerListItemPrefab) {
            throw new UnityException("Player list item prefab missing!");
        }
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
