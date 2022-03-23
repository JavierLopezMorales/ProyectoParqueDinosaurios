using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemigoController : MonoBehaviour
{
    private GameObject[] ojos;
    private GameObject detectorComida;
    private GameObject cuerpo;
    private NavMeshAgent nv;
    private Vector3 PosicionObjetivo;
    private Vector3 PosicionPatrulla;
    private bool encontrado;

    private int vida = 3;

    private void Awake()
    {
        ojos = new GameObject[27];
        ojos[0] = transform.GetChild(0).transform.GetChild(0).transform.gameObject;
        ojos[1] = transform.GetChild(0).transform.GetChild(1).transform.gameObject;
        ojos[2] = transform.GetChild(0).transform.GetChild(2).transform.gameObject;
        ojos[3] = transform.GetChild(0).transform.GetChild(3).transform.gameObject;
        ojos[4] = transform.GetChild(0).transform.GetChild(4).transform.gameObject;
        ojos[5] = transform.GetChild(0).transform.GetChild(5).transform.gameObject;
        ojos[6] = transform.GetChild(0).transform.GetChild(6).transform.gameObject;
        ojos[7] = transform.GetChild(0).transform.GetChild(7).transform.gameObject;
        ojos[8] = transform.GetChild(0).transform.GetChild(8).transform.gameObject;

        ojos[9] = transform.GetChild(0).transform.GetChild(9).transform.gameObject;
        ojos[10] = transform.GetChild(0).transform.GetChild(10).transform.gameObject;
        ojos[11] = transform.GetChild(0).transform.GetChild(11).transform.gameObject;
        ojos[12] = transform.GetChild(0).transform.GetChild(12).transform.gameObject;
        ojos[13] = transform.GetChild(0).transform.GetChild(13).transform.gameObject;
        ojos[14] = transform.GetChild(0).transform.GetChild(14).transform.gameObject;
        ojos[15] = transform.GetChild(0).transform.GetChild(15).transform.gameObject;
        ojos[16] = transform.GetChild(0).transform.GetChild(16).transform.gameObject;
        ojos[17] = transform.GetChild(0).transform.GetChild(17).transform.gameObject;

        ojos[18] = transform.GetChild(0).transform.GetChild(18).transform.gameObject;
        ojos[19] = transform.GetChild(0).transform.GetChild(19).transform.gameObject;
        ojos[20] = transform.GetChild(0).transform.GetChild(20).transform.gameObject;
        ojos[21] = transform.GetChild(0).transform.GetChild(21).transform.gameObject;
        ojos[22] = transform.GetChild(0).transform.GetChild(22).transform.gameObject;
        ojos[23] = transform.GetChild(0).transform.GetChild(23).transform.gameObject;
        ojos[24] = transform.GetChild(0).transform.GetChild(24).transform.gameObject;
        ojos[25] = transform.GetChild(0).transform.GetChild(25).transform.gameObject;
        ojos[26] = transform.GetChild(0).transform.GetChild(26).transform.gameObject;

        detectorComida = transform.GetChild(0).transform.GetChild(27).transform.gameObject;

        nv = GetComponent<NavMeshAgent>();

    }

    // Start is called before the first frame update
    void Start()
    {
        PosicionObjetivo = Vector3.zero;
        PosicionPatrulla = Vector3.zero;
        encontrado = false;
        StartCoroutine("patrullar");
    }


    IEnumerator patrullar()
    {

        destinoPatrulla();
        nv.SetDestination(PosicionPatrulla);

        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (encontrado==true)
            {
                nv.ResetPath();
                StopAllCoroutines();
                StartCoroutine("acercarse");
                break;
            }

            if (Vector3.Distance(transform.position, PosicionPatrulla) < 2.0f)
            {
                nv.ResetPath();
                StopAllCoroutines();
                StartCoroutine("buscar");
                break;
            }
        }


    }

    IEnumerator acercarse()
    {

        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (encontrado == true)
            {
                nv.SetDestination(PosicionObjetivo);

                RaycastHit hit;

                if (Physics.Raycast(detectorComida.transform.position, detectorComida.transform.forward, out hit, 1))
                {
                    Debug.DrawRay(detectorComida.transform.position, detectorComida.transform.forward * hit.distance, Color.green);
                    nv.ResetPath();
                    StopAllCoroutines();
                    StartCoroutine("comer", hit.transform.gameObject);
                    break;
                }
            }
            else
            {
                nv.ResetPath();
                StopAllCoroutines();
                StartCoroutine("buscar");
                break;
            }
        }
    }

    IEnumerator comer(GameObject objetivo)
    {
        yield return new WaitForSeconds(1);
        Destroy(objetivo);
        StopAllCoroutines();
        StartCoroutine("buscar");
    }

        //TODO: Al ver enemigo huir

        IEnumerator buscar()
    {

        float rotacion = 0;
        
        while(rotacion<360)
        {
            yield return new WaitForFixedUpdate();

            transform.Rotate(Vector3.up, 1);

            rotacion++;

            if(encontrado == true)
            {
                break;
            }
        }

        if(encontrado == true)
        {
            nv.ResetPath();
            StopAllCoroutines();
            StartCoroutine("acercarse");
        }
        else
        {
            nv.ResetPath();
            StopAllCoroutines();
            StartCoroutine("patrullar");
        }
    }

    private void FixedUpdate()
    {
        mirar();
    }


    private void mirar()
    {
        encontrado = false;

        for (int i=0;i<27;i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(ojos[i].transform.position, ojos[i].transform.forward, out hit, 15))
            {
                if(hit.transform.gameObject.tag == "comida")
                {
                    PosicionObjetivo = hit.point;
                    encontrado = true;
                    Debug.DrawRay(ojos[i].transform.position, ojos[i].transform.forward * hit.distance, Color.yellow);
                }else if (hit.transform.gameObject.tag == "carnotauro") 
                {
                    Debug.DrawRay(ojos[i].transform.position, ojos[i].transform.forward * hit.distance, Color.red);
                }
                else
                {
                    Debug.DrawRay(ojos[i].transform.position, ojos[i].transform.forward * hit.distance, Color.white);
                }
            }
            else
            {
                Debug.DrawRay(ojos[i].transform.position, ojos[i].transform.forward * 15, Color.white);
            }
        }

        /*
         TODO: DETECTOR DE COMIDA
         */
    }

    private void destinoPatrulla()
    {
        int CoorX = Random.Range(-50, 50);
        int CoorZ = Random.Range(-50, 50);

        Vector3 InicioRayo = new Vector3(CoorX, 6, CoorZ);

        if (Physics.Raycast(InicioRayo, Vector3.down, 10))
        {
            PosicionPatrulla = new Vector3(CoorX, 0, CoorZ);
        }

    }

    public void quitarVida()
    {
        vida--;

        if (vida == 0)
        {
            nv.ResetPath();
            nv = null;
            Destroy(this.gameObject);
        }
    }
}
