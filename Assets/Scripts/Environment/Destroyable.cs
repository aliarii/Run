using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Destroyable")
        {
            Destroy(collision.collider.gameObject.GetComponent<Collider>(), 0.1f);
        }
    }
}
