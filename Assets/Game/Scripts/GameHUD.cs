using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
    public TextMeshProUGUI bonusText;

    public void UpdateBonus(int bounus)
    {
        bonusText.text = bounus.ToString("0000");
    }
}
