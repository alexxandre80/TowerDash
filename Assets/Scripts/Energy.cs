using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{

	//tuto avec faim 
	public Text txtEnergy;
	public int energy = 50;

    // Update is called once per frame
    void Update()
    {
        txtEnergy.text = "Energy " + energy + "%";

    }
}
