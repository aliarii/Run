using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{

    private float speed = 5f;
    private bool goRight;
    private bool goLeft;
    private void Start()
    {
        if (transform.position.x < 2 && transform.position.x > 0)
        {
            goLeft = true;
            goRight = false;
        }
        else
        {
            goLeft = false;
            goRight = true;
        }
    }
    void Update()
    {
        if (goLeft)
        {
            if (transform.position.x < -2)
            {
                goLeft = false;
                goRight = true;
            }
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        }
        else if (goRight)
        {
            if (transform.position.x > 2)
            {
                goLeft = true;
                goRight = false;
            }
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

    }
}
