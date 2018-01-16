using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public class NetworkAction {
    public string command;
    public string arg1;
    //public string arg2;

    public NetworkAction(string cmd, string a1)
    {
        command = cmd;
        arg1 = a1;
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
        string userId = Context.UserEndPoint.Address.ToString();
        switch (e.Data) {
            case "joinGame":
                //TODO sessionId
                EventManager.AddNetworkEvent(new NetworkAction("joinGame", userId));
                Send("OK");
                break;
            case "left":
                EventManager.AddNetworkEvent(new NetworkAction(userId, "left"));
                break;
            case "right":
                EventManager.AddNetworkEvent(new NetworkAction(userId, "right"));
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

        wssv = new WebSocketServer("ws://localhost:" + port);
        wssv.AddWebSocketService<GameSocketBehavior>("/");
        wssv.Start();
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
}
