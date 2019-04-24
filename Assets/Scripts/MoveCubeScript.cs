using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class MoveCubeScript : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private float moveSpeed;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            targetTransform.position +=
                Vector3.back * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            targetTransform.position +=
                Vector3.forward * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            targetTransform.position +=
                Vector3.left * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            targetTransform.position +=
                Vector3.right * Time.deltaTime * moveSpeed;
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            Fire();
        }
    }

    void Fire()
    {
        //Creation de la balle à partir du prefab "Bullet"
        var bullet = (GameObject) Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        
        //Ajout de velocite a la ball
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
        
        //Destruction de la balle apres 2 seconde
        Destroy(bullet, 2.0f);
    }
}
