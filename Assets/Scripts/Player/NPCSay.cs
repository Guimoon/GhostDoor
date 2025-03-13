using UnityEngine;

public class NPCSay : MonoBehaviour
{

    public float moveMax;
    public float speed;

    Vector3 pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirPos = pos;
        dirPos.y = pos.y + moveMax * Mathf.Sin(Time.time * speed);
        transform.position = dirPos;
    }
}
