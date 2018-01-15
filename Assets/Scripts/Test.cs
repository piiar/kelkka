using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;

using System;
using WebSocketSharp;
using WebSocketSharp.Server;

public class NetworkManager : WebSocketBehavior
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
    }
}

public class Test : MonoBehaviour {

    public Image qrimage;
    public int port = 7777;

    private WebSocketServer wssv;

	// Use this for initialization
	void Start () {
        String ip = Network.player.ipAddress;
        Debug.Log("My IP address: " + ip);

        Texture2D kuva = this.generateQR("http://" + ip + "/game.html?port=" + port);
        this.qrimage.sprite = Sprite.Create(kuva, new Rect(0, 0, 256, 256), new Vector2());

        wssv = new WebSocketServer("ws://localhost:" + port);
        wssv.AddWebSocketService<NetworkManager>("/");
        wssv.Start();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Stopping");
            wssv.Stop();
        }
    }

    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    private Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }
}
