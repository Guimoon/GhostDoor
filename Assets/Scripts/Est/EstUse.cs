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

    //public float cooldownTime = 60f; // 공격 에스트 버튼 재사용 대기 시간 (초)
    //public float attackDuration = 10f; // 공격력 증가 지속 시간 (초)

   // private bool isAttackCooldown = false; // 공격 에스트 버튼이 쿨다운 중인지 확인
   // private bool isAttackActive = false; // 공격 증가 효과가 활성화되었는지 확인

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
            StartCoroutine(AttackEffectDuration()); // 지속시간 적용
            StartCoroutine(AttackCooldown()); // 쿨다운 시작
        }
    }
    */

    void UseEstus()
    {
        estRemain--;
        Debug.Log("남은 에스트 개수: " + estRemain);

        UpdateButtons();
    }

    void UpdateButtons()
    {
        // 에스트 개수가 0개면 모든 버튼 비활성화
        if (estRemain <= 0)
        {
            healEstButton.interactable = false;
            //attackEstButton.interactable = false;
            Debug.Log("에스트를 다 사용했습니다!");
            return; // 0개면 여기서 함수 종료
        }

        /*
        // 쿨다운 중이면 공격 버튼 비활성화
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
        Debug.Log("공격력이 증가했습니다! 지속 시간: " + attackDuration + "초");

        yield return new WaitForSeconds(attackDuration);

        attackEst.value = Mathf.Clamp(attackEst.value - attackIncreaseAmount, attackEst.minValue, attackEst.maxValue);
        isAttackActive = false;
        Debug.Log("공격력 증가 효과가 사라졌습니다.");
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
                Debug.Log("공격 에스트 재사용까지 남은 시간: " + remainingTime + "초");
            }

            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        isAttackCooldown = false;
        UpdateButtons(); // 버튼 상태 다시 확인
        Debug.Log("공격 에스트를 다시 사용할 수 있습니다!");
    }
    */
}
