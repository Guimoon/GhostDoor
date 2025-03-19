using UnityEngine;

[CreateAssetMenu(fileName = "New Wind Talisman", menuName = "Talisman/Wind")]
public class WindTalisman : Talisman
{
    public override void UseSkill1()
    {
        Debug.Log("칼바람");
    }

    public override void UseSkill2()
    {
        Debug.Log("상풍제");
    }

    public override void UseUltimate()
    {
        Debug.Log("천풍");
    }
}
