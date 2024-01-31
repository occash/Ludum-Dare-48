using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public UnityEvent<int> onBonus;

    public GameObject[] shipPrefabs;
    public float spawnRate;

    private float coolDownTime;
    private float[] spawnPoints = new float[3];
    private readonly List<Enemy> enemyList = new List<Enemy>();
    private bool isRunning = true;

    public void Run()
    {
        isRunning = true;
    }

    public void Stop()
    {
        isRunning = false;
    }

    public void Clear()
    {
        foreach (Enemy enemy in enemyList)
            Destroy(enemy.gameObject);

        enemyList.Clear();
    }

    private void Awake()
    {
        spawnPoints[0] = LevelBounds.bounds.min.x / 2;
        spawnPoints[1] = 0.0f;
        spawnPoints[2] = LevelBounds.bounds.max.x / 2;
    }

    private void Update()
    {
        if (!isRunning)
            return;

        if (coolDownTime <= 0.0f)
        {
            SpawnEnemy();
            coolDownTime = 1.0f / spawnRate;
        }
        else
            coolDownTime -= Time.deltaTime;
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, shipPrefabs.Length);
        GameObject enemyObject = Instantiate(shipPrefabs[index]);
        Collider2D collider = enemyObject.GetComponent<Collider2D>();

        int spawnIndex = Random.Range(0, 3);
        float x = spawnPoints[spawnIndex];
        float y = LevelBounds.bounds.max.y + collider.bounds.extents.y;

        if (enemyObject.transform.childCount > 0)
            Destroy(collider);

        Vector3 position = new Vector3(x, y, 0);
        enemyObject.transform.position = position;

        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.onDestroy.AddListener(() => enemyList.Remove(enemy));
        enemyList.Add(enemy);

        foreach (Ship ship in enemy.ships)
            ship.onDestroy.AddListener(() => onBonus.Invoke(ship.bonus));
    }
}
