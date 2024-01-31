using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOverMenu gameOverMenu;
    public GameObject playerShipPrefab;
    public EnemySpawner enemySpawner;
    public GameHUD gameHUD;

    private int totalBonus;

    public void Restart()
    {
        SpawnPlayer();
        enemySpawner.Clear();
        enemySpawner.Run();
        totalBonus = 0;
        gameHUD.UpdateBonus(0);
    }

    private void Awake()
    {
        enemySpawner.onBonus.AddListener(AddBonus);
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        GameObject playerShipObject = Instantiate(
            playerShipPrefab,
            new Vector3(0, -15, 0),
            Quaternion.identity
        );
        Ship playerShip = playerShipObject.GetComponent<Ship>();
        playerShip.onDestroy.AddListener(OnGameOver);
    }

    private void OnGameOver()
    {
        enemySpawner.Stop();
        gameOverMenu.Show();
    }

    private void AddBonus(int bonus)
    {
        totalBonus += bonus;
        gameHUD.UpdateBonus(totalBonus);
    }
}
