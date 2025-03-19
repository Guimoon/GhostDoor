using UnityEngine;

[CreateAssetMenu(fileName = "New Water Talisman", menuName = "Talisman/Water")]
public class WaterTalisman : Talisman
{
    public override void UseSkill1()
    {
        Debug.Log("염수");
    }

    public override void UseSkill2()
    {
        Debug.Log(" 용왕의 가호");
    }

    public override void UseUltimate()
    {
        Debug.Log("궁극기 수령환술");
    }
}
