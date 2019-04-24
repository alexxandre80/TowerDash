using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{

    public GameObject minimap;
    private bool minimapActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m")){
        	minimapActive = !minimapActive;
        }

        if(minimapActive){
        	minimap.GetComponent<Camera>().enabled = true;
        }else{
        	minimap.GetComponent<Camera>().enabled = false;
        }
    }
}
