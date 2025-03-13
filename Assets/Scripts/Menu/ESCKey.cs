using UnityEngine;

public class ESCKey : MonoBehaviour
{
    public GameObject targetCanvas; // UI 캔버스

    void Start()
    {
        targetCanvas.SetActive(false); // 게임 시작 시 비활성화
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC 키 입력 감지
        {
            targetCanvas.SetActive(!targetCanvas.activeSelf); // 현재 상태 반대로 변경
        }
    }
}
