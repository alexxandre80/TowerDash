using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabConsumable : MonoBehaviour
{

	Inventory Inventory_script;

    // Start is called before the first frame update
    void Start()
    {
        Inventory_script = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    // dès qu'il y a un collider
    void OnTriggerEnter(Collider col)
    {
        switch(col.gameObject.tag){

        	//en fonction des comsommables et de leur tags cela met a jour le nb d'item et detruit l'objet 
        	//"case" a faire pour chaque consommable
        	case "Slot": //apple
        	Inventory_script.tabSlots[0] +=1;
        	Inventory_script.UpdateTXT(0,Inventory_script.tabSlots[0].ToString());
        	Destroy (col.gameObject);
        	break;

        }
    }
}
