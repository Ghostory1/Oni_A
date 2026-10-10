using NUnit.Framework.Constraints;
using System;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }


    public PlayerInputSet input { get; private set; }
    private StateMachine stateMachine;
    private Coroutine attackQueueCo;
    public  Player_IdleState idleState { get; private set;  }
    public  Player_MoveState moveState { get; private set; }
    public  Player_JumpState jumpState { get; private set; }
    public  Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set;  }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }
    public Vector2 moveInput { get; private set; }
    public Vector2 wallJumpForce;

    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = 0.1f;
    public float comboResetTime = 1f;

    [Header("Movement details")]
    public float moveSpeed;
    public float jumpForce = 5f;
    [Range(0, 1)]
    public float inAirMoveMultiplier = 0.7f; 
    [Range(0, 1)]
    public float wallSlideMultiplier = 0.7f;
    [Space]
    public float dashDuration = .25f;
    public float dashSpeed = 20f;

    public bool facingRight { get; private set; } = true;
    public int facingDir { get; private set; } = 1;

    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance = 0.55f;
    [SerializeField] private LayerMask whatIsGround;
    public bool groundDetected;
    public bool wallDetected;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this,stateMachine,"idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
    }
    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }
    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }
    public void EnterAttackStateWithDelay()
    {
        if (attackQueueCo != null)
            StopCoroutine(attackQueueCo);
        attackQueueCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }
    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield  return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }
    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }
    public void SetVelocity(float xVelocity,float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity,yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if (rb.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.linearVelocity.x < 0 && facingRight)
        {
            Flip();
        }
    }
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }
    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,transform.position + new Vector3(0,-groundCheckDistance,0));
        Gizmos.DrawLine(transform.position + new Vector3(0,0.5f,0), transform.position + new Vector3(wallCheckDistance * facingDir, 0.5f,0));
    }
}
