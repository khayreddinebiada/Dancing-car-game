using System.Collections;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    public int numberOfMove = 0;
    public float deltaMoveTime = 0.4f;
    public float timeAdded = 20;

    public GameObject Body;

    private float timeToMove = 10;
    private bool isMove = false;
    // Use this for initialization
    void Start()
    {
        timeToMove = deltaMoveTime * (numberOfMove + timeAdded);
        Body.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToMove < Player.player.time && !isMove)
        {
            Body.SetActive(isMove = true);

            // StartCoroutine(WaitAndDestroy());
        }

    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }
}
