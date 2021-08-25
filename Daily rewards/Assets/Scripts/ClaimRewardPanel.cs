using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClaimRewardPanel : MonoBehaviour
{
    public static ClaimRewardPanel Instance;

    [SerializeField]
    private Image rewardIcon;
    [SerializeField]
    private TextMeshProUGUI rewardValue;
    [SerializeField]
    private CanvasGroup cg;

    [Space(5)]
    [SerializeField]
    private Sprite rewardGold;
    [SerializeField]
    private Sprite rewardCaBolls;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Hide();
    }

    // отображение панели
    public void Show(Reward reward)
    {
        rewardIcon.sprite = reward.Type == Reward.RewardType.GOLD ? rewardGold : rewardCaBolls;
        rewardValue.text = $"Got{reward.Value}{reward.Name}";

        gameObject.LeanScale(Vector2.one, .5f).setEaseInBack();
        cg.blocksRaycasts = true;
    }

    //скрытие панели

    public void Hide()
    {
        cg.blocksRaycasts = false;
        gameObject.LeanScale(Vector2.zero, .5f).setEaseInBack();
    }
}
