using UnityEngine;

public class PowerUp : MonoBehaviour
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
            PlayerController.Instance.ActivateImmunity();
            Destroy(gameObject);
        }
    }
}