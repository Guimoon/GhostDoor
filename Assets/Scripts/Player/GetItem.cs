using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� �Ҵ��ؾ� �մϴ�.
    public float itemDetectRange = 1f; // ���� ���� (����: ����Ƽ �Ÿ� ����)
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
