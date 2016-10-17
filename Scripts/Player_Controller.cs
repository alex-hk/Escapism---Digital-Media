﻿using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
    Rigidbody2D rigid; //Players rigidbody
    //AudioSource fmusic, smusic; //fantasy music and steampunk music
    //Component[] fantasyColliders; //array of colliders of fantasy obstacles
    //Component[] steampunkColliders; //array of colliders of steampunk obstacles
    GameObject[] beziers;
    GameObject fCam, sCam, fObsticals, sObsticals; //both cameras and both sets of obstacles
    bool isWhite, transitioning; //is the player touching the ground?
    SpriteRenderer image;
    float alpha = 0, horizontal, vertical;
    Vector3 initial_position;
    public bool fantasyWorld; //is the player in the fantasy world? (If not, they are in the steampunk world

    //void Awake()
    //{
    //    Application.targetFrameRate = 200;
    //}

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); //gets rigidbody
        image = GameObject.Find("White Screen").GetComponent<SpriteRenderer>();
        fantasyWorld = true; //player starts in fantasy world
        isWhite = false;
        transitioning = false;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        initial_position = transform.position;

        //fObsticals = GameObject.Find("Fantasy Obsticals"); //gets fantasy obsticals (done by using the name of object in the hierachy
        //sObsticals = GameObject.Find("Steampunk Obsticals"); //gets steampunk obsticals (done by using the name of object in the hierachy

        fCam = GameObject.Find("Fantasy Camera"); //gets fantasy camera
        sCam = GameObject.Find("Steampunk Camera"); //gets steampunk camera

        //fmusic = GameObject.Find("Fantasy").GetComponent<AudioSource>(); //gets fantasy music
        //smusic = GameObject.Find("Steampunk").GetComponent<AudioSource>(); //gets steampunk music
        
        //fantasyColliders = fObsticals.GetComponentsInChildren<BoxCollider2D>(); //gets all the boxcollider2Ds in fantasy obsticals
        //steampunkColliders = sObsticals.GetComponentsInChildren<BoxCollider2D>(); //gets all the boxcollider2Ds in steampunk obsticals
        beziers = GameObject.FindGameObjectsWithTag("Curve");

        fCam.GetComponent<Camera>().depth = 0; //fantasy camera is enabled (player starts in fantasy world)
        sCam.GetComponent<Camera>().depth = -1; //steampunk camera is disabled (player starts in fantasy world)

       // foreach (BoxCollider2D b in fantasyColliders) //each boxcollider in fantasy world is enabeld (player starts in fantasy world)
       //     b.enabled = true;
       // foreach (BoxCollider2D b in steampunkColliders) //each boxcollider in steampunk world is disabled (player starts in fantasy world)
       //     b.enabled = false;
      //  foreach (GameObject g in beziers)
      //      g.GetComponent<Circles>().enabled = false;

        //fmusic.enabled = false; //music doens't start until later (see OnTriggerEnter2D)
       // smusic.enabled = false;
    }
	
	void Update () 
    {
        //transform.Translate(Vector2.right * Time.smoothDeltaTime * 10, Space.World); //Moves player forward

        horizontal = Input.GetAxis("Horizontal") * 5;
        vertical = Input.GetAxis("Vertical") * 5;
        transform.position = new Vector3(horizontal, vertical, 0f) + initial_position;

        if (Input.GetKeyDown(KeyCode.Space) && (!transitioning)  ) //when player hits spacebar
        {
            transitioning = true;
            //if(fantasyWorld)
            //    foreach (BoxCollider2D b in fantasyColliders) //fantasy colliders are disabled
            //            b.enabled = false;
            //else
            //    foreach (BoxCollider2D b in steampunkColliders) // colliders are disabled
            //        b.enabled = false;
        }

        if (transitioning && !isWhite)
        {
            if(alpha < 1f)
                alpha += .015f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            //if (fantasyWorld)
            //    fmusic.volume -= .015f;
            //else
            //    smusic.volume -= .015f;
            if (alpha >= 1f)
            {
                isWhite = true;
                fantasyWorld = !fantasyWorld; //switches worlds
                if (fantasyWorld) //if player is in fantasy world after pressing spacebar
                {
                    fCam.GetComponent<Camera>().depth = 0; //switches view from fantasy to steampunk
                    sCam.GetComponent<Camera>().depth = -1;
                }
                else
                {
                    fCam.GetComponent<Camera>().depth = -1; //switches view from steampunk to fantasy
                    sCam.GetComponent<Camera>().depth = 0;
                }
            }
        }

        else if(transitioning && isWhite)
        {
            alpha -= .015f;
            if(alpha > 0)
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            //if (fantasyWorld)
            //    fmusic.volume += .015f;
            //else
            //    smusic.volume += .015f;
            
            if(alpha <= 0)
            {
                isWhite = false;
                alpha = 0;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                transitioning = false;
            //    if(fantasyWorld)
            //    foreach (BoxCollider2D b in fantasyColliders) //fantasy colliders are enabled
            //            b.enabled = true;
            //else
            //    foreach (BoxCollider2D b in steampunkColliders) // colliders are enabled
            //        b.enabled = true;
            }
        
        }

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.tag == "Damage")
         //   gameObject.GetComponent<SpriteRenderer>().enabled = false; // player is no longer rendered once he hits an obstacles
         if(col.name == "Bezier Trigger")
            foreach (GameObject g in beziers)
                g.GetComponent<Circles>().enabled = true;
        //else if (col.name == "Music Trigger")
        //{
        //    fmusic.enabled = true;
        //    smusic.enabled = true;
        //    fmusic.time = 1f;
        //    fmusic.Play();
        //    smusic.time = 5f; //nightcall 5 seconds ahead
        //    smusic.Play();
            
        //    if(fantasyWorld)
        //    {
        //        fmusic.volume = 1;
        //        smusic.volume = 0;
        //    }

        //    else
        //    {
        //        fmusic.volume = 0;
        //        smusic.volume = 1;
        //    }
        //}
    }
}