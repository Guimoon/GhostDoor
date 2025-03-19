using UnityEngine;

[CreateAssetMenu(fileName = "New Wood Talisman", menuName = "Talisman/Wood")]
public class WoodTalisman : Talisman
{
    public override void UseSkill1()
    {
        Debug.Log("가지 묶이");
    }

    public override void UseSkill2()
    {
        Debug.Log("가시 장판");
    }

    public override void UseUltimate()
    {
        Debug.Log("자목생멸");
    }
}
