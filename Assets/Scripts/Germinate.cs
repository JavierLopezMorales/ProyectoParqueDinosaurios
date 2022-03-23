using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Germinate : MonoBehaviour
{

    public GameObject tree;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("germ", 5.0f);
    }
    private void germ()
    {
        Vector3 pos = new Vector3(transform.position.x, 0f, transform.position.z);
        Instantiate(tree, pos, Quaternion.identity);
        
        Destroy(this.gameObject);
    }
}
