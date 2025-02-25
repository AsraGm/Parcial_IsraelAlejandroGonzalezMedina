using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DESTROYOBSTACULE : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EndLayer"))
        {
            Destroy(gameObject);
        }
    }
}
