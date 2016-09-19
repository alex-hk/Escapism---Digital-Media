using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {

	void Start () 
    {
	}
	
	void Update () 
    {
        transform.Translate(Vector2.right * Time.smoothDeltaTime * 10);
	}
}
