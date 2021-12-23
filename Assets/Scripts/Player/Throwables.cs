using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwables : MonoBehaviour
{
    public Vector3 startPosition;
    private float rotationSpeed = 10f;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Destroyable")
        {
            Destroy(gameObject, 0.5f);
            Destroy(collision.collider.gameObject);
        }
        if (collision.transform.tag == "Obstacle" || collision.transform.tag == "Ground")
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {

        transform.Rotate(Vector3.up, rotationSpeed);

        if (transform.position.z - startPosition.z > 30 || transform.position.y < -1 || transform.position.y > 10)
        {
            Destroy(gameObject);
        }

    }
}
