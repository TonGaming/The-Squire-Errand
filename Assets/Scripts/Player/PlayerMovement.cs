
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour // Cẩn thận tên class và tên script phải giống nhau 
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] float jumpForce = 8f;

    // Khai báo biến gravity scale của Ember
    [SerializeField] float baseGravity = 2f;

    // Khai báo âm thanh cho nhân vật
    [SerializeField] AudioSource FootstepsSounds;
    [SerializeField] AudioSource ClimbingSounds;
    [SerializeField] AudioSource BouncingSounds;
    [SerializeField] AudioSource JumpingSounds;


    // Giá trị nhập vào từ bàn phím, thường là 1, -1 ở mỗi trục
    Vector2 moveInput;

    // khai báo biến rigidbody2D
    Rigidbody2D myRigidbody2D;

    // Khai báo biến Animator
    Animator myAnimator;

    // Get Player Living State
    PlayerDeathDetector playerDeathDetector;

    // Khai báo biến Collider2D
    CircleCollider2D myCircleCollider2D;
    CapsuleCollider2D myCapsuleCollider2D;



    // status flag
    bool isGrounded = true;
    bool isClimbable = true;
    bool isWalking = false;
    bool isClimbing = false;

    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.gravityScale = baseGravity;
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myCircleCollider2D = GetComponent<CircleCollider2D>();


        playerDeathDetector = FindAnyObjectByType<PlayerDeathDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        /**
         * Note to self:
         * Phải tạo biến isAlive để check, 
         * nếu chỉ khoá PlayerInput.enabled = false lại không thôi thì các hàm tác động tới X axis và Y axis vẫn sẽ hoạt động
         * từ đó ngăn cản các cú dead kick dc thực thi
         */
        MoveCheck();

        FootstepsSoundPlayer();
        ClimbLadderSoundPlayer();

        if (playerDeathDetector.GetIsAliveState())
        {
            Run();
            FlipSprite();
            groundCheck();

#if UNITY_STANDALONE || UNITY_WEBPLAYER
            ClimbLadder();
#endif
        }
        else { return; }


        //Debug.Log("Y velocity: " + moveInput.y * climbSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D playerCollider = other.otherCollider;

        if (other.gameObject.CompareTag("Mushroom") && playerCollider is CircleCollider2D)
        {
            BouncingSounds.Play();
        }
    }
#if UNITY_STANDALONE || UNITY_WEBPLAYER
    public void OnMove(InputValue value) // Luôn phải tuân thủ tên Hàm là Pascal Case
    {
        // khai báo biến moveInput là kiểu Vector2 
        // sau đó get ra giá trị x y của InputValue sau đó gán ngược vào moveInput 
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        // Nếu đang chạm vào layer ground VÀ phím cách được ấn -> thì mới nhảy, hoặc đang bám thang và ấn cách thì cũng nhảy đc 
        if ((isGrounded && value.isPressed) || (isClimbable && value.isPressed))
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);
            myAnimator.SetTrigger("takeOff");
            isGrounded = false;
            JumpingSounds.Play();
        }
        else if (!isGrounded && value.isPressed)
        {
            return;
        }
    }
#endif

#if (UNITY_ANDROID || UNITY_IOS)
    public void OnMove(Vector2 value) // Luôn phải tuân thủ tên Hàm là Pascal Case
    {
        // khai báo biến moveInput là kiểu Vector2 
        // sau đó get ra giá trị x y của InputValue sau đó gán ngược vào moveInput 
        moveInput = value;
    }

    public void OnJump(InputValue value)
    {
        // Nếu đang chạm vào layer ground VÀ phím cách được ấn -> thì mới nhảy, hoặc đang bám thang và ấn cách thì cũng nhảy đc 
        if ((isGrounded && value.isPressed) || (isClimbable && value.isPressed))
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);
            myAnimator.SetTrigger("takeOff");
            isGrounded = false;
            JumpingSounds.Play();
        }
        else if (!isGrounded && value.isPressed)
        {
            return;
        }
    }
#endif
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
        // giữ nguyên k thay đổi để tránh hiện tượng phản trọng lực, bay lơ lửng.
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

    void FootstepsSoundPlayer()
    {
        if (isGrounded && Mathf.Abs(myRigidbody2D.velocity.x) >= 1 && isWalking == false && playerDeathDetector.GetIsAliveState() )
        {
            isWalking = true;
            FootstepsSounds.Play();

        }
        else if (!isGrounded 
            || !(Mathf.Abs(myRigidbody2D.velocity.x) >= 1) 
            
            || playerDeathDetector.GetIsAliveState() == false)
        {
            FootstepsSounds.Stop();
            isWalking = false;

        }
    }

#if UNITY_STANDALONE || UNITY_WEBPLAYER
    void ClimbLadder()
    {
        if (myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            // báo xem có climb-able hay k
            isClimbable = true;
            //myRigidbody2D.gravityScale = 0;
            myAnimator.SetBool("isJumping", false);
            myRigidbody2D.gravityScale = 0;


            Vector2 climbingInput = new Vector2(myRigidbody2D.velocity.x, moveInput.y * climbSpeed);
            myRigidbody2D.velocity = climbingInput;

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

        // Ladder Animation
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

    }

#endif

#if (UNITY_ANDROID || UNITY_IOS)
    
    public void ClimbLadder(Vector2 climbingInput)
    {
        if (myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            // báo xem có climb-able hay k
            isClimbable = true;
            //myRigidbody2D.gravityScale = 0;
            myAnimator.SetBool("isJumping", false);
            myRigidbody2D.gravityScale = 0;

            // lấy giá trị truyền vào và nhân với climbSpeed
            climbingInput = new Vector2(myRigidbody2D.velocity.x, moveInput.y * climbSpeed);

            myRigidbody2D.velocity = climbingInput;

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

        // Ladder Animation
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

    }



#endif
    void ClimbLadderSoundPlayer()
    {
        if (isClimbable && Mathf.Abs(myRigidbody2D.velocity.y) >= Mathf.Epsilon
            
            && isClimbing == false)
        {
            isClimbing = true;
            ClimbingSounds.Play();

        }
        else if (!isClimbable || !(Mathf.Abs(myRigidbody2D.velocity.y) >= Mathf.Epsilon) 
            || playerDeathDetector.GetIsAliveState() == false 

            )
        {
            isClimbing = false;
            ClimbingSounds.Stop();

        }
    }

    



    /***
     * Date: 08/09/2023
     * Author: VDDung
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

    public bool GetClimbableState()
    {
        return isClimbable;
    }

    public bool GetGroundedState()
    {
        return isGrounded;
    }

    void MoveCheck()
    {
        if (Mathf.Abs(myRigidbody2D.velocity.x) >= 1 || Mathf.Abs(myRigidbody2D.velocity.y) >= 1)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    
}
