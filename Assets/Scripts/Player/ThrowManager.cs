using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowManager : MonoBehaviour
{
    public Transform throwPoint;
    public static Transform passThrowPoint;
    public GameObject throwable;
    public Text woodsText;

    public static int numberOfWoods;
    public float forwardSpeed;
    private void Update()
    {
        if (forwardSpeed < PlayerController.maxSpeed + forwardSpeed)
        {
            forwardSpeed += 0.5f * Time.deltaTime;
        }
        passThrowPoint = throwPoint.transform;
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
        GameObject cB = Instantiate(throwable, throwPoint.position, throwable.transform.rotation);
        Rigidbody rig = cB.GetComponent<Rigidbody>();
        rig.AddForce(throwPoint.forward * forwardSpeed, ForceMode.VelocityChange);

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
