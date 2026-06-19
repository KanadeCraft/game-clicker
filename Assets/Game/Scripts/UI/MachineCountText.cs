using TMPro;
using UnityEngine;

public class MachineCountText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text machineCountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateText(GameManager.Instance.MachineCount);

        GameManager.Instance.OnMachineCountChanged += UpdateText;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMachineCountChanged -= UpdateText;
        }
    }

    private void UpdateText(int count)
    {
        machineCountText.text = $"魔石採掘機 : {count}";
    }
}
