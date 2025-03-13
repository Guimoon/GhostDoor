using UnityEngine;

public class NPCDetected : MonoBehaviour
{
    public Transform player;
    public float NPCDetectRange = 2f;
    public float NPCDetectRangeExit = 5f;
    public GameObject uiPanel;

    private void Awake()
    {
        uiPanel.SetActive(false);
    }

    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < NPCDetectRange && Input.GetKeyDown(KeyCode.F))
        {
            uiPanel.SetActive(true);
        }

        if(distance > NPCDetectRangeExit)
        {
            uiPanel.SetActive(false);
        }
    }
}