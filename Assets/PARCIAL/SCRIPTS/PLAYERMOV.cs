using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERMOV : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f; // Variable para saber la fuerza del salto
    [SerializeField] private float crounch = 0.008f; // Variable para saber cuanto se agacha
    [SerializeField] private LayerMask layerGround; // Variable para saber que es suelo
    [SerializeField] private Transform head; // Variable para saber la cabeza del jugador

    private Vector3 originalHeadPosition; // Variable para saber la posicion original de la cabeza
    private bool hasJumped = false; // Variable para saber si ya ha saltado y evitar doble salto
    private Rigidbody rb; // Referencia al Rigidbody

    private void Start()
    {
        originalHeadPosition = head.localPosition; // Se guarda la posicion original de la cabeza
        rb = GetComponent<Rigidbody>(); // Obtener referencia al Rigidbody
        rb.useGravity = true; // Asegúrate de que la gravedad esté habilitada
    }

    private void Update()
    {
        Jump();
        Crouch();
        UpdateHeadPosition();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround() && !hasJumped) // Si se presiona la tecla de espacio y esta en el suelo y no ha saltado
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Se le agrega una fuerza hacia arriba
            hasJumped = true; // Se cambia la variable a true
            Debug.Log("Jump");
        }
        else if (CheckGround()) // Si esta en el suelo
        {
            hasJumped = false; // Se cambia la variable a false
        }
        else
        {
            Debug.Log("Not Jump");
        }
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) && !hasJumped) // Si se presiona el botón del ratón y no ha saltado
        {
            head.localPosition = new Vector3(head.localPosition.x, originalHeadPosition.y - crounch, head.localPosition.z); // Se agacha
            Debug.Log("Crouch");
        }
        else if (!hasJumped) // Si no ha saltado
        {
            head.localPosition = originalHeadPosition; // Se para
            Debug.Log("Stand");
        }
    }

    void UpdateHeadPosition()
    {
        if (hasJumped)
        {
            head.localPosition = new Vector3(head.localPosition.x, transform.localPosition.y + originalHeadPosition.y, head.localPosition.z);
        }
    }

    bool CheckGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2.5f, layerGround)) // Si detecta el suelo
        {
            Debug.Log("Ground");
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * 2.5f);
    }
}