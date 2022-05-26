using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Sight : MonoBehaviour
{

    public float visionDistance; 
    public int visionAngle;
    private Transform player;

    public bool playerDetected;
    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    
    void Update()
    {
        Vision();
    }

    private void Vision()
    {
        ray.origin = transform.position + new Vector3(0,1,0); //para que el origen no sea a ras del suelo
        Vector3 direction = player.position - transform.position;
        ray.direction = direction ;
        if ((player.position - transform.position).magnitude < visionDistance) //En caso de que esté en la distancia de la visión
        {
            if (Vector3.Angle(transform.forward, direction) < visionAngle) //En caso de que esté en ángulo
            {

                if(Physics.Raycast(ray, out hit, Mathf.Infinity )) //En caso de que lo primero con lo que choque
                // el rayo que va hacia el jugador sea el propio jugador
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        playerDetected = true;
                    }
                }
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * visionDistance, Color.red);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; //Selecciamos que el gizmo que se va a pintar va a ser rojo.
        Gizmos.DrawWireSphere(transform.position, visionDistance); //Dibujamos una esfera que indica el rango de visión


        Gizmos.color = Color.green;
        Vector3 rightDir = Quaternion.Euler(0, visionAngle, 0) * transform.forward; //Para los ángulos, rotaciones etc usamos un Quaternion
        // en esta caso usamos Quaternion.Eulter(0,visionAngle,0) que significa que gira alrededor del eje y, pero esto no es tódo, necesitamos
        // multiplicar por vector a partir del cual vamos a girar, en este caso el vector forward

        Vector3 leftDir = Quaternion.Euler(0, -visionAngle, 0) * transform.forward; //Lo mismo pero ahora con el negativo visionAngle
        
        Gizmos.DrawRay(transform.position, rightDir*visionDistance); //Dibuamos un rayo, que parte de posición del gameObject
        // en la dirección calculado en rightDir y de longitud visionDistance, por eso multiplicamos el vector por visionDistance
        
        Gizmos.DrawRay(transform.position, leftDir*visionDistance);
    }
}
