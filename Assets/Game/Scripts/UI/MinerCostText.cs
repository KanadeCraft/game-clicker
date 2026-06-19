using TMPro;
using UnityEngine;

public class MinerCostText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text minerCostText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateText(GameManager.Instance.MinerCost);

        GameManager.Instance.OnMinerCostChanged += UpdateText;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMinerCostChanged -= UpdateText;
        }
    }

    private void UpdateText(int cost)
    {
        minerCostText.text = $"採掘師価格 : {cost}";
    }
}
