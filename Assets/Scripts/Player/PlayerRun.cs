using UnityEngine;

public class PlayerMove1 : MonoBehaviour
{
    public float maxSpeed; // 최대 속도 제한
    public float jumpPower; // 점프 힘
    public float fallMultiplier = 2.5f; // 낙하 속도 조절 값
    Rigidbody2D rigid; // Rigidbody2D 변수 선언
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        rigid.gravityScale = 2f; // 기본 중력 증가
    }

    void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
        }

        // 방향 전환 (스프라이트 반전)
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        // 애니메이션 처리
        if (Mathf.Abs(rigid.linearVelocityX) < 0.3f)
        {
            anim.SetBool("Run", false);
        }
        else
        {
            anim.SetBool("Run", true);
        }
    }

    void FixedUpdate()
    {
        // 빠른 낙하 적용
        if (rigid.linearVelocityY < 0)
        {
            rigid.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        // 수평 입력 값 받기
        float h = Input.GetAxisRaw("Horizontal");

        if (h != 0)
        {
            // 가속도 적용
            rigid.linearVelocity = new Vector2(h * maxSpeed, rigid.linearVelocityY); // X축 속도만 조정
        }
        else
        {
            // 입력이 없으면 X축 속도를 0으로 설정
            rigid.linearVelocity = new Vector2(0, rigid.linearVelocityY);
        }

        // 최대 속도 제한 (좌우 속도 제한)
        rigid.linearVelocity = new Vector2(Mathf.Clamp(rigid.linearVelocityX, -maxSpeed, maxSpeed), rigid.linearVelocityY);

        //점프 애니메이션
        if (rigid.linearVelocityY < 0) //내려갈떄만 스캔
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("Jump", false);
                }
            }
        }
    }
}