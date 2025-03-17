using UnityEngine;

public class InvetoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool activeInventory = false;

    private void Start()
    {
        inventoryPanel.SetActive(activeInventory);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }
}
