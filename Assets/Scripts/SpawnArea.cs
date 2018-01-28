using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour {
    public GameObject template;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Vector3 random = GenerateRandomPoint();
            Debug.Log("Generated Random Point : " + random);
            Instantiate(template, random, transform.rotation);

        }
    }

    public Vector3 GenerateRandomPoint()
    {
        Vector3 rndPosWithin;
        rndPosWithin = RandomPointInBox(this.transform.position, this.transform.localScale);
        return rndPosWithin;
    }

    private static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {

        return center + new Vector3(
           (Random.value - 0.5f) * size.x,
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }
}
