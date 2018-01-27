using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandConsole : MonoBehaviour {

    public int counter;

    public void Use()
    {

        if (counter > 1000)
        {
            Debug.Log("Command Console hacked!");
        }
        counter++;
    }

    public void ResetUse()
    {
        Debug.Log("Resetted Command Console! Counter Was : " + counter);
        counter = 0;
    }
}
