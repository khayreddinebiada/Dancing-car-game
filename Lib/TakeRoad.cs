using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeRoad : MonoBehaviour
{
    public GameObject path;
    public bool isMoving = false;
    public float speedRotate = 10;

    public Transform[] PointMovement;

    private float speedMoving = 10;
    private int nextPointMoveIndex = 1;

	// Use this for initialization
	void Start () {

        speedMoving = Player.player.Speed;

        PointMovement = path.GetComponentsInChildren <Transform>();

        StartCoroutine(waitAndSetMoving());
	}
    IEnumerator waitAndSetMoving()
    {
        isMoving = false;
        yield return new WaitForSeconds(Player.player.timeToStart);

        isMoving = true;
    }
	// Update is called once per frame
	void FixedUpdate () {

        MovingPlayer();
    }

    void MovingPlayer()
    {
        if (!isMoving)
            return;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, PointMovement[nextPointMoveIndex].position, speedMoving * Time.fixedDeltaTime);

        Vector3 targetDir = PointMovement[nextPointMoveIndex].position - transform.position;

        rotateToWord(targetDir);

        if (Vector3.Distance(transform.position, PointMovement[nextPointMoveIndex].transform.position) == 0)
            nextPointMoveIndex++;

        if (nextPointMoveIndex == PointMovement.Length)
        {
            isMoving = false;
        }


    }

    void rotateToWord(Vector3 target)
    {
        Vector3 rotateTo = Vector3.RotateTowards(transform.forward, target, speedRotate * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(rotateTo);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotateTo), Player.player.time * speedRotate);
    }

}
