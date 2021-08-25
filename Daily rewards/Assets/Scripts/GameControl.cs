using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    #region SaveItems
    /// <summary>
    /// храниение значений данных золота и пушечных ядер
    /// </summary>

    public int Gold
    {
        get => PlayerPrefs.GetInt("Gold",0);
        private set => PlayerPrefs.SetInt("Gold", value);
    }

      public int Cannonballs
      {
            get => PlayerPrefs.GetInt("Cannonballs", 0);
            private set => PlayerPrefs.SetInt("Cannonballs", value);
      }
    #endregion

    [SerializeField]
    public TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI cannonballsText;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddGold(int value)
    {

        Gold += value;
        UpdateUI();
        
    }

    public void AddCabolls(int value)
    {
        Cannonballs += value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        goldText.SetText(Gold.ToString("N0"));
        
        cannonballsText.SetText(Cannonballs.ToString("N0"));
    }
}
