using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent onDestroy;
    public float speed;
    public Vector3 direction;
    public Ship[] ships;

    private int liveCount;

    private void Awake()
    {
        liveCount = ships.Length;

        foreach (Ship ship in ships)
            ship.onDestroy.AddListener(OnShipDestroyed);
    }

    private void Update()
    {
        Vector3 position = transform.position;
        position += direction * speed * Time.deltaTime;
        transform.position = position;

        if (position.y < -LevelBounds.bounds.extents.y - transform.localScale.y)
        {
            onDestroy.Invoke();
            Destroy(gameObject);
        }

        foreach (Ship ship in ships)
            ship.Fire();
    }

    private void OnShipDestroyed()
    {
        if (--liveCount == 0)
        {
            onDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
