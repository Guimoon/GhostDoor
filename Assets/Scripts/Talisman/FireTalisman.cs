using UnityEngine;

[CreateAssetMenu(fileName = "New Fire Talisman", menuName = "Talisman/Fire")]
public class FireTalisman : Talisman
{
    public override void UseSkill1()
    {
        Debug.Log("도깨비불");
    }

    public override void UseSkill2()
    {
        Debug.Log("열화");
    }

    public override void UseUltimate()
    {
        Debug.Log("열화폭염");
    }
}
