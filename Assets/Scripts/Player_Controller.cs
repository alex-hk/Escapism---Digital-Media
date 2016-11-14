using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
    Rigidbody2D rigid; //Players rigidbody
    AudioSource fmusic, smusic; //fantasy music and steampunk music
    //GameObject[] beziers;
    bool isWhite, transitioning, hazSphere, dead;
    float alpha = 0, horizontal, vertical;
    GameObject fCam, sCam, fObsticals, sObsticals, stupid_sphere; //both cameras and both sets of obstacles
    int stillNotDead= 0;
    Light pointLight, directLight;
    SpriteRenderer image;
    Vector3 initial_position;
    public bool fantasyWorld; //is the player in the fantasy world? (If not, they are in the steampunk world)

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); //gets rigidbody
        image = GameObject.Find("White Screen").GetComponent<SpriteRenderer>();
        fantasyWorld = false; //player starts in steampunk world
        isWhite = false;
        transitioning = false;
        hazSphere = false;
        dead = false;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        initial_position = transform.position;

        fObsticals = GameObject.Find("Fantasy Obsticals"); //gets fantasy obsticals (done by using the name of object in the hierachy
        sObsticals = GameObject.Find("Steampunk Obsticals"); //gets steampunk obsticals (done by using the name of object in the hierachy

        fCam = GameObject.Find("Fantasy Camera"); //gets fantasy camera
        sCam = GameObject.Find("Steampunk Camera"); //gets steampunk camera

        fmusic = fObsticals.GetComponent<AudioSource>(); //gets fantasy music
        smusic = sObsticals.GetComponent<AudioSource>(); //gets steampunk music

        pointLight = GameObject.Find("Point Light").GetComponent<Light>();
        directLight = GameObject.Find("Directional Light").GetComponent<Light>();

        fCam.GetComponent<Camera>().depth = -1; //fantasy camera is disabled (player starts in steampunk world)
        sCam.GetComponent<Camera>().depth = 0; //steampunk camera is enabled (player starts in steampink world)

        stupid_sphere = GameObject.Find("Sphere");
    }
	
	void Update () 
    {
        horizontal = Input.GetAxis("Horizontal") * 5;
        vertical = Input.GetAxis("Vertical") * 5;
        transform.position = new Vector3(horizontal, vertical, 0f) + initial_position;
        if (!dead && !hazSphere)
            stillNotDead++;
        if (!hazSphere && stillNotDead >= 100)
            stupid_sphere.transform.Translate(Vector2.left * Time.deltaTime * 10);

        if (hazSphere && Input.GetKeyDown(KeyCode.Space) && (!transitioning)) //when player hits spacebar
        {
            transitioning = true;
            hazSphere = false;
            if(fantasyWorld)
                fObsticals.GetComponent<Obstical_Script>().running = false;
            else
                sObsticals.GetComponent<Obstical_Script>().running = false;
        }

        if (transitioning && !isWhite)
        {
            if(alpha < 1f)
                alpha += .015f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            if (fantasyWorld)
            {
                fmusic.volume -= .015f;
                directLight.intensity -= .03f;
            }
            else
            {
                smusic.volume -= .015f;
                pointLight.intensity -= .015f * 8;
            }
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

            if (fantasyWorld)
            {
                fmusic.volume += .015f;
                directLight.intensity += .03f;
            }
            else
            {
                smusic.volume += .015f;
                pointLight.intensity += .015f * 8;
            }
            
            if(alpha <= 0)
            {
                isWhite = false;
                alpha = 0;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                transitioning = false;
                if(fantasyWorld)
                    fObsticals.GetComponent<Obstical_Script>().running = true;
                else
                    sObsticals.GetComponent<Obstical_Script>().running = true;
            }
        
        }

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Damage")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false; // player is no longer rendered once he hits an obstacles
            dead = true;
        }

        else if (col.tag == "Stupid")
        {
            col.transform.parent.GetComponent<MeshRenderer>().enabled = false;
            hazSphere = true;
        }
    }
}
