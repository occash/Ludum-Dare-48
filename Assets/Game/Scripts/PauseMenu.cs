using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup menuCanvas;
    public MusicManager musicManager;

    private bool isPaused = false;

    public void Resume()
    {
        musicManager.Play();
        menuCanvas.DOFade(0.0f, 0.4f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                menuCanvas.interactable = false;
                menuCanvas.blocksRaycasts = false;
            })
            .SetUpdate(true);
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1.0f, 0.4f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() => isPaused = false)
            .SetUpdate(true);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                Pause();
            else
                Resume();
        }
    }

    private void Pause()
    {
        isPaused = true;
        musicManager.Pause();

        menuCanvas.DOFade(1.0f, 0.4f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                menuCanvas.interactable = true;
                menuCanvas.blocksRaycasts = true;
            })
            .SetUpdate(true);
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.0f, 0.4f)
            .SetEase(Ease.OutQuint)
            .SetUpdate(true);
    }
}
