using UnityEngine;
using UnityEngine.UI;

using ZXing;
using ZXing.QrCode;

public class Test : MonoBehaviour {

    public Image qrimage;
    public int port = 7777;

	// Use this for initialization
	void Start () {
        string ip = Network.player.ipAddress;
        Texture2D kuva = this.generateQR("http://" + ip + "/?wsHost=" + ip + "&" + "wsPort=" + port);
        this.qrimage.sprite = Sprite.Create(kuva, new Rect(0, 0, 256, 256), new Vector2());
	}
	
	// Update is called once per frame
    void Update()
    {
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
