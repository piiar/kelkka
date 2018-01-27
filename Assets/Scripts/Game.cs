using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using LitJson;

enum GameState {
    Lobby,
    InGame,
    Finish
};

[System.Serializable]
public class PlayerEvent : UnityEvent<List<NetworkEnemyData>>
{
}

public class Game : MonoBehaviour {

    public GameObject enemyPrefab;
    public PlayerEvent playerListChanged;

    private Dictionary<string, NetworkEnemyData> players;

    public static Game instance = null;

    private GameState state = GameState.Lobby;

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
        Application.runInBackground = true;
        players = new Dictionary<string, NetworkEnemyData>();
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
        JsonData data = JsonMapper.ToObject(action.data);

        string command = data["command"].ToString();
        switch (command) {
            case "joinGame":
                this.OnJoin(action);
                break;
            case "transmitRobot":
                if (state != GameState.InGame) {
                    SendError(action.senderSession, "Invalid game state");
                }
                else {
                    this.OnAddRobot(action);
                }
                break;
            default:
                Debug.Log("Unrecognized command: " + command);
                break;
        }
    }

    void SendError(string session, string message) {
        string msg = "{'status':'error','message':'" + message + "'}";
        SocketServer.instance.SendMessage(session, msg);
    }

    void OnJoin(NetworkAction action) {
        Debug.Log("OnJoin data " + action.data);
        string ip = action.senderIp;
        string session = action.senderSession;
        RegisterPlayer(ip, session);
        playerListChanged.Invoke(GetEnemies());
    }

    void OnAddRobot(NetworkAction action) {
        Debug.Log("OnAddRobot " + action);
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
            players.Add(ip, new NetworkEnemyData(ip, sessionId, uid.ToString(), name));
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
        state = GameState.InGame;
        string message = "{'command':'startGame'}";
        SocketServer.instance.SendMessage("broadcast", message);
    }

    public void StopGame() {
        state = GameState.Lobby;
        string message = "{'command':'stopGame'}";
        SocketServer.instance.SendMessage("broadcast", message);
    }

    public List<NetworkEnemyData> GetEnemies() {
        return new List<NetworkEnemyData>(players.Values);
    }
}
