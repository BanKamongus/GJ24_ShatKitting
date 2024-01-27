using UnityEngine;

public class RepeatBG : MonoBehaviour
{
    private Vector3 startPos;
    public float BGrange = 15;
    public float repeatWidth;

    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}