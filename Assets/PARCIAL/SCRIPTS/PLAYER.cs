using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
    public float jumpForce = 10f;
    public float scaleReduction = 0.5f;
    private bool isGrounded = false;
    private Vector3 originalScale;

    /// <summary>
    /// Su mision es devolvernos cual de las 3 velocidades es la que necesitamos (walk, run, crouch)
    /// </summary>
    void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            Jump();

        }

        if (Input.GetMouseButtonDown(1))
        {
            ScaleDown();
        }
    }

    void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void ScaleDown()
    {
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground")) { isGrounded = true; }
    }
}