using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EstUse : MonoBehaviour
{
    public Slider healthEst;
    public Button healEstButton;
    public float healIncreaseAmount = 30f;

    //public Slider attackEst;
    //public Button attackEstButton;
    //public float attackIncreaseAmount = 10f;

    public float estRemain = 4f;

    //public float cooldownTime = 60f; // ���� ����Ʈ ��ư ���� ��� �ð� (��)
    //public float attackDuration = 10f; // ���ݷ� ���� ���� �ð� (��)

   // private bool isAttackCooldown = false; // ���� ����Ʈ ��ư�� ��ٿ� ������ Ȯ��
   // private bool isAttackActive = false; // ���� ���� ȿ���� Ȱ��ȭ�Ǿ����� Ȯ��

    void Start()
    {
        healEstButton.onClick.AddListener(IncreaseHeal);
        //attackEstButton.onClick.AddListener(IncreaseAttack);

        UpdateButtons();
    }

    void IncreaseHeal()
    {
        if (estRemain > 0)
        {
            healthEst.value = Mathf.Clamp(healthEst.value + healIncreaseAmount, healthEst.minValue, healthEst.maxValue);
            UseEstus();
        }
    }

    /*
    void IncreaseAttack()
    {
        if (estRemain > 0 && !isAttackCooldown && !isAttackActive)
        {
            attackEst.value = Mathf.Clamp(attackEst.value + attackIncreaseAmount, attackEst.minValue, attackEst.maxValue);
            UseEstus();
            StartCoroutine(AttackEffectDuration()); // ���ӽð� ����
            StartCoroutine(AttackCooldown()); // ��ٿ� ����
        }
    }
    */

    void UseEstus()
    {
        estRemain--;
        Debug.Log("���� ����Ʈ ����: " + estRemain);

        UpdateButtons();
    }

    void UpdateButtons()
    {
        // ����Ʈ ������ 0���� ��� ��ư ��Ȱ��ȭ
        if (estRemain <= 0)
        {
            healEstButton.interactable = false;
            //attackEstButton.interactable = false;
            Debug.Log("����Ʈ�� �� ����߽��ϴ�!");
            return; // 0���� ���⼭ �Լ� ����
        }

        /*
        // ��ٿ� ���̸� ���� ��ư ��Ȱ��ȭ
        if (isAttackCooldown)
        {
            attackEstButton.interactable = false;
        }
        else
        {
            attackEstButton.interactable = true;
        }
        */
    }

    /*
    IEnumerator AttackEffectDuration()
    {
        isAttackActive = true;
        Debug.Log("���ݷ��� �����߽��ϴ�! ���� �ð�: " + attackDuration + "��");

        yield return new WaitForSeconds(attackDuration);

        attackEst.value = Mathf.Clamp(attackEst.value - attackIncreaseAmount, attackEst.minValue, attackEst.maxValue);
        isAttackActive = false;
        Debug.Log("���ݷ� ���� ȿ���� ��������ϴ�.");
    }

    IEnumerator AttackCooldown()
    {
        isAttackCooldown = true;
        attackEstButton.interactable = false;

        float remainingTime = cooldownTime;

        while (remainingTime > 0)
        {
            if (remainingTime % 10 == 0)
            {
                Debug.Log("���� ����Ʈ ������� ���� �ð�: " + remainingTime + "��");
            }

            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        isAttackCooldown = false;
        UpdateButtons(); // ��ư ���� �ٽ� Ȯ��
        Debug.Log("���� ����Ʈ�� �ٽ� ����� �� �ֽ��ϴ�!");
    }
    */
}
