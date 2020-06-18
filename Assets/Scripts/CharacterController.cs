using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject bomb;
    private int Force = 500;
    List<GameObject> bombs = new List<GameObject>();

    Animator animator;
    Rigidbody2D rb;

    [SerializeField]
    int speed;

    [SerializeField]
    int jumpforce;

    [SerializeField]
    Transform groundCheck;

    [SerializeField] GameObject canvas;

    bool isGrounded = true;

    private bool isFacingRight = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Physics2D.Linecast(new Vector2(transform.position.x -7, transform.position.y), groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else isGrounded = false;
        if (Input.GetKey(KeyCode.D))
        {
            if (!isFacingRight)
                Flip();

            rb.velocity = new Vector2(speed, rb.velocity.y);
            if(isGrounded)
                animator.Play("Player_Run");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if(isFacingRight)
            {
                Flip();
                isFacingRight = false;
            }
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (isGrounded)
                animator.Play("Player_Run");
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.Play("Player_Idle");
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            animator.Play("Player_Jump");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject bombGameObj = Instantiate(bomb, transform.position, Quaternion.identity);
            bombGameObj.transform.SetParent(canvas.transform);
            bombGameObj.transform.localScale = new Vector3(1, 1, 1);
           // bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000, 1000), ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Довнерский код
    /// </summary>
    private void Flip()
    {
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
}
