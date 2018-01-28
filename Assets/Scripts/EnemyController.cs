using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour {

    private string userId = null;
    private string userName = null;
    private Vector3 movement = Vector3.zero;
    private CharacterController controller;
    private int health = 30;

    public int speedClass; //Bottom -- 20*modifier 
    public Weapon weapon_left;
    public Weapon weapon_right;
    private AiMode aiMode;

    public ParticleSystem sparkEmitter;

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

    public void AddDamage(int damage, Vector3 direction) {
        health -= damage;

        //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        //sparkEmitter.transform.rotation = targetRotation;
        //sparkEmitter.Emit(10);

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

    public void InitEquipment(int top, int bottom, int left, int right)
    {
        weapon_left = weaponFor(left);
        weapon_right = weaponFor(right);
        aiMode = aiFor(top);
        //movement

        if (aiMode == AiMode.Aggressive || aiMode == AiMode.Flanking)
        {
            GetComponent<WatcherRobotMovement>().SetMovementTarget(GameObject.FindGameObjectWithTag("Player").transform);
        }
        else if (aiMode == AiMode.Objective)
        {
            GetComponent<WatcherRobotMovement>().SetMovementTarget(GameObject.FindGameObjectWithTag("Player").transform);
        }
        
    }

    private Weapon weaponFor(int i)
    {
        switch (i)
        {
            case 0:
                return new Weapon("Shield", 0);
            case 1:
                return new Weapon("FlameThrower", 10);
            default:
                return new Weapon("Zap", 20);
        }
    }

    private AiMode aiFor(int i)
    {
        switch (i)
        {
            case 0:
                return AiMode.Aggressive; 
            case 1:
                return AiMode.Flanking;
            default:
                return AiMode.Objective;
        }
    }

    public AiMode GetAIMode()
    {
        return aiMode;
    }

    public float MoveSpeed()
    {
        switch (speedClass)
        {
            case 0:
                return 1.0f;
            case 1:
                return 1.3f;
            default:
                return 1.5f;
        }
    }
}
