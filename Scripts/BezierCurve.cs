using UnityEngine;
using System.Collections;

public class BezierCurve : MonoBehaviour {

	// Use this for initialization
    public Vector3 anchor1, anchor2, anchor3, anchor4;
    public float frames;
    Vector3 helper1, helper2, initialOne, initialTwo, initialThree, initialFour;
    float OnetoTwoDistance, TwotoThreeDistance, ThreetoFourDistance;

	void Start () {
        helper1 = anchor1;
        transform.position = anchor1;
        helper2 = anchor2;

        initialOne = anchor1;
        initialTwo = anchor2;
        initialThree = anchor3;
        initialFour = anchor4;

        OnetoTwoDistance = Vector3.Distance(anchor1, anchor2);
        TwotoThreeDistance = Vector3.Distance(anchor2, anchor3);
        ThreetoFourDistance = Vector3.Distance(anchor3, anchor4);
	}
	
	// Update is called once per frame
	void Update () {

        anchor1 = Vector3.MoveTowards(anchor1, initialTwo, OnetoTwoDistance / frames);//120
        anchor2 = Vector3.MoveTowards(anchor2, initialThree, TwotoThreeDistance / frames);
        anchor3 = Vector3.MoveTowards(anchor3, initialFour, ThreetoFourDistance / frames);

        helper1 = Vector3.MoveTowards(helper1, anchor2, //60
            Vector3.Distance(anchor2, anchor1) / (frames/2));
        helper2 = Vector3.MoveTowards(helper2, anchor3,
            Vector3.Distance(anchor3, anchor2) / (frames/2));

        transform.position = Vector3.MoveTowards(transform.position, helper2,//40
            Vector3.Distance(helper1, helper2) / (frames/3));
	
	}
}
