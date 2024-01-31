using DG.Tweening;
using System;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform circle;
    [SerializeField]
    private RectTransform blackScreen;

    private RectTransform canvasTransform;
    private float diameter;

    public Tween Hide()
    {
        return circle.DOSizeDelta(new Vector2(0, 0), 0.4f)
            .SetEase(Ease.OutQuint)
            .SetUpdate(true);
    }

    public Tween Show()
    {
        return circle.DOSizeDelta(new Vector2(diameter, diameter), 4.4f)
            .SetEase(Ease.OutQuint)
            .SetUpdate(true);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        canvasTransform = GetComponent<RectTransform>();
        blackScreen.sizeDelta = canvasTransform.sizeDelta;
        float x = canvasTransform.sizeDelta.x;
        float y = canvasTransform.sizeDelta.y;
        diameter = 0.5f * Mathf.Sqrt(x * x + y * y) * 2;
        circle.sizeDelta = new Vector2(diameter, diameter);
    }
}
