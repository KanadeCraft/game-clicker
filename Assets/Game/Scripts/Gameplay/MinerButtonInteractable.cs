using UnityEngine;
using UnityEngine.UI;

public class MinerButtonInteractable : MonoBehaviour
{
    [SerializeField]
    private Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateButton(GameManager.Instance.Resource);

        GameManager.Instance.OnResourceChanged += UpdateButton;
        GameManager.Instance.OnMinerCostChanged += OnCostChanged;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnResourceChanged -= UpdateButton;
            GameManager.Instance.OnMinerCostChanged -= OnCostChanged;
        }
    }

    private void UpdateButton(int resource)
    {
        button.interactable = GameManager.Instance.CanBuyMiner();
    }

    private void OnCostChanged(int cost)
    {
        button.interactable = GameManager.Instance.CanBuyMiner();
    }
}
