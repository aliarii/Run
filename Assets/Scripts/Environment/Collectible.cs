using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //rotate object
    void Update()
    {
        transform.Rotate(0, 0, 50 * Time.deltaTime);
    }

    //check player interaction
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().PlaySound("PickUp");
            ThrowManager.numberOfWoods += 1;
            gameObject.SetActive(false);
        }
    }
}
