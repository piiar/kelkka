using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

    private string userId = null;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

    void OnDestroy()
    {
        if (userId != null) {
            EventManager.StopListening(userId, OnMessage);
        }
    }

    public void SetUserId(string id) {
        userId = id;
        EventManager.StartListening(userId, OnMessage);
    }

    void OnMessage(string message) {
        Debug.Log("player " + userId + " got message " + message);
    }
}
