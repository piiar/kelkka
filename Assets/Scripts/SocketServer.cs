using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using LitJson;

public class NetworkAction {
    public string senderIp;
    public string senderSession;
    public string data;

    public NetworkAction(string _senderIp, string _senderSession, string _data)
    {
        senderIp = _senderIp;
        senderSession = _senderSession;
        data = _data;
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
        if (data == null || data["command"] == null) {
            return;
        }

        string userIp = Context.UserEndPoint.Address.ToString();
        EventManager.AddNetworkEvent(new NetworkAction(userIp, ID, e.Data));
        List<string> ids = this.Sessions.ActiveIDs.ToList();
    }

    protected override void OnError(ErrorEventArgs e) {
        Debug.Log("GameSocket OnError " + e.Message + " from " + Context.UserEndPoint);
    }

    protected override void OnClose(CloseEventArgs e) {
        Debug.Log("GameSocket OnClose " + Context.UserEndPoint);
        string userIp = Context.UserEndPoint.Address.ToString();
        EventManager.AddNetworkEvent(new NetworkAction(userIp, ID, "'command':'disconnect'"));
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

        wssv = new WebSocketServer("ws://" + ip + ":" + port);
        wssv.AddWebSocketService<GameSocketBehavior>("/");
        wssv.Start();
        Debug.Log("WebSocket server listening on ws://" + ip + ":" + port);
    }

    void OnApplicationQuit() {
        if (wssv != null) {
            Debug.Log("Quitting WebSocket server");
            wssv.Stop();
        }
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


    // not reliable!
    public List<string> GetActiveSessions() {
        WebSocketServiceHost host = null;
        wssv.WebSocketServices.TryGetServiceHost("/", out host);
        if (host != null)
        {
            List<string> list = host.Sessions.ActiveIDs.ToList();
            return list;
        }
        return new List<string>();
    }

}
