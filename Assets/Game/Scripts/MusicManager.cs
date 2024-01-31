using DG.Tweening;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    public void Play()
    {
        audioSource.Play();
        audioSource.DOFade(1.0f, 0.4f)
            .SetEase(Ease.OutQuint)
            .SetUpdate(true);
    }

    public void Pause()
    {
        audioSource.DOFade(0.0f, 0.4f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() => audioSource.Pause())
            .SetUpdate(true);
    }

    public void Stop()
    {
        audioSource.DOFade(0.0f, 0.4f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() => audioSource.Stop())
            .SetUpdate(true);
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
