using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DailyReward : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI status;
    [SerializeField]
    private Button claimBtn;

    [Space(5)]
    [SerializeField]
    private RewardPref rewardPref;
    [SerializeField]
    private Transform rewardsGrid;

    [Space(5)]
    [SerializeField]
    private List<Reward> rewards;

    

    //������ � ��������� ������ � ���������� ��������� �������
    private int currentStreak
    {
        get => PlayerPrefs.GetInt("currentStreak",0);
        set => PlayerPrefs.SetInt("currentStreak", value);
    }

    // ����� ���������� ���������
    private DateTime? lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClaimedTime", null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastClaimedTime", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastClaimedTime");
        }
    }


    private bool canClaimReward; // ����� �� �� ������ ������� ������� ��� ���
    private int maxStreakCount=8; // ����� ���������� ���� �� ������� ���������� �������
    private float claimCoolDown = 24f / 24 / 60 / 6 / 2; //����� ������� ����� ������� ������� 
    private float claimDeadLine = 48f / 24 / 60 / 6 / 2; //� ����� �� ����� ���������� ����� ������� ������� 

    private List<RewardPref> rewardPrefabs;

    private void Start()
    {
        InitPrefabs();
        StartCoroutine(RewardsStateUpdater());
    }


    // �������� �������� ������
    private void InitPrefabs()
    {
        rewardPrefabs = new List<RewardPref>();

        //�������� �������� � ���������� �� � ������

        for (int i = 0; i < maxStreakCount; i++)
            rewardPrefabs.Add(Instantiate(rewardPref, rewardsGrid, false));
    }

    //����� ��� �������� ��������� ������

    IEnumerator RewardsStateUpdater()
    {
        while (true)
        {
            UpdateRewardsState();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateRewardsState()
    {
        canClaimReward = true;

        if (lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - lastClaimTime.Value;

            if (timeSpan.TotalHours > claimDeadLine)
            {
                lastClaimTime = null;
                currentStreak = 0;
            }
            else if (timeSpan.TotalHours < claimCoolDown)
                canClaimReward = false;
        }
        UpdateRewardsUI();
    }

    // ����������� �� ������
    private void UpdateRewardsUI()
    {
        claimBtn.interactable = canClaimReward;

        if (canClaimReward)
            status.text = "Claim your reward!";
        else
        {
            var nexClaimTime = lastClaimTime.Value.AddHours(claimCoolDown);
            var currentClaimCooldowm = nexClaimTime - DateTime.UtcNow;

            string cd = $"{currentClaimCooldowm.Hours:D2}:{currentClaimCooldowm.Minutes:D2}:{currentClaimCooldowm.Seconds:D2}";

            status.text = $"Come back in {cd} for your next reward";
        }

        for (int i = 0; i < rewardPrefabs.Count; i++)
            rewardPrefabs[i].SetRewardDate(i,currentStreak,rewards[i]);
    }

    // ����� ��� ��������� �������

    public void ClaimReward()
    {
        if (!canClaimReward)
            return;

        var reward = rewards[currentStreak];

        switch (reward.Type)
        {
            case Reward.RewardType.GOLD:
                GameControl.Instance.AddGold(reward.Value);
                break;
            case Reward.RewardType.CABOOLLS:
                GameControl.Instance.AddCabolls(reward.Value);
                break;
        }

        ClaimRewardPanel.Instance.Show(reward);

        lastClaimTime = DateTime.UtcNow;
        currentStreak = (currentStreak + 1) % maxStreakCount;

        UpdateRewardsState();

    }

}
