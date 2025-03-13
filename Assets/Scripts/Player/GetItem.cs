using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 할당해야 합니다.
    public float itemDetectRange = 1f; // 감지 범위 (단위: 유니티 거리 단위)
    public GameObject item1;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < itemDetectRange && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(item1);
        }
    }
}
