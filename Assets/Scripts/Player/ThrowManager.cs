using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowManager : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject throwableObject;
    public Text woodsText;

    public static int numberOfWoods;
    public float forwardSpeed;
    private void Start()
    {
        numberOfWoods = 0;
    }
    private void Update()
    {
        if (forwardSpeed < PlayerController.maxSpeed + forwardSpeed)
        {
            forwardSpeed += 0.5f * Time.deltaTime;
        }
        if (numberOfWoods > 99)
        {
            woodsText.text = "99+";
        }
        else
        {
            woodsText.text = numberOfWoods.ToString();
        }


    }
    public void ThrowObject()
    {

        GameObject throwable = Instantiate(throwableObject, throwPoint.position + new Vector3(0, 0, 3f), throwableObject.transform.rotation);
        Rigidbody throwableRb = throwable.GetComponent<Rigidbody>();
        throwableRb.AddForce(throwPoint.forward * forwardSpeed, ForceMode.VelocityChange);
    }
    public void ControlThrow()
    {
        if (numberOfWoods <= 0)
        {
            return;
        }
        else
        {
            ThrowObject();
            numberOfWoods -= 1;
        }

    }
}
