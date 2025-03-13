using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Buff : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Slider attackEst;
    public Button attackEstButton;
    public Image attackBuffIcon; // ���� ���� ������
    public Text tooltipText; // ���� �ؽ�Ʈ
    public float attackIncreaseAmount = 10f;

    public float cooldownTime = 60f; // ���� ����Ʈ ��ư ���� ��� �ð� (��)
    public float attackDuration = 10f; // ���ݷ� ���� ���� �ð� (��)

    private bool isAttackCooldown = false; // ���� ����Ʈ ��ư�� ��ٿ� ������ Ȯ��
    private bool isAttackActive = false; // ���� ���� ȿ���� Ȱ��ȭ�Ǿ����� Ȯ��

    void Start()
    {
        attackEstButton.onClick.AddListener(IncreaseAttack);
        attackBuffIcon.gameObject.SetActive(false); // ó������ ������ ����
        tooltipText.gameObject.SetActive(false); // ó������ ���� ����

        AddEventTriggers();
        UpdateButtons();
    }

    void IncreaseAttack()
    {
        if (!isAttackCooldown && !isAttackActive)
        {
            attackEst.value = Mathf.Clamp(attackEst.value + attackIncreaseAmount, attackEst.minValue, attackEst.maxValue);
            attackBuffIcon.gameObject.SetActive(true); // ���� ������ Ȱ��ȭ
            StartCoroutine(AttackEffectDuration()); // ���ӽð� ����
            StartCoroutine(AttackCooldown()); // ��ٿ� ����
        }
    }

    void UpdateButtons()
    {
        attackEstButton.interactable = !isAttackCooldown;
    }

    IEnumerator AttackEffectDuration()
    {
        isAttackActive = true;
        Debug.Log("���ݷ��� �����߽��ϴ�! ���� �ð�: " + attackDuration + "��");

        yield return new WaitForSeconds(attackDuration);

        attackEst.value = Mathf.Clamp(attackEst.value - attackIncreaseAmount, attackEst.minValue, attackEst.maxValue);
        isAttackActive = false;
        attackBuffIcon.gameObject.SetActive(false); // ���� ������ ����
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
        UpdateButtons();
        Debug.Log("���ݷ� ������ �ٽ� ����� �� �ֽ��ϴ�!");
    }

    // ���콺 �����Ͱ� ������ ���� �ö󰡸� ���� ǥ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipText.gameObject.SetActive(true);
    }

    // ���콺 �����Ͱ� �����ܿ��� ����� ���� ����
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipText.gameObject.SetActive(false);
    }

    // attackBuffIcon�� �̺�Ʈ Ʈ���� �߰�
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
