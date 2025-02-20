using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    public int currentGold = 0;

    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    protected override void Awake()
    {
        base.Awake();
    }

    public void UpdateCurrentGold(int amount){
        currentGold += amount;

        if(goldText == null){
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        goldText.text = currentGold.ToString("D3");
    }
}
