using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShipController : MonoBehaviour
{
    private float holdDownStartTime; 

   // private bool didMouseDown;
	private bool didMouseUp;
   
    public float speed; 

    
    private Rigidbody2D rd2d;

    Vector2 lookDirection = new Vector2(1,0);

    Animator animator;
   // AudioSource audioSource;

    //Ui time and win/lose
    public Text TimerTxt;

    public float TimeLeft;
    public bool TimerOn = false; 

    public GameObject loseTextObject;
    public GameObject winTextObject;

    public bool gameOver;
    public bool catSaved = false;


    //particle systems
    public ParticleSystem boostEffect;
    public ParticleSystem sparkleEffect;
    public ParticleSystem smokeEffect;

    public ParticleSystem catCollected; 
    public ParticleSystem catCollected2;

    // audio 
    AudioSource audioSource;

    public AudioClip holdSound;
    public AudioClip boostSound;
    public AudioClip catSound;
    public AudioClip starSound;
   
    public GameObject winSound;
    public GameObject looseSound;

    public GameObject backgroundMusic;

    void Start()
    {
       ResetMouse();
       rd2d = GetComponent <Rigidbody2D> ();
       animator = GetComponent<Animator>();
       audioSource = GetComponent<AudioSource>();
       
       backgroundMusic.SetActive(true);
       winSound.SetActive(false);
       looseSound.SetActive(false); 
       // didMouseDown = false; 
        didMouseUp = false; 
        gameOver = false; 
        loseTextObject.SetActive(false);
        winTextObject.SetActive(false);

    }
  
    private void Update()
    {
 
        if (Input.GetMouseButtonDown(0)) 
        {
           holdDownStartTime = Time.time; 
          // Debug.Log("Button down");
          // didMouseDown = true;

        }

        if (Input.GetMouseButtonUp(0))
        {
            TimerOn = true;
            float holdDownTime = Time.time - holdDownStartTime; 
         //.Launch(CalculateHoldDownForce(holdDownTime));
            rd2d.velocity = transform.forward * (CalculateHoldDownForce(holdDownTime));
         //Debug.Log("Button Up");
            didMouseUp = true; 
     
          
         
          if(speed > 1 )
          {
            Instantiate(boostEffect, transform.position, Quaternion.identity);
            boostEffect.Play(); 
            Instantiate(sparkleEffect, transform.position, Quaternion.identity);
            sparkleEffect.Play();
            Instantiate(smokeEffect, transform.position, Quaternion.identity);
            smokeEffect.Play();
          }


        if (didMouseUp == true & gameOver == false)
          {
            PlaySound(boostSound);
          }


        }

        if(TimerOn)
        {
            if(TimeLeft > 0) 
            {
                TimeLeft -= Time.deltaTime; 
                updateTimer(TimeLeft); 
                // if collect cat and time is more then 0 game win

                if(catSaved == true)
                {
                    winTextObject.SetActive(true);
                    speed = 0.0f; 
                    gameOver = true;
                    TimerOn = false; 
                    backgroundMusic.SetActive(false);
                    winSound.SetActive(true);
                }
            }
            else
            {
               // Debug.Log("Times up loser!");
                TimeLeft = 0; 
                TimerOn = false; 
                speed = 0.0f;
                loseTextObject.SetActive(true); 
                gameOver = true; 
                backgroundMusic.SetActive(false);
                looseSound.SetActive(true);
             }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cat"))
        {
            Destroy(other.gameObject);
            catCollected.Play();
            catCollected2.Play();
            catSaved = true; 
            PlaySound(catSound);
        }

      //  if (other.CompareTag("Star"))
     //   {
      //      audioSource.PlayOneShot(starSound); 
      //      Debug.Log("ive hit a star"); 
      //  }
    }

 


    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}", seconds);
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        //transform.position = new Vector3(transform.position.x + (shipSpeed * hozMovement), transform.position.y + (shipSpeed * verMovement), 0f);
 
         if(hozMovement!=0 || verMovement!=0)
        {
            Quaternion newRotation = transform.rotation;
            newRotation.SetLookRotation(new Vector3(hozMovement, verMovement, 3f).normalized, Vector3.back);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, .1f);
        }
        

    }

    
    void ResetMouse ()
	{
		didMouseUp = false;
		//didMouseDown = false;
        
	}

    // holding mouse down * time = force 
    private float CalculateHoldDownForce(float holdTime)
    {
        float maxForceHoldDownTime = 5f; 
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime); 
        float force = holdTimeNormalized * speed;
       // rd2d.AddForce(transform.forward * speed);
        return force; 
    }
    
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);

     //   if (gameOver == true)
     //     {
     //       audioSource.mute = !audioSource.mute;
      //    }
    }

}
