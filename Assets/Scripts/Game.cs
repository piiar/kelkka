using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public GameObject enemyPrefab;

    private Dictionary<string, string> players;

    public static Game instance = null;

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

    void Start()
    {
        players = new Dictionary<string, string>();
        EventManager.StartListening("joinGame", OnJoin);
    }

    void OnDisable()
    {
        EventManager.StopListening("joinGame", OnJoin);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnJoin(string userId) {
        // TODO sessionId
        RegisterPlayer(userId, userId);
    }

    void RegisterPlayer(string userId, string sessionId) {
        Debug.Log("registerPlayer " + userId);
        if (players.ContainsKey(userId)) {
            // old user reconnected, update sessionId
            players[userId] = sessionId;
        } else {
            // new user
            players.Add(userId, sessionId);
            createPlayer(userId);
        }
    }

    private void createPlayer(string userId) {
        Debug.Log("createPlayer " + userId);
        GameObject obj = Instantiate(enemyPrefab) as GameObject;
        EnemyController player = obj.GetComponent<EnemyController>();
        player.SetUserId(userId);
    }
}
