using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeActive : MonoBehaviour
{
    public GameObject[] objectToEnable;
    private void OnEnable()
    {

        for (int i = 0; i < objectToEnable.Length; i++)
        {
            objectToEnable[i].SetActive(true);
        }
    }
}
