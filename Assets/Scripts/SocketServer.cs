using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using LitJson;

public class NetworkAction {
    public string target;
    public string command;
    public string[] args;

    public NetworkAction(string tgt, string cmd, string[] a1)
    {
        target = tgt;
        command = cmd;
        args = a1;
    }
}

public class GameSocketBehavior : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        this.OriginValidator = (val) =>
        {
            return true;
        };

        Debug.Log("Got message " + e.Data + " from " + Context.UserEndPoint + ", sessionId=" + ID);

        JsonData data = JsonMapper.ToObject(e.Data);
        if (data["command"] == null) {
            return;
        }
        //string param = data["param"].ToString();

        string userId = Context.UserEndPoint.Address.ToString();

        switch (data["command"].ToString()) {
            case "joinGame":
                Debug.Log("joinGame");
                string[] args = { userId, ID };
                EventManager.AddNetworkEvent(new NetworkAction("game", "joinGame", args));
                break;
            case "left":
                EventManager.AddNetworkEvent(new NetworkAction(userId, "left", null));
                break;
            case "right":
                EventManager.AddNetworkEvent(new NetworkAction(userId, "right", null));
                break;
        }
        //List<string> ids = this.Sessions.ActiveIDs.ToList();
    }
}

public class SocketServer : MonoBehaviour
{

    public int port = 7777;

    private WebSocketServer wssv;

    public static SocketServer instance = null;

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
    }

    // Use this for initialization
    void Start()
    {
        string ip = Network.player.ipAddress;
        Debug.Log("My IP address: " + ip);
        wssv = new WebSocketServer("ws://192.168.0.105:" + port);
        wssv.AddWebSocketService<GameSocketBehavior>("/");
        wssv.Start();
    }

    void OnApplicationQuit() {
        Debug.Log("Quitting socket server");
        wssv.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Stopping");
            wssv.Stop();
        }
    }

    public void SendMessage(string target, string msg) {
        Debug.Log("Sending " + msg + " to session " + target);
        WebSocketServiceHost host = null;
        wssv.WebSocketServices.TryGetServiceHost("/", out host);
        if (host != null) {
            if (target == "broadcast")
            {
                host.Sessions.BroadcastAsync(msg, null);
            }
            else
            {
                IWebSocketSession session;
                if (!host.Sessions.TryGetSession(target, out session))
                {
                    Debug.Log("The session could not be found.");
                    return;
                }
                host.Sessions.SendToAsync(msg, target, null);
            }
        }
    }
}
