using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSeed : MonoBehaviour
{
 public GameObject seed;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i< 10; i++) 
        { 

        float x = Random.Range(-45.0F, 45.0F);
        float z = Random.Range(-45.0F, 45.0F);

        Vector3 position = new Vector3(x, 5, z);

        Instantiate(seed, position, Quaternion.identity);
        }
    }

}
