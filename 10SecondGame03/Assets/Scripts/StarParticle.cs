using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarParticle : MonoBehaviour
{
     public Rigidbody2D rb;
     public ParticleSystem starTrail;

     public GameObject starSound;
 
    private void Start()
     {
         rb = GetComponent<Rigidbody2D>();
         starSound.SetActive(false);
     }
 
    void Update()
     {
         Vector2 v2Velocity = rb.velocity;
         if (v2Velocity.x != 0)
         {
            //Instantiate(starTrail, transform.position, Quaternion.identity);
            starTrail.Play();
            //Debug.Log("LOOK A SHOOTING STAR!");
         }

    void OnTriggerEnter2D(Collider2D other)
      {
       // ShipController controller = other.GetComponent<ShipController>();

        if (other.gameObject.tag == "Player")
        {
            starSound.SetActive(true);
        }
        
        if (other.gameObject.tag == "Star")
        {
            starSound.SetActive(true);
        }

      }

      //   else
       //  {
       //      StopStarTrail();
       //  }
     }
   //  void StartStarTrail()
    // {
    //        Instantiate(starTrail, transform.position, Quaternion.identity);
   //         starTrail.Play(); 
   //  }
     //void StopStarTrail()
    // {
    //     starTrail.Stop();
   //  }
 }
