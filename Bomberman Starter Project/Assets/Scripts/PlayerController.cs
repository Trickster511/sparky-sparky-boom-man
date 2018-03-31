using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : PlayerStats
{
    private GameObject pm;

    public float speed = 20f;
    public float kickSpeed = 8f;

    bool stopRepeating = false;

    private Tilemap tilemap;
    public GameObject bombPrefab;
    public GameObject BombPrefab()
    {
        GameObject bp = bombPrefab;
        bp.GetComponent<Bomb>().player = this;

        return bp;
    }
    public Animator anim { get; private set; }

    private bool isAlreadyBomb;
    private bool isDead = false;
    public bool isMultiPlayer;

    public int numPlayer = 1;

    MenuScript menuscript;
    MultiplayerManager multiManager;

    void Start()
    {
        if (!isMultiPlayer)
        {
            menuscript = MenuScript.instance;
            pm = menuscript.pm;
            tilemap = menuscript.tilemap;
        }
        else
        {
            multiManager = MultiplayerManager.instance;
            pm = multiManager.pm;
            tilemap = multiManager.tilemap;
        }

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(pm.activeSelf || isDead)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1") && numPlayer == 1)
        {
            Debug.Log("Player " + numPlayer + " layed a bomb.");
            LayBombs();
        }
        if (Input.GetButtonDown("Fire12p") && numPlayer == 2)
        {
            Debug.Log("Player " + numPlayer + " layed a bomb.");
            LayBombs();
        }
        if (Input.GetButtonDown("Fire13p") && numPlayer == 3)
        {
            Debug.Log("Player " + numPlayer + " layed a bomb.");
            LayBombs();
        }
        if (Input.GetButtonDown("Fire14p") && numPlayer == 4)
        {
            Debug.Log("Player " + numPlayer + " layed a bomb.");
            LayBombs();
        }
    }

    void FixedUpdate()
    {
        if (numPlayer == 1)
        {
            Move();
        }
        if(numPlayer == 2)
        {
            MoveTwo();
        }
        if (numPlayer == 3)
        {
            MoveThree();
        }
        if (numPlayer == 4)
        {
            MoveFour();
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);

        anim.SetFloat("X", h);
        anim.SetFloat("Y", v);
    }
    void MoveTwo()
    {
        float h = Input.GetAxis("Horizontal2");
        float v = Input.GetAxis("Vertical2");

        transform.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);

        anim.SetFloat("X", h);
        anim.SetFloat("Y", v);
    }
    void MoveThree()
    {
        float h = Input.GetAxis("Horizontal3");
        float v = Input.GetAxis("Vertical3");

        transform.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);

        anim.SetFloat("X", h);
        anim.SetFloat("Y", v);
    }
    void MoveFour()
    {
        float h = Input.GetAxis("Horizontal4");
        float v = Input.GetAxis("Vertical4");

        transform.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);

        anim.SetFloat("X", h);
        anim.SetFloat("Y", v);
    }

    void LayBombs()
    {
        if (bombCount >= bombMax)
        {
            return;
        }
        if (isAlreadyBomb)
        {
            return;
        }
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);


        Instantiate(BombPrefab(), cellCenterPos, Quaternion.identity);
        HigherBombCount();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bomb" && isKick)
        {
            Vector3 dis = transform.position - other.transform.position;
            Bomb bomb = other.gameObject.GetComponent<Bomb>();
            bomb.direct = dis;
            bomb.speed = kickSpeed;
            bomb.isKicked = true;
        }
            
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (stopRepeating)
        {
            return;
        }

        if (other.tag == "Enemy" || other.tag == "Explosion")
        {
            isDead = true;
            if (!isMultiPlayer)
                menuscript.PlayerDied();
            else
                multiManager.PlayerDied(this.gameObject);

            stopRepeating = true;
        }

        if (other.tag == "Bomb")
        {
            isAlreadyBomb = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Bomb")
        {
            isAlreadyBomb = false;
        }
    }
}
