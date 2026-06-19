using TMPro;
using UnityEngine;

public class MinerCountText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text minerCountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateText(GameManager.Instance.MinerCount);

        GameManager.Instance.OnMinerCountChanged += UpdateText;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMinerCountChanged -= UpdateText;
        }
    }

    private void UpdateText(int count)
    {
        minerCountText.text = $"採掘師 : {count}";
    }
}
