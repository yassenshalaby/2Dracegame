using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Transform road1;
    public Transform road2;
    public float speed = 3f;

    private float _roadHeight = 20f;

    void Start()
    {
        road2.position = new Vector3(road2.position.x,
            road1.position.y + _roadHeight,
            road2.position.z);
    }

    void Update()
    {
        float move = speed * Time.deltaTime;

        road1.position += Vector3.down * move;
        road2.position += Vector3.down * move;

        if (road1.position.y < -_roadHeight)
            road1.position = new Vector3(road1.position.x, road2.position.y + _roadHeight, road1.position.z);

        if (road2.position.y < -_roadHeight)
            road2.position = new Vector3(road2.position.x, road1.position.y + _roadHeight, road2.position.z);
    }
}