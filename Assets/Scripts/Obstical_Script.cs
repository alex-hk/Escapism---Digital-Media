using UnityEngine;
using System.Collections;

public class Obstical_Script : MonoBehaviour {

    public bool running;
    public float tempo, start;

    AudioSource music;
    float interval;
    int chooseChild;
    

	void Start ()
    {
        music = GetComponent<AudioSource>();
        interval = 0;
	}
	
	void Update () 
    {
        if (music.time >= start + interval)
        {
            if (running)
            {
               do
                {
                    chooseChild = Random.Range(0, 8);
                }

                while (transform.GetChild(chooseChild).GetComponent<ChildObsticalsScript>().fading == true);
                transform.GetChild(chooseChild).GetComponent<ChildObsticalsScript>().fading = true;
            }
            interval += tempo;
        }
	}
}
