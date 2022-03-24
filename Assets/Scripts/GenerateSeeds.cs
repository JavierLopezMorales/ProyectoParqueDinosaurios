using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSeeds : MonoBehaviour
{
    public GameObject seed;
    private GameObject flower;
    public ParticleSystem explosion;
    public ParticleSystem muerte;
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
            float tiempo = Random.Range(3f, 6f);
            yield return new WaitForSeconds(tiempo);

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
                explosion.Play();
                s.GetComponent<Rigidbody>().AddForce(s.transform.up * speed);
            }
            

        }
    }

    IEnumerator killTree()
    {
        float tiempo = Random.Range(8f, 12f);
        yield return new WaitForSeconds(tiempo);
        Destroy(this.gameObject);
        Instantiate(muerte, this.transform.position, Quaternion.identity);
    }

}
