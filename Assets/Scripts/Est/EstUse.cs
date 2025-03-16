using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EstUse : MonoBehaviour
{
    public Slider healthEst; // 체력 UI 슬라이더
    public float healIncreaseAmount = 30f; // 한 번에 회복되는 체력량
    public float estRemain = 4f; // 남은 에스트 개수
    public float healSpeed = 20f; // 체력이 차오르는 속도

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            IncreaseHeal();
        }
    }

    void IncreaseHeal()
    {
        if (estRemain > 0)
        {
            StartCoroutine(HealOverTime(healIncreaseAmount)); // 체력을 천천히 증가시키는 코루틴 실행
            UseEstus();
        }
    }

    IEnumerator HealOverTime(float amount)
    {
        float targetHealth = Mathf.Clamp(healthEst.value + amount, healthEst.minValue, healthEst.maxValue);
        
        while (healthEst.value < targetHealth)
        {
            healthEst.value += healSpeed * Time.deltaTime;
            yield return null;
        }

        healthEst.value = targetHealth;
    }

    void UseEstus()
    {
        estRemain--;
    }
}
