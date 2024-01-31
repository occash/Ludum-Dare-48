using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Ship ship;
    private Vector3 shipSize;

    private void Awake()
    {
        ship = GetComponent<Ship>();
        Collider2D collider = GetComponent<Collider2D>();
        shipSize = collider.bounds.size;
    }

    private void Update()
    {
        Vector3 position = transform.position;

        position.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        position.y += Input.GetAxis("Vertical") * speed * Time.deltaTime;

        /*if (Input.GetKey("w"))
            position.y += speed * Time.deltaTime;
        if (Input.GetKey("s"))
            position.y -= speed * Time.deltaTime;
        if (Input.GetKey("d"))
            position.x += speed * Time.deltaTime;
        if (Input.GetKey("a"))
            position.x -= speed * Time.deltaTime;*/

        Vector3 positionMin = LevelBounds.bounds.min + shipSize / 2.0f;
        Vector3 positionMax = LevelBounds.bounds.max - shipSize / 2.0f;

        position.x = Mathf.Clamp(position.x, positionMin.x, positionMax.x);
        position.y = Mathf.Clamp(position.y, positionMin.y, positionMax.y);

        transform.position = position;

        if (Input.GetButton("Fire"))
            ship.Fire();
    }
}
