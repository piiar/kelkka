using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string ip;
    public string sessionId;
    public string guid;

    public PlayerData(string _ip, string _sessionId, string _guid) {
        ip = _ip;
        sessionId = _sessionId;
        guid = _guid;
    }
}

public class Game : MonoBehaviour {

    public GameObject playerPrefab;

    private Dictionary<string, PlayerData> players;

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
        players = new Dictionary<string, PlayerData>();
        EventManager.StartListening("game", OnMessage);
    }

    void OnDisable()
    {
        EventManager.StopListening("game", OnMessage);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMessage(NetworkAction action) {
        if (action.command == "joinGame") {
            this.OnJoin(action);
        }
    }

    void OnJoin(NetworkAction data) {
        RegisterPlayer(data.args[0], data.args[1]);
    }

    void RegisterPlayer(string ip, string sessionId) {
        Debug.Log("registerPlayer " + ip);
        if (players.ContainsKey(ip)) {
            // old user reconnected, update sessionId
            players[ip].sessionId = sessionId;
        } else {
            // new user
            System.Guid uid = System.Guid.NewGuid();
            players.Add(ip, new PlayerData(ip, sessionId, uid.ToString()));
            createPlayer(ip);
        }
        SocketServer.instance.SendMessage(sessionId, "OK " + players[ip].guid);
    }

    private void createPlayer(string ip) {
        Debug.Log("createPlayer " + ip);
        GameObject obj = Instantiate(playerPrefab) as GameObject;
        PlayerController player = obj.GetComponent<PlayerController>();
        player.SetUserId(ip);
    }
}
