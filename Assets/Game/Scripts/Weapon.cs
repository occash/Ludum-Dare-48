using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Game/Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject projectilePrefab;
    public AudioClip shotSound;
    public float cooldownTime;
    public int shotCount;
    public float shotDelay;
    public float damage;
    public float[] bulletAngles;
}

/*[System.Serializable]
public class Bullet
{
    public Vector3 direction;
}*/