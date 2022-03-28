using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class automata3d : Agent
{
    public GameObject Comida;
    public int velocidad;
    private Vector3 posOriginal;
    // Start is called before the first frame update
    void Start()
    {
        posOriginal = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = posOriginal;

        float posObjX = Random.Range(-5.5f, 5.5f);
        if (posObjX > -0.5f && posObjX < 0.0f)
        {
            posObjX = posObjX - 1.5f;
        }
        else if (posObjX < 0.5f && posObjX > 0.0f)
        {
            posObjX = posObjX + 1.5f;
        }

        float posObjZ = Random.Range(-5.5f, 5.5f);
        if (posObjZ > -0.5f && posObjZ < 0.0f)
        {
            posObjZ = posObjZ - 1.5f;
        }
        else if (posObjZ < 0.5f && posObjZ > 0.0f)
        {
            posObjZ = posObjZ + 1.5f;
        }

        Comida.transform.position = new Vector3(posOriginal.x + posObjX, 1, posOriginal.z + posObjZ);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int Mover = actions.DiscreteActions[0];
        int Girar = actions.DiscreteActions[1];

        int m = 0;
        int g = 0;

        if (Mover == 1)
        {
            m = 1;
        }
        else if (Mover == 2)
        {
            m = -1;
        }

        if (Girar == 1)
        {
            g = 1;
        }
        else if (Girar == 2)
        {
            g = -1;
        }

        if (m != 0)
        {
            transform.position = transform.position + transform.forward * m * velocidad * Time.deltaTime;
        }
        if (g != 0)
        {
            transform.Rotate(Vector3.up, g);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstaculo")
        {
            AddReward(-1.0f);
        }
        if (collision.gameObject.tag == "triceratops")
        {
            AddReward(1.0f);
        }
        EndEpisode();
    }
}
