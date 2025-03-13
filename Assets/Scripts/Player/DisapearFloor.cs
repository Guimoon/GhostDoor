using System.Collections;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject player; // 플레이어 오브젝트
    public GameObject block;  // 블록 오브젝트
    private int stepCount = 0; // 플레이어가 블록을 밟은 횟수
    public float respawnTime = 10f; // 블록이 다시 나타날 시간 (초)
    public float blinkDuration = 0.2f; // 깜빡이는 지속 시간
    public int blinkCount = 2; // 깜빡이는 횟수

    private Renderer blockRenderer; // 블록의 렌더러
    private Collider2D blockCollider; // 블록의 Collider2D

    public float topColliderOffset = 0.1f; // 블록 윗부분의 충돌 영역 오프셋

    private void Start()
    {
        // 블록의 Renderer 및 Collider2D 컴포넌트 가져오기
        blockRenderer = block.GetComponent<Renderer>();
        blockCollider = block.GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어와 블록의 충돌을 감지
        if (collision.gameObject == player)
        {
            // 충돌 지점이 블록의 윗부분인지 확인
            Vector2 contactPoint = collision.contacts[0].point;
            if (contactPoint.y > block.transform.position.y + blockCollider.bounds.size.y / 2 - topColliderOffset)
            {
                stepCount++; // 윗부분에 닿으면 밟은 횟수 증가

                // 1번 밟았을 때 색상을 주황색으로 변경 (RGB: R=1, G=0.647, B=0)
                if (stepCount == 1)
                {
                    blockRenderer.material.color = new Color(1f, 0.647f, 0f); // 주황색
                }

                // 2번 밟았을 때 색상을 빨간색으로 변경 (RGB: R=1, G=0, B=0)
                else if (stepCount == 2)
                {
                    blockRenderer.material.color = new Color(1f, 0f, 0f); // 빨간색
                }

                // 3번 밟았을 때 블록을 사라지게 하고 다시 나타나게 함
                if (stepCount >= 3)
                {
                    stepCount = 0; // 카운트 초기화
                    StartCoroutine(HandleBlockRespawn()); // 블록 사라졌다가 다시 나타나게 처리
                }
            }
        }
    }

    private IEnumerator HandleBlockRespawn()
    {
        // 블록을 사라지게 함
        blockRenderer.enabled = false;
        blockCollider.enabled = false; // 블록이 사라지면 충돌도 비활성화

        // 일정 시간 대기 (10초)
        yield return new WaitForSeconds(respawnTime);

        // 블록을 다시 나타나게 함
        blockRenderer.enabled = true;
        blockCollider.enabled = true; // 블록이 다시 나타나면 충돌 활성화

        // 노란색으로 깜빡이는 효과: 두 번 깜빡임
        for (int i = 0; i < blinkCount; i++)
        {
            // 노란색 반투명 색으로 변경 (50% 투명도)
            blockRenderer.material.color = new Color(1f, 1f, 0f, 0.5f); // 노란색 반투명

            // 깜빡이는 시간동안 대기
            yield return new WaitForSeconds(blinkDuration);

            // 불투명 노란색으로 되돌리기
            blockRenderer.material.color = new Color(1f, 1f, 0f, 1f); // 노란색 불투명

            // 깜빡이는 시간동안 대기
            yield return new WaitForSeconds(blinkDuration);
        }

        // 블록 색상 초기화 (기본 색상으로 복귀)
        blockRenderer.material.color = Color.white; // 기본 색상은 하얀색 (R=1, G=1, B=1)
    }
}
