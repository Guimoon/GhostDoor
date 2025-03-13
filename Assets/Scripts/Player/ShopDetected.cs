using UnityEngine;

public class ShopDetected : MonoBehaviour
{
    public Transform player;
    public float detectRange = 3f;
    public GameObject uiPanel;


    private void Awake()
    {
        uiPanel.SetActive(false);
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectRange)
        {
            uiPanel.SetActive(false);
        }

        if (distance < detectRange)
        {
            uiPanel.SetActive(true);
        }
    }
}
