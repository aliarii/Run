using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem explosionParticle;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Destroyable" || collision.transform.tag == "Throwable")
        {
            LevelUIManager.score += 5;
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            //Destroy(collision.collider.gameObject.GetComponent<Collider>(), 0.1f);
            Destroy(gameObject);
        }

    }
}
