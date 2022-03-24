using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Germinate : MonoBehaviour
{

    public GameObject tree;
    // Start is called before the first frame update
    void Start()
    {
        float tiempo = Random.Range(3f, 6f);
        Invoke("germ", tiempo);
    }
    private void germ()
    {
        Vector3 pos = new Vector3(transform.position.x, 0f, transform.position.z);
        Instantiate(tree, pos, Quaternion.identity);
        
        Destroy(this.gameObject);
    }
}
