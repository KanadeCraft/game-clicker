using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string ResourceKey = "Resource";
    private const string MinerCountKey = "MinerCount";
    private const string MinerCostKey = "MinerCost";

    [ContextMenu("Delete Save")]
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();

        Debug.Log("Delete Save");
    }

    private void Start()
    {
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt(
            ResourceKey,
            GameManager.Instance.Resource);

        PlayerPrefs.SetInt(
            MinerCountKey,
            GameManager.Instance.MinerCount);

        PlayerPrefs.SetInt(
            MinerCostKey,
            GameManager.Instance.MinerCost);

        PlayerPrefs.Save();

        Debug.Log("Save Complete");
    }

    public void LoadGame()
    {
        int resource =
            PlayerPrefs.GetInt(ResourceKey, 0);

        int minerCount =
            PlayerPrefs.GetInt(MinerCountKey, 0);

        int minerCost =
            PlayerPrefs.GetInt(MinerCostKey, 10);

        GameManager.Instance.LoadData(
            resource,
            minerCount,
            minerCost);

        Debug.Log("Load Complete");
    }
}
