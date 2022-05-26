using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    private Animator anim;
    public int speed; 

    private Ray ray;
    private RaycastHit hit;
    public LayerMask layerFloor;
    public bool isMoving;
    private Vector3 destination;

    private float h;
    private float v;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
        //En caso de querer moverse con nivel fácil o medio:
        /*h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");  
        */
        
        //Para el movimiento PointAndClick
        
        MovePointAndClick();


    }

    private void FixedUpdate()
    {
        
        //Para primer apartado de movimiento facil
        //Move();
        //Turning();
        
        //Para nivel intermedio de movimiento:
        
        /*MoveAllwaysForward();
        Turning();*/
        
        
        if (isMoving)
        {
            MovingPointAndClick();
        }
    }

    /// <summary>
    /// Método encargado de girar el personaje hacia donde está el ratón
    /// </summary>
    private void Turning()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerFloor))
        {
            
            TurnOnClick(hit.point - transform.position);


        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
    }

    /// <summary>
    /// Método que gira el personaje en la dirección donde se ha hecho click para el movimiento
    /// </summary>
    /// <param name="direction"></param>
    private void TurnOnClick(Vector3 direction)
    {
        direction.y = 0; //Ponemos la Y a 0 porque puede ser que intente ir hacia arriba/abajo y se buge
        Quaternion newRotation = Quaternion.LookRotation(direction);//Creamos un Quaternion a partir de la dirección
        rb.MoveRotation(newRotation);//Giramos el personaje en la dirección del Quaternion creado anteriormente
    }
    
    /// <summary>
    /// Mueve el personaje siempre la dirección forward de el mismo
    /// </summary>
    void MoveAllwaysForward()
    {

        float v = Input.GetAxis("Vertical");
        rb.MovePosition(transform.position + transform.forward * v *speed * Time.deltaTime );
        if (h != 0 || v != 0)
        {
            anim.SetBool("IsMoving", true);
        }        
        else
        {
            anim.SetBool("IsMoving", false);
        }

    }
    
    /// <summary>
    /// Método que lanza un raycast desde la cámara hasta un punto y choca con el suelo, en caso de hacer click se
    /// detecta donde se ha hecho click y lo guarda como la posición de destino en la variable destination, luego
    /// pone a true la varialbe isMoving.
    /// </summary>
    void MovePointAndClick()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = false;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerFloor))
            {
                isMoving = true;
                destination = hit.point;
                destination.y = transform.position.y;
                Vector3 direction = hit.point - transform.position;
                direction.y = 0;
                Quaternion newRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(newRotation);
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
    }

    /// <summary>
    /// Método encargado de mover a el personaje hacia el lugar guardado en la variable destination
    /// </summary>
    void MovingPointAndClick()
    {
        if ((destination - transform.position).magnitude <0.1f) //Si ha llegado al destino
        {
            anim.SetBool("IsMoving", false); //Desactivamos la animación de movimiento
            isMoving = false; 
            return;
        }

        rb.MovePosition(transform.position + (destination - transform.position).normalized * speed * Time.deltaTime);
        //Movemos el personaje hacia destination con una velocidad speed
        anim.SetBool("IsMoving", true); //Activamos animación de movimiento

    }
    
    
    /// <summary>
    /// Método que mueve el personaje a partir de unos Input.
    /// </summary>
    private void Move()
    {

        Vector3 movementVector = new Vector3(h, 0, v);

        rb.MovePosition(transform.position +movementVector.normalized * speed * Time.deltaTime );
        if (h != 0 || v != 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}
