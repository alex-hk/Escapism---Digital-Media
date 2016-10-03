using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fade : MonoBehaviour {

	void Start () {

	}

	// Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.smoothDeltaTime * 10);
    }
       
}
