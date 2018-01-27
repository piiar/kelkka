using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandConsoleTrigger : MonoBehaviour {
    public CommandConsole commandConsoleParent;
    private bool playerClose;

    void Start()
    {
        playerClose = false;
    }

    void Update()
    {
        if (playerClose)
        {
            commandConsoleParent.Use();
        }
        else
        {
            commandConsoleParent.ResetUse();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerClose = false;
        }
    }
}
