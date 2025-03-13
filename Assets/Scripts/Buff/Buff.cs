using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Buff : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Slider attackEst;
    public Button attackEstButton;
    public Image attackBuffIcon; // 공격 버프 아이콘
    public Text tooltipText; // 툴팁 텍스트
    public float attackIncreaseAmount = 10f;

    public float cooldownTime = 60f; // 공격 에스트 버튼 재사용 대기 시간 (초)
    public float attackDuration = 10f; // 공격력 증가 지속 시간 (초)

    private bool isAttackCooldown = false; // 공격 에스트 버튼이 쿨다운 중인지 확인
    private bool isAttackActive = false; // 공격 증가 효과가 활성화되었는지 확인

    void Start()
    {
        attackEstButton.onClick.AddListener(IncreaseAttack);
        attackBuffIcon.gameObject.SetActive(false); // 처음에는 아이콘 숨김
        tooltipText.gameObject.SetActive(false); // 처음에는 툴팁 숨김

        AddEventTriggers();
        UpdateButtons();
    }

    void IncreaseAttack()
    {
        if (!isAttackCooldown && !isAttackActive)
        {
            attackEst.value = Mathf.Clamp(attackEst.value + attackIncreaseAmount, attackEst.minValue, attackEst.maxValue);
            attackBuffIcon.gameObject.SetActive(true); // 버프 아이콘 활성화
            StartCoroutine(AttackEffectDuration()); // 지속시간 적용
            StartCoroutine(AttackCooldown()); // 쿨다운 시작
        }
    }

    void UpdateButtons()
    {
        attackEstButton.interactable = !isAttackCooldown;
    }

    IEnumerator AttackEffectDuration()
    {
        isAttackActive = true;
        Debug.Log("공격력이 증가했습니다! 지속 시간: " + attackDuration + "초");

        yield return new WaitForSeconds(attackDuration);

        attackEst.value = Mathf.Clamp(attackEst.value - attackIncreaseAmount, attackEst.minValue, attackEst.maxValue);
        isAttackActive = false;
        attackBuffIcon.gameObject.SetActive(false); // 버프 아이콘 숨김
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
        UpdateButtons();
        Debug.Log("공격력 버프를 다시 사용할 수 있습니다!");
    }

    // 마우스 포인터가 아이콘 위에 올라가면 툴팁 표시
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipText.gameObject.SetActive(true);
    }

    // 마우스 포인터가 아이콘에서 벗어나면 툴팁 숨김
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipText.gameObject.SetActive(false);
    }

    // attackBuffIcon에 이벤트 트리거 추가
    void AddEventTriggers()
    {
        EventTrigger trigger = attackBuffIcon.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });

        trigger.triggers.Add(entryEnter);
        trigger.triggers.Add(entryExit);
    }
}
