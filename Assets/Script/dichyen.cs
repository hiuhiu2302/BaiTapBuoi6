using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class dichuyen : MonoBehaviour
{
    public static bool isGameOver = false;
    public float speedX, speedY;
    private Animator player;
    private bool canJump = true;
    private int jumpCount = 0;
    private int mau = 3;
    private GameObject[] mauObjects;

    int score = 0;
    public Text txtScore;

    void Start()
    {
        txtScore = GameObject.Find("txtDiem").GetComponent<Text>();
        player = GetComponent<Animator>();
        isGameOver = false;

        player.SetBool("dichuyen", false);
        player.SetBool("dungyen", true);

        mauObjects = new GameObject[3];
        mauObjects[0] = GameObject.FindGameObjectWithTag("TagTim1");
        mauObjects[1] = GameObject.FindGameObjectWithTag("TagTim2");
        mauObjects[2] = GameObject.FindGameObjectWithTag("TagTim3");

        UpdateScoreText();
        UpdateMauObjects();
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                Jump();
                player.SetBool("dichuyen", true);
                player.SetBool("dungyen", false);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                player.SetBool("dichuyen", true);
                player.SetBool("dungyen", false);

                gameObject.transform.Translate(Vector2.right * speedX * Time.deltaTime);

                if (gameObject.transform.localScale.x < 0)
                {
                    gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                player.SetBool("dichuyen", true);
                player.SetBool("dungyen", false);

                gameObject.transform.Translate(Vector2.left * speedX * Time.deltaTime);

                if (gameObject.transform.localScale.x > 0)
                {
                    gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                }
            }
            else
            {
                player.SetBool("dichuyen", false);
                player.SetBool("dungyen", true);
            }
        }
    }

    void Jump()
    {
        jumpCount++;

        if (jumpCount <= 1)
        {
            player.SetBool("dichuyen", true);
            player.SetBool("dungyen", false);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, speedY);
        }

        if (jumpCount >= 1)
        {
            canJump = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CoinTag")
        {
            score++;
            Destroy(collision.gameObject);
            UpdateScoreText();
        }

        if (collision.gameObject.GetComponent<TilemapCollider2D>() != null)
        {
            jumpCount = 0;
            canJump = true;
        }

        if (collision.gameObject.tag == "DichTag")
        {
            SceneManager.LoadScene("man2");
        }

        if (collision.gameObject.tag == "CNVTag")
        {
            mau--;

            if (mau >= 0 && mau < mauObjects.Length)
            {
                mauObjects[mau].SetActive(false);
            }
        }

        if (collision.gameObject.tag == "TagTimPlus")
        {
            mau++;
         

            if (mau >= 0 && mau < mauObjects.Length)
            {
                mauObjects[mau-1].SetActive(true);
            } 
            Destroy(collision.gameObject);
        }
    }

    void UpdateScoreText()
    {
        txtScore.text = "Score: " + score.ToString();
    }

    void UpdateMauObjects()
    {
        for (int i = 0; i < mauObjects.Length; i++)
        {
            mauObjects[i].SetActive(i < mau);
        }
    }
}