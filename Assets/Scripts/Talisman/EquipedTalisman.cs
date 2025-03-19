using UnityEngine;

public class EquipedTalisman : MonoBehaviour
{
    public Talisman[] talismans; // 부적들 (Fire, Water, Wood, Wind, Earth)
    private int currentIndex = 0; // 현재 선택된 부적의 인덱스

    public Talisman equippedTalisman; // 현재 장착된 부적

    void Start()
    {
        if (equippedTalisman == null && talismans.Length > 0) // 아무 부적도 없으면 기본 부적 장착 (첫 번째 부적)
        {
            equippedTalisman = talismans[0];
        }
    }

    void Update()
    {
        // 부적의 스킬 사용
        if (Input.GetKeyDown(KeyCode.J)) equippedTalisman?.UseSkill1();
        if (Input.GetKeyDown(KeyCode.K)) equippedTalisman?.UseSkill2();
        if (Input.GetKeyDown(KeyCode.L)) equippedTalisman?.UseUltimate();

        // 부적 전환
        if (Input.GetKeyDown(KeyCode.Q)) EquipNextTalisman(-1); // Q 키: 이전 부적
        if (Input.GetKeyDown(KeyCode.E)) EquipNextTalisman(1);  // E 키: 다음 부적
    }

    // 부적을 전환하는 함수
    void EquipNextTalisman(int direction)
    {
        // 부적 리스트를 순환하여 전환
        currentIndex = (currentIndex + direction + talismans.Length) % talismans.Length;
        equippedTalisman = talismans[currentIndex];
        Debug.Log(equippedTalisman.talismanName + " 장착됨!");
    }
}
