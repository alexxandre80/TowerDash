using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Deplacement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 20f;
    public float drag = 0.5f;
    public float terminalRotationSpeed = 25.0f;
    
    public bool Canjump = false;
    public Joystick moveJoystick;

    private Rigidbody controller;
    private Transform camTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Rigidbody>();
        controller.maxAngularVelocity = terminalRotationSpeed;
        controller.drag = drag;

        camTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        //Deplacement du Joueur 
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        if (direction.magnitude > 1)
            direction.Normalize();

        if (moveJoystick.InputDirection != Vector3.zero)
        {
            direction = moveJoystick.InputDirection;
        }
        
        //Faire pivoter notre vecteur de direction avec la caméra
        Vector3 rotatedDir = camTransform.TransformDirection(direction);
        rotatedDir = new Vector3(rotatedDir.x,0,rotatedDir.z);
        rotatedDir = rotatedDir.normalized * direction.magnitude;
        
        controller.AddForce(rotatedDir * moveSpeed);
    }

}
