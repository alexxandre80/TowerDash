using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        
        //Recuperation de l'objet touché par la balle
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        if (!health != null)
        {
            //Application des dégats
            health.TakeDamage(10);
        }
        
        Destroy(gameObject);
    }
}
