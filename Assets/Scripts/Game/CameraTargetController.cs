using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetController : MonoBehaviour
{
    private Vector3 directionCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelUIManager.isGameStarted)
        {
            return;
        }
        directionCamera.z = PlayerController.forwardSpeed;
        transform.position += directionCamera * Time.deltaTime;
    }
}
