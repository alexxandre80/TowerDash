using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubeScript : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private float moveSpeed;

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
    }
}
