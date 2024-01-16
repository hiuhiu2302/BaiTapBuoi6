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

    int mau = 3;

    int score = 0;
    public Text txtScore;
    void Start()
    {
        //ánh xạ điểm 
        txtScore = GameObject.Find("txtDiem").GetComponent<Text>();

        player = GetComponent<Animator>();
        isGameOver = false;

        // Thiết lập tham số trạng thái
        player.SetBool("dichuyen", false);
        player.SetBool("dungyen", true);
    }

   

    void Update()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canJump) // Sự kiện xảy ra khi nút Space được nhấn xuống và có thể nhảy
            {
                Jump();
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
        jumpCount++; // Tăng biến đếm số lần nhảy

        if (jumpCount <= 1) // Giới hạn số lần nhảy
        {
            // Thực hiện hành động nhảy của nhân vật người chơi
            player.SetBool("dichuyen", true);
            player.SetBool("dungyen", false);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, speedY);
        }

        if (jumpCount >= 1) // Nếu đã nhảy đủ 2 lần, không cho phép nhảy nữa
        {
            canJump = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //sử lí va chạm coin
        if (collision.gameObject.tag == "CoinTag")
        {
            score++;//tăng điểm
            Destroy(collision.gameObject);//hủy coin
            txtScore.text = "Score:" + score.ToString();
        }

        if (collision.gameObject.tag=="CNVTag")
        {
            // Trừ điểm

            mau--;
            if (mau == 2)
            {
                GameObject mauObject = GameObject.FindGameObjectWithTag("TagTim3");
                if (mauObject != null)
                {
                    Destroy(mauObject);
                }
            }else if (mau == 1)
            {
                GameObject mauObject = GameObject.FindGameObjectWithTag("TagTim2");
                if (mauObject != null)
                {
                    Destroy(mauObject);
                }
            }else if (mau == 0)
            {
                GameObject mauObject = GameObject.FindGameObjectWithTag("TagTim1");
                if (mauObject != null)
                {
                    Destroy(mauObject);
                }

            }

           
        }

        // Kiểm tra nếu có va chạm với mặt đất (hoặc nền tảng khác), reset số lần nhảy và cho phép nhảy lại
        if (collision.gameObject.GetComponent<TilemapCollider2D>() != null)
        {
            jumpCount = 0;
            canJump = true;
        }


        if (collision.gameObject.tag == "DichTag")
        {
            SceneManager.LoadScene("man2");
        }
    }
}