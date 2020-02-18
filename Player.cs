using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Enums
    public enum MoveType { Stat, Left, Forward };
    public enum LossBy { RayCast, Collider };

    // Public variables
    public MoveType moveType = MoveType.Stat;
    public float Speed = 1f;
    public LossBy lossBy = LossBy.RayCast;
    public bool isLoss = false;
    public bool isStop = false;
    public float lossSpeed = 4;
    public float DistanceToChangeDirection = 1f;

    public bool isWin = false;
    
    public float timeScale = 1;
    public float timeToFinishGame = 50;
    public float timeToStart = 4;
    public float gamePercent;

    public int nbBags = 0;
    public int nbTurn = 0;


    public Animator anim;
    public Animator animCamera;
    public GameObject body;
    public GameObject Effects;
    public GameObject Explosion;
    private bool startPlay = false;
    private Vector3 lastPosition;
    private float distanceOnZ;
    // Static variables
    public static Player player;
    public float time = 0;


    // Private variables
    private Rigidbody rigid;

    void Awake()
    {
        time = 0;
        player = this;
        Time.timeScale = timeScale;
        rigid = GetComponent<Rigidbody>();
    }

	// Use this for initialization
    void Start()
    {
        nbTurn = 0;
        nbBags = 0;
        ControlUI.controlUI.begsTextOnPlay.text = "0";
        Effects.SetActive(false);
        isStop = true;
        isLoss = false;
        isWin = false;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();

        distanceOnZ = Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, lastPosition.y, 0));
        lastPosition = transform.position;
	}

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (Time.timeScale == timeScale)
            {
                Time.timeScale = 0.02f;
            }
            else
            {
                Time.timeScale = timeScale;
            }

        }

        if (timeToStart + 0.1f >= time && timeToStart - 0.1f <= time && !startPlay)
        {
            ControlUI.controlUI.UIOnTap.SetActive(true);
            if (AudioManager.audioManager) AudioManager.audioManager.VolumeMusic(0.3f);
            Time.timeScale = 0;
            startPlay = true;
        }

        if (isLoss || isStop)
        {
            Effects.SetActive(false);
        }
        else
        {

            Effects.SetActive(true);

            gamePercent = Mathf.Clamp((time - timeToStart) * 100 / timeToFinishGame,0,100);
            ControlUI.controlUI.percentOnPlay.value = gamePercent;

        }

        time += Time.deltaTime;
    }

    void PartIsFinished()
    {

    }


    void PlayerMove()
    {
        if (!isLoss && !isStop)
        {

            if (moveType == MoveType.Forward)
            {
                transform.position -= transform.right * Time.fixedDeltaTime * Speed;

                if (!isWin)
                {
                    anim.SetInteger("Set", -1);
                }
            }

            if ((moveType == MoveType.Left || moveType == MoveType.Stat))
            {
                transform.position += transform.forward * Time.deltaTime * Speed;

                if (!isWin)
                {
                    anim.SetInteger("Set", 1);
                }
            }

        }

    }

    public void DirectionChanged()
    {
        if (!isLoss && !isWin && !isStop)
        {

            nbTurn++;
            ControlUI.controlUI.nbTurnTextOnPlay.text = nbTurn.ToString();

            if (distanceOnZ < DistanceToChangeDirection)
            {
                if (Player.player.moveType == Player.MoveType.Forward)
                {
                    Player.player.moveType = Player.MoveType.Left;
                    return;
                }

                if (Player.player.moveType == Player.MoveType.Stat || Player.player.moveType == Player.MoveType.Left)
                {
                    Player.player.moveType = Player.MoveType.Forward;
                    return;
                }
            }

        }

    }
    
    public void IsWin()
    {
        if (!isLoss && !isWin)
        {
            isWin = true;

            ControlUI.controlUI.IsWin();

            ControlUI.controlUI.PanelOnPlay.SetActive(false);
            ControlUI.controlUI.WinPanel.SetActive(true);

            ControlUI.controlUI.nbTurnTextWin.text = nbBags.ToString();
            ControlUI.controlUI.nbTurnTextWin.text = nbTurn.ToString();

            Data.changePercent((int)((99 <= gamePercent) ? 100 : gamePercent));
            Data.changeBagsNumber(nbBags);
            Data.changeTurnNumber(nbTurn);

            if (Random.Range(0, 4) == 1)
                AdsManager.showAdVideo();

            print(player.time);
        }
    }

    public void IsLoss()
    {
        if (!isWin && !isLoss)
            isLoss = true;
        else
            return;

        Data.changePercent((int)gamePercent);
        Data.changeBagsNumber(nbBags);
        Data.changeTurnNumber(nbTurn);

        if (Random.Range(0,4) == 1)
            AdsManager.showAdVideo();

        ControlUI.controlUI.maxPercentLossPanel.text = "MAX: %" + Data.getPercent().ToString();

        ControlUI.controlUI.PanelOnPlay.SetActive(false);
        ControlUI.controlUI.PanelLoss.SetActive(true);

        if (AudioManager.audioManager)
        {
            AudioManager.audioManager.VolumeMusic(0.3f);
            AudioManager.audioManager.StartCollision();

            StartCoroutine(AudioManager.audioManager.waitAndStop(3));
        }

        ControlUI.controlUI.nbTurnText.text = nbTurn.ToString();

        if (rigid)
        {
            rigid.useGravity = false;
        }
        
    }

    void OnTriggerEnter(Collider coll)
    {

        if (!isLoss && !isWin && lossBy == LossBy.Collider)
        {
            
            if (coll.gameObject.layer == 14)
            {

                IsLoss();
                Explosion.SetActive(true);
                body.SetActive(false);

            }
            
        }

        if (!isLoss && !isWin && coll.gameObject.layer == 10)
        {
            IsWin();

        }

        if (coll.gameObject.layer == 11)
        {

            Bag bag = coll.gameObject.GetComponent<Bag>();
            nbBags++;
            ControlUI.controlUI.begsTextOnPlay.text = nbBags.ToString();

            bag.onTouch();

        }

    }

    void OnCollisionEnter(Collision coll)
    {

        if (!isLoss && !isWin && lossBy == LossBy.Collider)
        {
            
            if (coll.gameObject.layer == 14)
            {

                IsLoss();
                Explosion.SetActive(true);
                Explosion.transform.rotation = body.transform.rotation;
                body.SetActive(false);

            }
            else
            {
                if (coll.gameObject.layer == 13)
                {
                    Move move = coll.gameObject.GetComponent<Move>();

                    if (move)
                    {
                        move.StartMovingDown();
                    }
                }
            }
            
        }

    }
}
