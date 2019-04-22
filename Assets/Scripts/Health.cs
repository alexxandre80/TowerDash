using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public const int maxHealth = 100;
    public int currentHealth = maxHealth;

    public RectTransform healthBar;
    
    //Prendre des dégats
    public void TakeDamage(int amount)
    {
        //On baisse la vie actuelle du joueur selon les degats
        currentHealth -= amount;
        
        //Si la vie actuelle atteint 0 le presonnage meurt
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
            Debug.Log("Mort");
        }
        healthBar.sizeDelta = new Vector2(currentHealth,healthBar.sizeDelta.y);
    }

    private void OnGUI()
    {
        //Afficher la vie du joueur sur son interface
     //   GUI.Label(new Rect(10,10,100,20 ),"Vie : " + healthBar.sizeDelta.x + "/" + maxHealth) ;
    }
}
