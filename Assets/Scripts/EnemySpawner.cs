using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //prefab de notre ennemi
    public GameObject enemyPrefab;
    
    //nombre d'ennemis à faire apparaitre
    private int numberOfEnemies = 2;
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(-8.0f,8.0f),
                0.0f,
                Random.Range(-8.0f,8.0f));
            
            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0,180),
                0.0f);

            var enemy = (GameObject) Instantiate(enemyPrefab, spawnPosition, spawnRotation);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
