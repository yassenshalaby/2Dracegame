using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if (GameManager.Instance.isGameOver)
        {
            Destroy(gameObject);
            return;
        }

        transform.position += Vector3.down * (speed * Time.deltaTime);

        if (transform.position.y < -10f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerController.Instance.IsImmune())
            {
                Destroy(gameObject);
            }
            else
            {
                GameManager.Instance.TriggerGameOver(other.transform.position);
            }
        }
    }
}