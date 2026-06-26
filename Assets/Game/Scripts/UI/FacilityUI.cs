using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FacilityUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text countText;

    [SerializeField]
    private TMP_Text costText;

    [SerializeField]
    private Button buyButton;

    private FacilityState state;
    private FacilityDefinition definition;

    public void Initialize(FacilityDefinition facilityDefinition, FacilityState facilityState)
    {
        definition = facilityDefinition;
        state = facilityState;

        UpdateView();

        buyButton.onClick.AddListener(BuyFacility);

        GameManager.Instance.OnFacilityChanged += OnFacilityChanged;
        GameManager.Instance.OnResourceChanged += OnResourceChanged;
    }

    private void OnDestroy()
    {
        if (buyButton != null)
        {
            buyButton.onClick.RemoveListener(BuyFacility);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnFacilityChanged -= OnFacilityChanged;
            GameManager.Instance.OnResourceChanged -= OnResourceChanged;
        }
    }

    private void OnResourceChanged(int resource)
    {
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        buyButton.interactable =
            GameManager.Instance.Resource >=
            GameManager.Instance.GetCost(definition.Id);
    }

    private void BuyFacility()
    {
        GameManager.Instance.BuyFacility(definition.Id);
    }

    private void OnFacilityChanged(string id)
    {
        if (id != definition.Id)
        {
            return;
        }

        UpdateView();
    }

    private void UpdateView()
    {
        if (iconImage != null)
        {
            iconImage.sprite = definition.Icon;
        }

        if (nameText != null)
        {
            nameText.text = definition.DisplayName;
        }

        if (countText != null)
        {
            countText.text = $"{state.Count}";
        }

        if (costText != null)
        {
            costText.text =
                $"価格 : {GameManager.Instance.GetCost(definition.Id)}";
        }

        UpdateButtonState();
    }
}
    
