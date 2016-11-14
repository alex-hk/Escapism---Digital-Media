using UnityEngine;
using System.Collections;

public class ChildObsticalsScript : MonoBehaviour {

    public bool fading;
    bool fadeOut;
    Color defaultColor;
    float alpha;
    Light pointLight;
    int wait;

	void Start () 
    {
        fading = false;
        fadeOut = false;
        alpha = 0;
        wait = 5;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, 
            gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0);

        defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
        pointLight = GameObject.Find("Point Light").GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(fading)
        {
            if (alpha < 1f && !fadeOut)
            {
                if (!GameObject.Find("Player").GetComponent<Player_Controller>().fantasyWorld)
                {
                    pointLight.transform.position = new Vector3(transform.position.x, transform.position.y, -3);
                    pointLight.intensity += 8f * Time.deltaTime;
                }
                alpha += 1f * Time.deltaTime;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r,
                gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, alpha);
            }

            if (alpha >= 1f && !fadeOut)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                alpha = 0;
                fadeOut = true;
            }
        }
        if (fadeOut)
        {
            --wait;
            if (wait <= 0)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
                wait = 5;
                fading = false;
                fadeOut = false;
                pointLight.intensity = 0;
            }
        }
	}
}
