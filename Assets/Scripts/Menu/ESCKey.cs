using UnityEngine;

public class ESCKey : MonoBehaviour
{
    public GameObject targetCanvas; // UI 캔버스

    private float previousTimeScale; // 이전 타임스케일 저장용 변수

    void Start()
    {
        targetCanvas.SetActive(false); // 게임 시작 시 비활성화
        previousTimeScale = Time.timeScale; // 초기 타임스케일 저장
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC 키 입력 감지
        {
            bool newActiveState = !targetCanvas.activeSelf;
            targetCanvas.SetActive(newActiveState); // 현재 상태 반대로 변경
            
            // 시간 제어 추가
            if (newActiveState) // 캔버스가 활성화되면 시간 정지
            {
                previousTimeScale = Time.timeScale; // 현재 타임스케일 저장
                Time.timeScale = 0f; // 시간 정지
            }
            else // 캔버스가 비활성화되면 시간 복원
            {
                Time.timeScale = previousTimeScale; // 이전 타임스케일로 복원
            }
        }
    }
}