using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public int order;
    public GameObject[] objectPrefabs;
    public float speed;
    public int objectCount;
    public float scaleMin;
    public float scaleMax;

    private List<GameObject> layerObjects = new List<GameObject>();

    private void Awake()
    {
        CreateLayer(Vector3.zero);
        CreateLayer(GetSpawnPoint());
    }

    private void Update()
    {
        for (int i = layerObjects.Count - 1; i >= 0; --i)
        {
            GameObject layerObject = layerObjects[i];
            Vector3 position = layerObject.transform.position;
            position += Vector3.down * speed * Time.deltaTime;

            if (position.y < (LevelBounds.bounds.min * 2).y)
            {
                CreateLayer(GetSpawnPoint());
                layerObjects.RemoveAt(i);
                Destroy(layerObject);
            }

            layerObject.transform.position = position;
        }
    }

    private Vector3 GetSpawnPoint()
    {
        return new Vector3()
        {
            x = 0.0f,
            y = LevelBounds.bounds.max.y * 2,
            z = 0.0f
        };
    }

    private void CreateLayer(Vector3 position)
    {
        GameObject layerObject = new GameObject("Layer");
        layerObject.transform.parent = transform;
        layerObject.transform.position = position;

        for (int i = 0; i < objectCount; ++i)
            SpawnObject(layerObject);

        layerObjects.Add(layerObject);
    }

    private void SpawnObject(GameObject layerObject)
    {
        int index = Random.Range(0, objectPrefabs.Length);
        GameObject spaceObject = Instantiate(objectPrefabs[index], layerObject.transform);
        SpriteRenderer spriteRenderer = spaceObject.GetComponent<SpriteRenderer>();

        spriteRenderer.sortingOrder = order;

        float scale = Random.Range(scaleMin, scaleMax);
        float x = Random.Range(LevelBounds.bounds.min.x, LevelBounds.bounds.max.x);
        float y = Random.Range(LevelBounds.bounds.min.y + scale, LevelBounds.bounds.max.y - scale);

        Vector3 position = new Vector3(x, y, 0);

        spaceObject.transform.localPosition = position;
        spaceObject.transform.localScale = new Vector3(scale, scale, 1.0f);
    }
}
