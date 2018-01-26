using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerListItem : MonoBehaviour {
    public TextMeshProUGUI label; 

    public void SetName(string name){
        label.GetComponent<TextMeshProUGUI>().enabled = true;
        label.SetText(name);
    }

    public void OnClick(){
        
    }
}
