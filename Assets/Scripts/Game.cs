using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using LitJson;
using UnityEngine.SceneManagement;

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

    public PlayerEvent playerListChanged;

    private Dictionary<string, NetworkEnemyData> players;

    public static Game instance = null;

    private GameState state = GameState.Lobby;

    public HudController Hud { get; private set; }

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
        DontDestroyOnLoad(gameObject);
        players = new Dictionary<string, NetworkEnemyData>();
    }

    void Start()
    {
        Application.runInBackground = true;
        EventManager.StartListening("game", OnMessage);

        Hud = FindObjectOfType<HudController>();
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
            case "disconnect":
                this.OnDisconnect(action);
                break;
            case "transmitRobot":
                if (state != GameState.InGame) {
                    SendError(action.senderSession, "Invalid game state");
                }
                else {
                    this.OnAddRobot(action);
                }
                break;
            case "changeRobot":
                if (state != GameState.InGame)
                {
                    SendError(action.senderSession, "Invalid game state");
                }
                else
                {
                    this.OnChangeRobot(action);
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

    void OnDisconnect(NetworkAction action) {
        players.Remove(action.senderIp);
        playerListChanged.Invoke((GetEnemies()));
    }

    void OnAddRobot(NetworkAction action)
    {
        Debug.Log("OnAddRobot " + action);
        string name = players[action.senderIp].name;
        JsonData data = JsonMapper.ToObject(action.data);
        JsonData robotStructure = data["robot"];
        Debug.Log("RoboStruct : " + robotStructure.ToString());
        string hexCode = data["color"].ToString();
        EnemyManager.instance.CreatePlayer(action.senderIp, name, robotStructure, hexCode);
    }

    void OnChangeRobot(NetworkAction action)
    {
        Debug.Log("OnChangeRobot");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            EnemyController ctrl = enemy.GetComponent<EnemyController>();
            if (ctrl.userId == action.senderIp) {
                Debug.Log("Found robot " + action.senderIp);
                ctrl.ChangeRobot(action);
                break;
            }
        }
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
        }
        string isStarted = (state == GameState.InGame ? "true" : "false");
        string response = "{"
            + "'status':'OK',"
            + "'gameStarted':'" + isStarted + "',"
            + "'token':'" + players[ip].guid + "',"
            + "'name':'" + players[ip].name + "',"
            + "'points':" + players[ip].points
            + "}";
        SocketServer.instance.SendMessage(sessionId, response);
    }

    public void PrepareToStartGame() {
        string message = "{'command':'prepareStart'}";
        SocketServer.instance.SendMessage("broadcast", message);
    }

    public void StartGame() {
        state = GameState.InGame;
        string message = "{'command':'startGame'}";
        SocketServer.instance.SendMessage("broadcast", message);
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void StopGame() {
        state = GameState.Lobby;
        string message = "{'command':'stopGame'}";
        SocketServer.instance.SendMessage("broadcast", message);
        SceneManager.LoadSceneAsync("LobbyScene");
    }

    public List<NetworkEnemyData> GetEnemies() {
        //RefreshActiveEnemies();
        return new List<NetworkEnemyData>(players.Values);
    }

    public NetworkEnemyData GetEnemy(string id) {
        return players[id];
    }

    //private void RefreshActiveEnemies() {
    //    List<string> activeSessions = SocketServer.instance.GetActiveSessions();
    //    Debug.Log("active sessions " + activeSessions.Count);
    //}
}
