using UnityEngine;

public class RealisticishJumpScript : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;
    
    [SerializeField]
    private float initialJumpSpeed;
    
    [SerializeField]
    private float gravityForce;

    private bool isJumping;
    private Vector3 jumpInitialPosition;
    private float currentJumpSpeed;

    // Update is called once per frame
    void Update()
    {
        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpInitialPosition = targetTransform.position;
            currentJumpSpeed = initialJumpSpeed;
        }

        if (!isJumping)
        {
            return;
        }

        if (targetTransform.position.y < jumpInitialPosition.y)
        {
            isJumping = false;
            targetTransform.position = jumpInitialPosition;
            return;
        }

        targetTransform.position += Vector3.up * currentJumpSpeed * Time.deltaTime;
        currentJumpSpeed += gravityForce * Time.deltaTime;
    }
}