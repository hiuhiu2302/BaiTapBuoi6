using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speedX, speedY;
    public float doubleJumpDelay = 0.5f; // Khoảng thời gian cho phép nhảy liên tục
    public GameObject panelEndGame;
    private Animator player;
    private bool isJumping = false;
    private bool isDoubleJumping = false;
    private bool isWaitingForDoubleJump = false;
    private float doubleJumpTimer = 0f;

    Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GetComponent<Animator>();
    }

    public void Jump()
    {
        if (!isJumping)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speedY);
            isJumping = true;
            isDoubleJumping = false;
            isWaitingForDoubleJump = true;
            doubleJumpTimer = 0f;
            player.SetTrigger("isjump");
        }
        else if (isWaitingForDoubleJump && doubleJumpTimer <= doubleJumpDelay)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speedY);
            isDoubleJumping = true;
            isWaitingForDoubleJump = false;
            player.SetTrigger("doublejump");
        }
    }

    public void Chaytrai()
    {
        gameObject.transform.Translate(Vector2.left * speedX * Time.deltaTime);

        if (gameObject.transform.localScale.x > 0)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }
        player.SetBool("isrun", true);
        player.SetBool("isidle", false);
        player.SetBool("isjump", isJumping);
        player.SetBool("doublejump", isDoubleJumping);
    }

    public void Chayphai()
    {
        gameObject.transform.Translate(Vector2.right * speedX * Time.deltaTime);

        if (gameObject.transform.localScale.x < 0)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }
        player.SetBool("isrun", true);
        player.SetBool("isidle", false);
        player.SetBool("isjump", isJumping);
        player.SetBool("doublejump", isDoubleJumping);
    }

    public void RestartGame()
    {
        panelEndGame.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "obstacles")
        {
            panelEndGame.SetActive(true);
            Time.timeScale = 0;
        }

        // Reset trạng thái nhảy khi va chạm với mặt đất
        isJumping = false;
        isDoubleJumping = false;
        isWaitingForDoubleJump = false;
        player.SetBool("isjump", false);
        player.SetBool("doublejump", false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Chayphai();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Chaytrai();
        }
        else
        {
            player.SetBool("isrun", false);
            player.SetBool("isjump", isJumping);
            player.SetBool("doublejump", isDoubleJumping);
            player.SetBool("isidle", true);
        }

        // Cập nhật doubleJumpTimer nếu đang chờ nhảy liên tục
        if (isWaitingForDoubleJump)
        {
            doubleJumpTimer += Time.deltaTime;
            if (doubleJumpTimer > doubleJumpDelay)
            {
                isWaitingForDoubleJump = false;
            }
        }
    }
}