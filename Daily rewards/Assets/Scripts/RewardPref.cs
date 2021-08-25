using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardPref : MonoBehaviour
{
    // подсветка отображаемого элемента
    [SerializeField]
    private Image background;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color currentStreakerColor;

    // отображения дня и количесвтва награды

    [Space(5)]
    [SerializeField]
    private TextMeshProUGUI dayText;
    [SerializeField]
    private TextMeshProUGUI rewardValue;

    // инициализация 2 иконок

    [Space(5)]
    [SerializeField]
    private Image rewardIcon;
    [SerializeField]
    private Sprite rewardGold;
    [SerializeField]
    private Sprite rewardCaBolls;


    public void SetRewardDate(int day, int currentStreak, Reward reward)
    {
        dayText.text = $"Day {day + 1}";
        rewardIcon.sprite = reward.Type == Reward.RewardType.GOLD ? rewardGold : rewardCaBolls;
        rewardValue.text = reward.Value.ToString();

        background.color = day == currentStreak ? currentStreakerColor : defaultColor;
    }
}
