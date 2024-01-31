using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Ship : MonoBehaviour
{
    public UnityEvent onDestroy;

    public Weapon weapon;
    public GameObject explosionEffect;
    public float hitPoints;
    public int bonus;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private int shotCount;
    private float shotDelay;
    private float cooldownTime;

    public void Fire()
    {
        if (weapon == null)
            return;

        if (cooldownTime <= 0.0f)
        {
            shotCount = weapon.shotCount;
            shotDelay = 0.0f;
            cooldownTime = weapon.cooldownTime;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (weapon == null)
            return;

        if (shotCount > 0 && shotDelay <= 0.0f)
        {
            foreach (float angle in weapon.bulletAngles)
                CreateBullet(angle);

            audioSource.PlayOneShot(weapon.shotSound);
            shotCount--;
            shotDelay = weapon.shotDelay;
        }

        if (shotDelay > 0.0f)
            shotDelay -= Time.deltaTime;
        else if (cooldownTime > 0.0f)
            cooldownTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();

        if (projectile)
            hitPoints -= projectile.damage;
        else
            hitPoints = 0.0f;

        if (hitPoints <= 0.0f)
        {
            onDestroy.Invoke();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            Sequence sequence = DOTween.Sequence();

            for (int i = 0; i < 5; ++i)
            {
                sequence.Append(spriteRenderer.DOColor(Color.red, 0.2f));
                sequence.Append(spriteRenderer.DOColor(Color.white, 0.2f));
            }
        }
    }

    private void CreateBullet(float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        GameObject projectileObject = Instantiate(weapon.projectilePrefab, transform.position, rotation * transform.rotation);
        projectileObject.layer = gameObject.layer;
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.direction = rotation * transform.up;
        projectile.damage = weapon.damage;
    }
}
