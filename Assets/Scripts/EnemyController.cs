using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour {

    private string userId = null;
    private string userName = null;
    private Vector3 movement = Vector3.zero;
    private CharacterController controller;

    public int health = 30;

    // Use this for initialization
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        //controller.Move(movement);
    }

    void OnDestroy() {
        if (userId != null) {
            EventManager.StopListening(userId, OnMessage);
        }
    }

    public void AddDamage(int damage) {
        health -= damage;

        if(health <= 0) {
            // TODO
            print("died");
        }
    }

    public void SetUserId(string id) {
        userId = id;
        EventManager.StartListening(userId, OnMessage);
    }

    public void SetName(string name) {
        userName = name;
    }

    void OnMessage(NetworkAction message) {
        Debug.Log("player " + userId + " got message " + message);
    }
}
