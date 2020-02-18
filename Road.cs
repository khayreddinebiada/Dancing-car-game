using System.Collections;
using UnityEngine;

public class Road : MonoBehaviour
{
    public bool isMoveByAnim = false;
    public int numberOfMove = 0;
    public float deltaMoveTime = 0.4f;
    public float timeAdded = 10;

    public float speed = 10;
    

    private float timeToMove = 0;
    public GameObject body;
    public float waitAndSetAnimator = 3;

    public bool movingToCenter = false;

    private bool isMovingDown = false;
    public bool isGizmasStart = false;
	// Use this for initialization
	void Start () {

        timeToMove = deltaMoveTime * numberOfMove + timeAdded;
        body.GetComponent<Animator>().enabled = false;

        //body.transform.localPosition = Vector3.zero;
    }

	// Update is called once per frame
	void Update ()
    {

        if (timeToMove + waitAndSetAnimator <= Player.player.time)
        {
            body.GetComponent<Animator>().enabled = true;
            Destroy(this);
        }
        else
            if (movingToCenter && timeToMove <= Player.player.time)
            {
                RoadMove(body.transform);
            }
        
	}

    void RoadMove(Transform body)
    {
        // Move our position a step closer to the target.
        body.transform.position = Vector3.MoveTowards(body.position, body.parent.position, speed * Time.deltaTime);

        if (Vector3.Distance(body.position, body.parent.position) == 0)
            movingToCenter = false;

    }

    public void voidStartMovingDown()
    {
        if (!isMovingDown)
        {
            StartCoroutine(waitAndMoveDown());

            isMovingDown = false;
        }
    }

    IEnumerator waitAndMoveDown()
    {
        yield return new WaitForSeconds(1f);

        GetComponent<Move>().enabled = true;
    }

    /*
    void OnDrawGizmos()
    {
        if (isGizmasStart)
        {
            GameObject go = new GameObject("Body");
            go.transform.parent = transform.parent;

            isGizmasStart = false;
        }
    }
    */
}
