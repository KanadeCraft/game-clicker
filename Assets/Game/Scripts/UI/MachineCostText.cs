using TMPro;
using UnityEngine;

public class MachineCostText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text machineCostText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateText(GameManager.Instance.MachineCost);

        GameManager.Instance.OnMachineCostChanged += UpdateText;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMachineCostChanged -= UpdateText;
        }
    }

    private void UpdateText(int cost)
    {
        machineCostText.text = $"魔石採掘機価格 : {cost}";
    }
}
