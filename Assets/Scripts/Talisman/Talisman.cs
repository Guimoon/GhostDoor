using UnityEngine;

// 부적의 기본 클래스
public abstract class Talisman : ScriptableObject
{
    public string talismanName; // 부적 이름
    public Sprite talismanIcon; // 부적 아이콘

    public abstract void UseSkill1();
    public abstract void UseSkill2();
    public abstract void UseUltimate();
}
