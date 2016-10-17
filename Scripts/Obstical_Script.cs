using UnityEngine;
using System.Collections;

public class Obstical_Script : MonoBehaviour {

    AudioSource fmusic; //fantasy music and steampunk music
    //bool fadeIn;
	void Start () {

        fmusic = GetComponent<AudioSource>();
        //fadeIn = false;
        foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>())
            s.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (fmusic.time >= 2.0f)
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        
        Debug.Log(fmusic.time);
	}
}
