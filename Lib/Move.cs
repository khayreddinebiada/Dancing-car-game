using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speedRotate = 1f;
    public bool isRotate = false;

    public float speedMovingToCenter = 1f;
    public bool movingToCenter = false;

    public float speedMovingDown = 1f;
    public float timeToStartMovingUp = 1f;
    public bool isMovingDown = false;
    public bool MovingToStartGame = false;


    public float DistanceToMoveToInitPlace = 10f;
    public float speedMovingToInitPlace = 10f;
    public Vector3 newPlaceAdded;
    private Vector3 initPlace;
    public bool isMovingToInitPlace = false;
    private bool startMovingToInitPlace = false;

	// Use this for initialization
    void Start()
    {
        if (MovingToStartGame)
        {
            StartCoroutine(waitAndMoveDown());
        }

        if (isMovingToInitPlace)
        {
            initPlace = transform.position;
            transform.position += newPlaceAdded;
        }

	}
	
	// Update is called once per frame
    void Update()
    {

        if (isRotate) transform.Rotate(speedRotate * Time.deltaTime * Vector3.up);

        if (movingToCenter) MoveToCenter();

        if (isMovingDown) MovingDown();

        if (isMovingToInitPlace && Vector3.Distance(Player.player.transform.position, transform.position) <= DistanceToMoveToInitPlace)
        {
            startMovingToInitPlace = true;
        }
        if (startMovingToInitPlace)
        {
            transform.position = Vector3.MoveTowards(transform.position, initPlace, speedMovingToInitPlace * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, initPlace) == 0)
                Destroy(this);
        }

    }

    void MoveToCenter()
    {
        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, speedMovingToCenter * Time.deltaTime);

        
        if (Vector3.Distance(transform.position, transform.parent.position) == 0)
            movingToCenter = false;
        
    }

    void MovingDown()
    {
        transform.position -= Vector3.up * speedMovingDown * Time.deltaTime;
    }

    public void StartMovingDown()
    {
        if (!isMovingDown)
        {
            StartCoroutine(waitAndMoveDown());

            isMovingDown = false;
        }
    }

    IEnumerator waitAndMoveDown()
    {

        yield return new WaitForSeconds(timeToStartMovingUp);

        GetComponent<Move>().isMovingDown = true;
        StartCoroutine (waitAndDestroy());

    }

    IEnumerator waitAndDestroy()
    {

        yield return new WaitForSeconds(3);
        Destroy(gameObject);

    }
}
