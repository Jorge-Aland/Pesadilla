using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGhost : MonoBehaviour
{
    
    private Sight sight;
    public Transform[] positions;//array de posiciones, que son los empty objects que hay en la escena
    NavMeshAgent agent;
    Vector3 posToGo;//variable que me va a guardar la posición a la que quiero que vaya el enemigo
    int pos = 0;
    private bool goBack = false;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sight = GetComponent<Sight>();
        posToGo = positions[0].position;
    }
    // Update is called once per frame
    void Update()
    {
        Patrol();
        
        if (sight.playerDetected)
        {
            StartCoroutine(GameManager.SharedInstance.EndGame());

        }
    }

    void Patrol()
    {
        float distance = Vector3.Distance(transform.position, posToGo);
        if (distance < 0.1f)
        {
            //¿Estoy en la última casilla del array?
            if (pos == positions.Length - 1) goBack = true; //En caso de que si pongo goBack en true, para que si 
            //  recorrido es 1-2-3 haga 3-2-1
            if (goBack)
            {
                pos--;
            }
            else
            {
                pos++;
            }

            posToGo = positions[pos].position;
            if (pos == 0) goBack = false;
        }
        agent.SetDestination(posToGo);
    }
    
}
