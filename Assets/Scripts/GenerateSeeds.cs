using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSeeds : MonoBehaviour
{
    public GameObject seed;
    private GameObject flower;
    // Start is called before the first frame update
    void Start()
    {
        //flower = transform.GetChild(7).gameObject;
        StartCoroutine("generateSeed");
        
        StartCoroutine("killTree");
    }

    IEnumerator generateSeed()
    {
        while(true)
        {
            yield return new WaitForSeconds(5.0f);

            if (GameObject.FindGameObjectsWithTag("comida").Length < 50)
            {
                Vector3 pos = this.gameObject.transform.position;
                pos.y = 3f;
                GameObject s = Instantiate(seed, pos, Quaternion.identity);

                float speed = 300;
                float rX = Random.Range(-60f, 60f);
                float rZ = Random.Range(-60f, 60f);
                Vector3 rFinal = new Vector3(rX, 0, rZ);

                s.transform.rotation = Quaternion.Euler(rFinal);

                s.GetComponent<Rigidbody>().AddForce(s.transform.up * speed);
            }
            

        }
    }

    IEnumerator killTree()
    {
        yield return new WaitForSeconds(11.0f);
        Destroy(this.gameObject);
    }

}
