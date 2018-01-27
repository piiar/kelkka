using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugViewer : MonoBehaviour {
    private static Dictionary<string, string> data = new Dictionary<string, string>();

    public static void Debug(string key, object value) {
        data[key] = value.ToString();
    }

    void OnGUI() {
        int y = 10;
        foreach (var kvp in data) {
            string s = kvp.Key + ": " + kvp.Value;
            GUI.Label(new Rect(10, y, 1000, 20), s);
            y += 20;
        }
    }
}
