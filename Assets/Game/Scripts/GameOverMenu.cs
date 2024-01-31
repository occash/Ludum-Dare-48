using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public CanvasGroup menuCanvas;
    public MusicManager musicManager;
    public GameManager gameManager;

    public void Show()
    {
        musicManager.Stop();
        menuCanvas.DOFade(1.0f, 0.4f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                menuCanvas.interactable = true;
                menuCanvas.blocksRaycasts = true;
            })
            .SetUpdate(true);
    }

    public void RestartGame()
    {
        musicManager.Play();
        gameManager.Restart();
        menuCanvas.DOFade(0.0f, 0.4f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                menuCanvas.interactable = false;
                menuCanvas.blocksRaycasts = false;
            })
            .SetUpdate(true);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
