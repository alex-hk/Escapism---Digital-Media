using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
    Rigidbody2D rigid; //Players rigidbody
    AudioSource fmusic, smusic; //fantasy music and steampunk music
    Component[] fantasyColliders; //array of colliders of fantasy obstacles
    Component[] steampunkColliders; //array of colliders of steampunk obstacles
    GameObject fCam, sCam, fObsticals, sObsticals; //both cameras and both sets of obstacles
    bool grounded; //is the player touching the ground?
    public bool fantasyWorld; //is the player in the fantasy world? (If not, they are in the steampunk world
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); //gets rigidbody
        grounded = true; //player starts off on the ground
        fantasyWorld = true; //player starts in fantasy world

        fObsticals = GameObject.Find("Fantasy Obsticals"); //gets fantasy obsticals (done by using the name of object in the hierachy
        sObsticals = GameObject.Find("Steampunk Obsticals"); //gets steampunk obsticals (done by using the name of object in the hierachy

        fCam = GameObject.Find("Fantasy Camera"); //gets fantasy camera
        sCam = GameObject.Find("Steampunk Camera"); //gets steampunk camera

        fmusic = GameObject.Find("Fantasy").GetComponent<AudioSource>(); //gets fantasy music
        smusic = GameObject.Find("Steampunk").GetComponent<AudioSource>(); //gets steampunk music
        
        fantasyColliders = fObsticals.GetComponentsInChildren<BoxCollider2D>(); //gets all the boxcollider2Ds in fantasy obsticals
        steampunkColliders = sObsticals.GetComponentsInChildren<BoxCollider2D>(); //gets all the boxcollider2Ds in steampunk obsticals

        fCam.GetComponent<Camera>().enabled = true; //fantasy camera is enabled (player starts in fantasy world)
        sCam.GetComponent<Camera>().enabled = false; //steampunk camera is disabled (player starts in fantasy world)

        foreach (BoxCollider2D b in fantasyColliders) //each boxcollider in fantasy world is enabeld (player starts in fantasy world)
            b.enabled = true;
        foreach (BoxCollider2D b in steampunkColliders) //each boxcollider in steampunk world is disabled (player starts in fantasy world)
            b.enabled = false;

        fCam.GetComponent<Camera>().enabled = true; //fantasy camera is enabled (player starts in fantasy world)
        sCam.GetComponent<Camera>().enabled = false; //steampunk camera is disabled (player starts in fantasy world)

        fmusic.enabled = false; //music doens't start until later (see OnTriggerEnter2D)
        smusic.enabled = false;
    }
	
	void Update () {
        transform.Translate(Vector2.right * Time.smoothDeltaTime * 10); //Moves player forward

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) //Jumping, only allowed while player is on the ground
            rigid.AddForce(Vector2.up * 300);

        if (Input.GetKeyDown(KeyCode.Space)) //when player hits spacebar
        {
            fantasyWorld = !fantasyWorld; //switches worlds

            if(fantasyWorld) //if player is in fantasy world after pressing spacebar
            {
                fCam.GetComponent<Camera>().enabled = true; //switches view from steampunk to fantasy
                sCam.GetComponent<Camera>().enabled = false;

                foreach (BoxCollider2D b in fantasyColliders) //fantasy colliders are enabled
                    b.enabled = true;
                foreach (BoxCollider2D b in steampunkColliders) //steampunk colliders are disabled
                    b.enabled = false;

                fmusic.volume = 1; //fantasy music is played at max volume
                smusic.volume = 0; //steampunk music is muted (still technically plays)
            }
            else //if player is in the steampunk world after pressing spacebar
            {
                sCam.GetComponent<Camera>().enabled = true; //switches view from fantasy to steampunk
                fCam.GetComponent<Camera>().enabled = false;

                foreach (BoxCollider2D b in steampunkColliders) //turns on steampunk colliders
                    b.enabled = true;
                foreach (BoxCollider2D b in fantasyColliders) //turns off fantasy olliders
                    b.enabled = false;

                fmusic.volume = 0; //fantasy music is muted (still technically plays)
                smusic.volume = 1; //steampunk music is played at max volume
            }

        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") //grounded is true once player touches the ground
            grounded = true;
    }

    void OnCollisionExit2D(Collision2D col) //grounded is false once player leaves the ground
    {
        if(col.gameObject.tag == "Ground")
            grounded = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Damage")
            gameObject.GetComponent<SpriteRenderer>().enabled = false; // player is no longer rendered once he hits an obstacles (destroying the player turned the music off)
        if (col.name == "Music Trigger")
        {
            fmusic.enabled = true;
            smusic.enabled = true;
            fmusic.time = 1f;
            fmusic.Play();
            smusic.time = 11.75f; //nightcall starts after the intro
            smusic.Play();
            
            if(fantasyWorld)
            {
                fmusic.volume = 1;
                smusic.volume = 0;
            }

            else
            {
                fmusic.volume = 0;
                smusic.volume = 1;
            }
        }
    }
}
