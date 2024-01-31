using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float maxDistance;
    public float damage;
    public bool isDestructible = true;

    private float distance = 0.0f;

    void Update()
    {
        Vector3 position = transform.position;
        Vector3 movement = direction.normalized * speed * Time.deltaTime;

        distance += movement.magnitude;

        if (distance >= maxDistance)
        {
            Destroy(gameObject);
            return;
        }

        position += movement;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();

        if (projectile == null || isDestructible)
            Destroy(gameObject);
    }
}
