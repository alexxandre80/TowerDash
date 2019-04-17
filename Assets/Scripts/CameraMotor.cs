using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;

    private Vector3 desiredPosition;
    private Vector3 offset;
    
    private Vector2 touchPosition;
    private float swipeResistance = 200.0f;
    
    //eloignement du personnage
    private float distance = 10.0f;

    //Hauteur camera par rapport au personnage
    private float yOffset = 3.5f;

    private float smoothSpeed = 7.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0,yOffset,-1f * distance);
    }

    // Update is called once per frame
    void Update()
    {
        //Faire tourner la camera autour du personnage avec Q et D
        //if (Input.GetKeyDown(KeyCode.Q))
        //    SlideCamera(true);
        //else if (Input.GetKeyDown(KeyCode.D))
        //    SlideCamera(false);
        
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            touchPosition = Input.mousePosition;
        }
        
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            float swipeForce = touchPosition.x - Input.mousePosition.x;
            if (Mathf.Abs(swipeForce) > swipeResistance)
            {
                if (swipeForce < 0)
                    SlideCamera(true);
                else
                    SlideCamera(false);
            }
            
        }

        desiredPosition = lookAt.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(lookAt.position + Vector3.up);
    }
    
    public void SlideCamera(bool left)
    {
        if (left)
            offset = Quaternion.Euler(0, 90, 0) * offset;
        else
            offset = Quaternion.Euler(0, -90, 0) * offset;
     
    }
}
