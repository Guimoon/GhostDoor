using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimbing : MonoBehaviour
{
    [Header("Wall Climbing Settings")]
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private float wallCheckDistance = 0.6f;
    [SerializeField] private LayerMask wallLayer;
    
    private bool isWallDetected = false;
    private float originalGravity;
    
    private Rigidbody2D rb;
    private Animator animator; // 애니메이터가 있다면 사용
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalGravity = rb.gravityScale;
    }
    
    void Update()
    {
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
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
    
    void CheckForWall()
    {
        // 캐릭터 앞에 벽이 있는지 확인
        Vector2 direction = !spriteRenderer.flipX ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, wallCheckDistance, wallLayer);
        
        // 디버그 레이로 확인 (게임 뷰에서 볼 수 있음)
        Debug.DrawRay(transform.position, direction * wallCheckDistance, Color.red);
        
        isWallDetected = hit.collider != null;
    }
    
    void HandleClimbing()
    {
        // 벽을 오르는 중이라면 중력을 0으로 설정
        rb.gravityScale = 0;
        
        // 위/아래 입력에 따라 이동
        float verticalInput = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(0, verticalInput * climbSpeed);
        
        // 애니메이션 재생 (애니메이터가 있는 경우)
        // if (animator != null)
        // {
        //     animator.SetBool("IsClimbing", true);
        // }
    }
    
    void StopClimbing()
    {
        // 벽을 오르지 않는 상태라면 원래 중력으로 복원
        rb.gravityScale = originalGravity;
        
        // 애니메이션 중지 (애니메이터가 있는 경우)
        // if (animator != null)
        // {
        //     animator.SetBool("IsClimbing", false);
        // }
    }
    
    // 에디터에서 벽 감지 범위를 시각적으로 표시
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
        Gizmos.DrawLine(transform.position, transform.position + direction * wallCheckDistance);
    }
}