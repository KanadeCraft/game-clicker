using TMPro;
using UnityEngine;

public class ResourceText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text resourceText;

    private void Start()
    {
        UpdateText(GameManager.Instance.Resource);

        GameManager.Instance.OnResourceChanged += UpdateText;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnResourceChanged -= UpdateText;
        }
    }

    private void UpdateText(int resource)
    {
        resourceText.text = $"魔石片 : {resource}";
    }
}