using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimObject : MonoBehaviour {

    public GameObject PointsMove;
    public float SpeedMove = 1f;
    public bool isStartAnimation = true;

    private int indexStartAnimation = 1;
    private Transform[] Points;


    // Use this for initialization
    void Start()
    {
        Points = PointsMove.GetComponentsInChildren<Transform>();

        transform.position = Points[indexStartAnimation].position;
        indexStartAnimation++;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isStartAnimation)
        {
            transform.position += (Points[indexStartAnimation + 1].position - transform.position) / Vector3.Distance(Points[indexStartAnimation + 1].position, transform.position) * SpeedMove * Time.deltaTime;

            if (Vector3.Distance(transform.position, Points[indexStartAnimation + 1].position) < 0.3f)
            {
                indexStartAnimation++;
                if (indexStartAnimation == Points.Length - 1)
                    isStartAnimation = false;

            }

            if (Vector3.Distance(transform.position, Points[Points.Length - 1].position) < 0.3f)
            {
                isStartAnimation = false;
                transform.position = Points[indexStartAnimation].position;
            }
        }
    }
}
