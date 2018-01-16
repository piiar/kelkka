using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public class GameSocketBehavior : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        this.OriginValidator = (val) =>
        {
            return true;
        };

        Debug.Log("Got message " + e.Data);
        var msg = e.Data == "joinGame"
                  ? "Joined the game"
                  : "Unrecognized message";

        Send(msg);
        EventManager.TriggerEvent("player1", "join");
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
