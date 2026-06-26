using UnityEngine;

public class FacilityListUI : MonoBehaviour
{
    [SerializeField]
    private FacilityUI facilityPrefab;

    private void Start()
    {
        GameManager gameManager =
        GameManager.Instance;

        foreach (FacilityDefinition definition in GameManager.Instance.Facilities)
        {
            FacilityState state =
                GameManager.Instance.GetState(
                    definition.Id);

            FacilityUI ui =
                Instantiate(
                    facilityPrefab,
                    transform);

            ui.Initialize(definition,state);
        }
    }
}
