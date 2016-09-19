using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

    AudioSource audio;
    GameObject player;

	void Start () {
        audio = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        if(audio.clip.name == "Nightcall")
            audio.time = 11.75f;
        audio.Play();
	
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(audio.timeSamples);
	
	}
}
