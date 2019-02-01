using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPositionTransform;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private float timeBetweenSpawns;

    private float lastSpawnTime = float.MinValue;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime > timeBetweenSpawns)
        {
            Instantiate(ballPrefab,
                spawnPositionTransform.position,
                Quaternion.identity);
            lastSpawnTime = Time.time;
        }
    }
}