using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Throwable")
        {
            LevelUIManager.score += 5;
            FindObjectOfType<Throwables>().PlayParticle();
            gameObject.SetActive(false);
        }
        if (other.tag == "Player")
        {
            LevelUIManager.isGameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }
}
