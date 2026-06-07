using UnityEngine;

public class ButterflyMove : MonoBehaviour
{
    public float speed = 2f;
    public float range = 3f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * range;
        transform.position = startPos + new Vector3(x, 0, 0);
    }
}
