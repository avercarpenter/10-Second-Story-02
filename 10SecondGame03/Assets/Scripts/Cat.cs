using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        ShipController controller = other.GetComponent<ShipController>();

        if (controller != null)
        {
            Destroy(gameObject);
        }

    }
}
