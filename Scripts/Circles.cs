using UnityEngine;
using System.Collections;

public class Circles : MonoBehaviour {

    Vector2 initialOne, initialTwo, initialThree, initialFour;
    float OnetoTwo, TwotoThree, ThreetoFour;
    GameObject player;
    bool reverse;
    public float steps;

	void Start () {
        initialOne = transform.GetChild(0).transform.position;//H1
        initialTwo = transform.GetChild(1).transform.position;//H2
        initialThree = transform.GetChild(2).transform.position;//H3
        initialFour = transform.GetChild(3).transform.position;//H4

        OnetoTwo = Vector2.Distance(initialOne, initialTwo);
        TwotoThree = Vector2.Distance(initialTwo, initialThree);
        ThreetoFour = Vector2.Distance(initialThree, initialFour);

        transform.GetChild(4).position = initialOne;
        transform.GetChild(5).position = initialTwo;
        transform.GetChild(6).position = initialOne;

        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
            transform.GetChild(0).position = Vector2.MoveTowards(transform.GetChild(0).position, initialTwo, OnetoTwo / steps);//120
            transform.GetChild(1).position = Vector2.MoveTowards(transform.GetChild(1).position, initialThree, TwotoThree / steps);
            transform.GetChild(2).position = Vector2.MoveTowards(transform.GetChild(2).position, initialFour, ThreetoFour / steps);

            transform.GetChild(4).position = Vector2.MoveTowards(transform.GetChild(4).position, transform.GetChild(1).position, //60
                Vector2.Distance(transform.GetChild(1).position, transform.GetChild(0).position) / (steps / 2));
            transform.GetChild(5).position = Vector2.MoveTowards(transform.GetChild(5).position, transform.GetChild(2).position,
                Vector2.Distance(transform.GetChild(2).position, transform.GetChild(1).position) / (steps / 2));

            transform.GetChild(6).position = Vector2.MoveTowards(transform.GetChild(6).position, transform.GetChild(5).position,//40
                Vector2.Distance(transform.GetChild(4).position, transform.GetChild(5).position) / (steps / 3));


        if (transform.GetChild(6).position ==  transform.GetChild(5).position)
        {
            float move = Vector3.Distance(initialOne, initialFour);
            initialOne += Vector2.right * move;
            initialTwo += Vector2.right * move;
            initialThree += Vector2.right * move;
            initialFour += Vector2.right * move;

            transform.GetChild(0).transform.position = initialOne;//H1
            transform.GetChild(1).transform.position = initialTwo;//H2
            transform.GetChild(2).transform.position = initialThree;//H3
            transform.GetChild(3).transform.position = initialFour;//H4

            transform.GetChild(4).position = initialOne;
            transform.GetChild(5).position = initialTwo;
            transform.GetChild(6).position = initialOne;
        }

        Debug.DrawRay(initialOne, initialTwo - initialOne, Color.black);
        Debug.DrawRay(initialTwo, initialThree - initialTwo, Color.black);
        Debug.DrawRay(initialThree, initialFour - initialThree, Color.black);
        Debug.DrawRay(transform.GetChild(0).transform.position, transform.GetChild(1).transform.position - transform.GetChild(0).transform.position, Color.green);
        Debug.DrawRay(transform.GetChild(1).transform.position, transform.GetChild(2).transform.position - transform.GetChild(1).transform.position, Color.green);

        Debug.DrawRay(transform.GetChild(4).transform.position, transform.GetChild(5).transform.position - transform.GetChild(4).transform.position, Color.magenta);
	}
}
