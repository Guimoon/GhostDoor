using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class HeroKnight : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    //------------------------------------------------- //벽 타기
    [Header("Wall Climbing Settings")]
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private float wallCheckDistance = 0.6f;
    [SerializeField] private LayerMask wallLayer;
    //-------------------------------------------------
    public Slider hpBar; // 체력 UI 슬라이더
    private bool Alive = true;
    //-------------------------------------------------
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private SpriteRenderer m_spriteRenderer; //spriteRenderer
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;

    private bool isWallDetected = false;
    private float originalGravity;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>(); //spriteRenderer
        originalGravity = m_body2d.gravityScale;
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }

        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling)
            m_body2d.linearVelocity = new Vector2(inputX * m_speed, m_body2d.linearVelocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.linearVelocity.y);

        // -- Handle Animations --
        // //Wall Slide
        // m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        // m_animator.SetBool("WallSlide", m_isWallSliding);

        //Death
        if (hpBar.value == hpBar.minValue && !m_rolling && Alive)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
            Alive = !Alive;
        }

        if (Input.GetKeyDown("r"))
        {
            hpBar.value = hpBar.minValue;
        }

        // //Hurt
        // else if (Input.GetKeyDown("q") && !m_rolling)
        //     m_animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetKeyDown("m") && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // // Roll
        // else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        // {
        //     m_rolling = true;
        //     m_animator.SetTrigger("Roll");
        //     m_body2d.linearVelocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.linearVelocity.y);
        // }


        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.linearVelocity = new Vector2(m_body2d.linearVelocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }

        CheckForWall();

        if (isWallDetected)
        {
            HandleClimbing();
        }
        else
        {
            StopClimbing();
        }

        if (isWallDetected && Input.GetKeyDown("space"))
        {
            isWallDetected = !isWallDetected;
            m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
            m_body2d.linearVelocity = new Vector2(m_body2d.linearVelocity.x, m_jumpForce);
        }

    }

    void CheckForWall()
    {
        // 캐릭터 앞에 벽이 있는지 확인
        Vector2 direction = !m_spriteRenderer.flipX ? Vector2.right : Vector2.left;
        
        // y값을 -0.3만큼 낮추어 시작 위치를 조정
        Vector2 startPosition = new Vector2(transform.position.x, transform.position.y - 0.1f);
        
        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, wallCheckDistance, wallLayer);
        
        // 디버그 레이로 확인 (게임 뷰에서 볼 수 있음)
        Debug.DrawRay(startPosition, direction * wallCheckDistance, Color.red);
        
        isWallDetected = hit.collider != null;
    }

    void HandleClimbing()
    {
        // 벽을 오르는 중이라면 중력을 0으로 설정
        m_body2d.gravityScale = 0;
        
        // 위/아래 입력에 따라 이동
        float verticalInput = Input.GetAxis("Vertical");
        m_body2d.linearVelocity = new Vector2(0, verticalInput * climbSpeed);
        
        // 애니메이션 재생 (애니메이터가 있는 경우)
        // if (animator != null)
        // {
        //     animator.SetBool("IsClimbing", true);
        // }
    }
    
    void StopClimbing()
    {
        // 벽을 오르지 않는 상태라면 원래 중력으로 복원
        m_body2d.gravityScale = originalGravity;
        
        // 애니메이션 중지 (애니메이터가 있는 경우)
        // if (animator != null)
        // {
        //     animator.SetBool("IsClimbing", false);
        // }
    }

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
}
