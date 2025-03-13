using System.Collections;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject player; // �÷��̾� ������Ʈ
    public GameObject block;  // ��� ������Ʈ
    private int stepCount = 0; // �÷��̾ ����� ���� Ƚ��
    public float respawnTime = 10f; // ����� �ٽ� ��Ÿ�� �ð� (��)
    public float blinkDuration = 0.2f; // �����̴� ���� �ð�
    public int blinkCount = 2; // �����̴� Ƚ��

    private Renderer blockRenderer; // ����� ������
    private Collider2D blockCollider; // ����� Collider2D

    public float topColliderOffset = 0.1f; // ��� ���κ��� �浹 ���� ������

    private void Start()
    {
        // ����� Renderer �� Collider2D ������Ʈ ��������
        blockRenderer = block.GetComponent<Renderer>();
        blockCollider = block.GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾�� ����� �浹�� ����
        if (collision.gameObject == player)
        {
            // �浹 ������ ����� ���κ����� Ȯ��
            Vector2 contactPoint = collision.contacts[0].point;
            if (contactPoint.y > block.transform.position.y + blockCollider.bounds.size.y / 2 - topColliderOffset)
            {
                stepCount++; // ���κп� ������ ���� Ƚ�� ����

                // 1�� ����� �� ������ ��Ȳ������ ���� (RGB: R=1, G=0.647, B=0)
                if (stepCount == 1)
                {
                    blockRenderer.material.color = new Color(1f, 0.647f, 0f); // ��Ȳ��
                }

                // 2�� ����� �� ������ ���������� ���� (RGB: R=1, G=0, B=0)
                else if (stepCount == 2)
                {
                    blockRenderer.material.color = new Color(1f, 0f, 0f); // ������
                }

                // 3�� ����� �� ����� ������� �ϰ� �ٽ� ��Ÿ���� ��
                if (stepCount >= 3)
                {
                    stepCount = 0; // ī��Ʈ �ʱ�ȭ
                    StartCoroutine(HandleBlockRespawn()); // ��� ������ٰ� �ٽ� ��Ÿ���� ó��
                }
            }
        }
    }

    private IEnumerator HandleBlockRespawn()
    {
        // ����� ������� ��
        blockRenderer.enabled = false;
        blockCollider.enabled = false; // ����� ������� �浹�� ��Ȱ��ȭ

        // ���� �ð� ��� (10��)
        yield return new WaitForSeconds(respawnTime);

        // ����� �ٽ� ��Ÿ���� ��
        blockRenderer.enabled = true;
        blockCollider.enabled = true; // ����� �ٽ� ��Ÿ���� �浹 Ȱ��ȭ

        // ��������� �����̴� ȿ��: �� �� ������
        for (int i = 0; i < blinkCount; i++)
        {
            // ����� ������ ������ ���� (50% ����)
            blockRenderer.material.color = new Color(1f, 1f, 0f, 0.5f); // ����� ������

            // �����̴� �ð����� ���
            yield return new WaitForSeconds(blinkDuration);

            // ������ ��������� �ǵ�����
            blockRenderer.material.color = new Color(1f, 1f, 0f, 1f); // ����� ������

            // �����̴� �ð����� ���
            yield return new WaitForSeconds(blinkDuration);
        }

        // ��� ���� �ʱ�ȭ (�⺻ �������� ����)
        blockRenderer.material.color = Color.white; // �⺻ ������ �Ͼ�� (R=1, G=1, B=1)
    }
}
