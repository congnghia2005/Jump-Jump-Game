using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private bool isGround;
    private Rigidbody2D rb;  
    private GameManage gameManage;  
    private AudioManager audioManager;
    void Awake()
    {
        animator= GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManage=FindAnyObjectByType<GameManage>();
        audioManager=FindAnyObjectByType<AudioManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManage.IsGameOver() || gameManage.IsGameWin()) return;
        hanldeMovement();
        handleJump();
        UpdateAnimation();
    }

    private void hanldeMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput*moveSpeed, rb.linearVelocity.y);
        if(moveInput>0) transform.localScale = new Vector3 (1,1,1);
        else if (moveInput<0) transform.localScale = new Vector3 (-1,1,1);
    }

    private void handleJump()
    {
        if (Input.GetButtonDown("Jump")&&isGround)
        {
            audioManager.PlayJumpSound();
            rb.linearVelocity = new Vector2 (rb.linearVelocity.x,jumpForce);
        }
        isGround=Physics2D.OverlapCircle(groundCheck.position,0.2f,groundLayer);
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGround;
        animator.SetBool("isRunning",isRunning);
        animator.SetBool("isJumping",isJumping);
    }
}
