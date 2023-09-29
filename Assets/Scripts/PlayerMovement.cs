
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour // Cẩn thận tên class và tên script phải giống nhau 
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] float jumpForce = 8f;


    // Giá trị nhập vào từ bàn phím, thường là 1, -1 ở mỗi trục
    Vector2 moveInput;

    // khai báo biến rigidbody2D
    Rigidbody2D myRigidbody2D;

    // Khai báo biến Animator
    Animator myAnimator;

    // Khai báo biến Collider2D
    CircleCollider2D myCircleCollider2D;
    CapsuleCollider2D myCapsuleCollider2D;
    BoxCollider2D myBoxCollider2D;

    // Khai báo biến gravity scale của Ember
    [SerializeField] float baseGravity = 1f;

    // status flag
    bool isGrounded = true;
    bool isClimbable = true;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.gravityScale = baseGravity;
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myCircleCollider2D = GetComponent<CircleCollider2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();


    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        groundCheck();
        ClimbLadder();

        //Debug.Log("Y velocity: " + moveInput.y * climbSpeed);
    }

    void OnMove(InputValue value) // Luôn phải tuân thủ tên Hàm là Pascal Case
    {
        // khai báo biến moveInput là kiểu Vector2 
        // sau đó get ra giá trị x y của InputValue sau đó gán ngược vào moveInput 
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        // Nếu đang chạm vào layer ground VÀ phím cách được ấn -> thì mới nhảy, hoặc đang bám thang và ấn cách thì cũng nhảy đc 
        if ((isGrounded && value.isPressed) || (isClimbable && value.isPressed))
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);
            myAnimator.SetTrigger("takeOff");
            isGrounded = false;

        }
        else if (!isGrounded && value.isPressed)
        {
            return;
        }
    }

    //void OnFire(InputValue value)
    //{

    //    Instantiate(bullet, myGun.position, transform.rotation);

    //}

    void groundCheck()
    {
        if (myCircleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
            // chạy animation landing sau đó mới sang animation action
            myAnimator.SetBool("isJumping", false);
            myAnimator.SetBool("isClimbing", false);
        }
        else if (!myCircleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground", "Ladder")))
        {
            isClimbable = false;
            isGrounded = false;
            myAnimator.SetBool("isJumping", true);
            myAnimator.SetBool("isRunning", false);
            myAnimator.SetBool("isClimbing", false);
            return;
        }

    }


    void Run()
    {
        // Tạo Vector lưu hướng di chuyển của ng chơi, gán giá trị X, dựa trên moveInput vào. Giá trị Y thì
        //     giữ nguyên k thay đổi để tránh hiện tượng phản trọng lực, bay lơ lửng.
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;

        if (isGrounded) // ground check
        {
            // Nếu trị tuyệt đối của vận tốc > Epsi thì chạy animation running
            if (Mathf.Abs(myRigidbody2D.velocity.x) >= Mathf.Epsilon)
            {
                myAnimator.SetBool("isRunning", true);
                myAnimator.SetBool("isIdling", false);
            }
            else // còn nếu không(đứng im) thì cho đứng im = cách tắt animation running đi 
            {
                myAnimator.SetBool("isRunning", false);
                myAnimator.SetBool("isIdling", true);
            }

        }
        /** Hoẵc có thể code như sau cho clean
         * bool isRunning = enemyRigidbody.velocity.x >= Mathf.Epsilon;
         * myAnimator.SetBool("isRunning", isRunning); true false dựa theo biến bool luôn 
         */
    }

    void ClimbLadder()
    {
        if (myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            // báo xem có climb-able hay k
            isClimbable = true;
            myRigidbody2D.gravityScale = 0;
            myAnimator.SetBool("isJumping", false);

            Vector2 climbingInput = new Vector2(myRigidbody2D.velocity.x, moveInput.y * climbSpeed);
            myRigidbody2D.velocity = climbingInput;

            // cho phép trèo thang hướng lên chỉ khi đầu chạm vào thang
            //if (Input.GetKey(KeyCode.W) && !myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
            //{
            //    enemyRigidbody.velocity = new Vector2(enemyRigidbody.velocity.x, 0f);

            //    Debug.Log("Y velocity is: " + enemyRigidbody.velocity.y);
            //}
            //else { return; }


        }
        else if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            // nếu k chạm vào thang, thì k thể climb
            isClimbable = false;
            myAnimator.SetBool("isClimbing", false);
            myAnimator.SetBool("isHanging", false);
            myRigidbody2D.gravityScale = baseGravity;
        }
        else { return; }

        // Laadder Animation
        // nếu chạm thang k chạm sàn và di chuyển trên trục Y thì chạy animation đang trèo
        if (isClimbable && (isGrounded || !isGrounded) && Mathf.Abs(myRigidbody2D.velocity.y) >= Mathf.Epsilon)
        {

            myAnimator.SetBool("isClimbing", true);
            myAnimator.SetBool("isHanging", false);
            myAnimator.SetBool("isRunning", false);
        }
        else if (isClimbable && Mathf.Abs(myRigidbody2D.velocity.y) <= Mathf.Epsilon)
        {
            myAnimator.SetBool("isHanging", true);
            myAnimator.SetBool("isClimbing", false);
        }
        else { return; }



        //else if (!isClimbable && !isGrounded)
        //{
        //    myAnimator.SetBool("isClimbing", false);
        //    myAnimator.SetBool("isRunning", false);

        //    myAnimator.SetTrigger("takeOff");
        //    myAnimator.SetBool("isJumping", true);
        //}


    }



    /***
     * Author: VDDung
     * Date: 08/09/2023
     * Added Function Flip Sprite
     */
    void FlipSprite()
    {
        // Biến check xem có đang di chuyển hay không, vận tốc -1 hay 1 thì vẫn là movement nên phải dùng Abs,
        bool playerIsMovingHorizontal = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

        // Nếu velocity = 0 thì Mathf.Sign cũng trả về +1, từ đó flip sprite lại bên phải
        // Vì vậy nên phải tạo biến bool
        if (playerIsMovingHorizontal)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }



}
