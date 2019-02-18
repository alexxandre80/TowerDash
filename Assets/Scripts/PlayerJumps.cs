using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumps : MonoBehaviour{

    public LayerMask groundLayers;

    public float speed = 5;

    public float jumpForce = 7;

    private Rigidbody rb;

    private SphereCollider col;

    #region Monobehaviour API

    void Start () {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();

    }

	void Update ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }       
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }

    #endregion
}