using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class PlayerData
{
    public string ip;
    public string sessionId;
    public string guid;
    public string name;
    public int points = 0;

    public PlayerData(string _ip, string _sessionId, string _guid, string _name) {
        ip = _ip;
        sessionId = _sessionId;
        guid = _guid;
        name = _name;
    }
}

public class Game : MonoBehaviour {

    public GameObject enemyPrefab;

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
            string name = System.Guid.NewGuid().ToString().Substring(uid.ToString().Length - 4);
            players.Add(ip, new PlayerData(ip, sessionId, uid.ToString(), name));
            createPlayer(ip);
        }
        string response = "{"
            + "'status':'OK',"
            + "'token':'" + players[ip].guid + "',"
            + "'name':'" + players[ip].name + "',"
            + "'points':" + players[ip].points
            + "}";
        SocketServer.instance.SendMessage(sessionId, response);
    }

    private void createPlayer(string ip) {
        if (enemyPrefab == null) {
            throw new UnityException("Enemy prefab is missing!");
        }
        GameObject obj = Instantiate(enemyPrefab) as GameObject;
        EnemyController player = obj.GetComponent<EnemyController>();
        player.SetUserId(ip);
    }

    public void PrepareToStartGame() {
        string message = "{'command':'prepareStart'}";
        SocketServer.instance.SendMessage("broadcast", message);
    }

    public void StartGame() {
        string message = "{'command':'startGame'}";
        SocketServer.instance.SendMessage("broadcast", message);
    }

    public void StopGame() {
        string message = "{'command':'stopGame'}";
        SocketServer.instance.SendMessage("broadcast", message);
    }
}
