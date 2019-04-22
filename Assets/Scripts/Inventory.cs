using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

	bool activation = false;
	GameObject Panel;
	public int[] tabSlots;

    void Start()
    {
        //GetComponent<Canvas>().enabled = false;
        Panel = transform.GetChild (0).gameObject;
        tabSlots = new int[Panel.transform.childCount];
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){

        	//inverse activation en true 
        	activation = !activation;

        	GetComponent<Canvas>().enabled = activation;
        }
    }

    public void UpdateTXT(int nrSlot, string txt){

    	Panel.transform.GetChild(nrSlot).GetChild(1).GetComponent<Text>().text = txt;

    }
}
