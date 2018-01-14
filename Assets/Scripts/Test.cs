using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZXing;
using ZXing.QrCode;
using Quobject.SocketIoClientDotNet.Client;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public Image qrimage;

	// Use this for initialization
	void Start () {
        //QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
        //UnityQRCode qrCode = new UnityQRCode(qrCodeData);
        //Texture2D qrCodeAsTexture2D = qrCode.GetGraphic(20);

        Texture2D kuva = this.generateQR("http://www.octo3.fi");
        this.qrimage.sprite = Sprite.Create(kuva, new Rect(0, 0, 256, 256), new Vector2());

        var socket = IO.Socket("http://localhost:3000");
        socket.On(Socket.EVENT_CONNECT, () =>
        {
            socket.Emit("hi");
        });
        socket.On("hi", (data) =>
        {
            Debug.Log(data);
            socket.Disconnect();
        });
	}
	
	// Update is called once per frame
	void Update () {
		
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
