using UnityEngine;

[CreateAssetMenu(fileName = "New Earth Talisman", menuName = "Talisman/Earth")]
public class EarthTalisman : Talisman
{
    public override void UseSkill1()
    {
        Debug.Log("토암검");
    }

    public override void UseSkill2()
    {
        Debug.Log("축지법");
    }

    public override void UseUltimate()
    {
        Debug.Log("대지혼격");
    }
}
