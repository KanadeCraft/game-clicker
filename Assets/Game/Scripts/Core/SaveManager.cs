using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string ResourceKey = "Resource";
    private const string MinerCountKey = "MinerCount";
    private const string MinerCostKey = "MinerCost";
    private const string LastQuitTimeKey = "LastQuitTime";
    private const string MachineCountKey = "MachineCount";
    private const string MachineCostKey = "MachineCost";

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

        PlayerPrefs.SetString(
        LastQuitTimeKey,
        DateTime.UtcNow.ToBinary().ToString());

        PlayerPrefs.SetInt(
            MachineCountKey,
            GameManager.Instance.MachineCount);

        PlayerPrefs.SetInt(
            MachineCostKey,
            GameManager.Instance.MachineCost);

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

        int machineCount =
            PlayerPrefs.GetInt(MachineCountKey,0);

        int machineCost =
            PlayerPrefs.GetInt(MachineCostKey,100);

        GameManager.Instance.LoadData(
            resource,
            minerCount,
            minerCost,
            machineCount,
            machineCost);

        ApplyOfflineProgress();

        Debug.Log("Load Complete");
    }

    private void ApplyOfflineProgress()
    {
        if (!PlayerPrefs.HasKey(LastQuitTimeKey))
        {
            return;
        }

        long binaryTime =
        long.Parse(
        PlayerPrefs.GetString(LastQuitTimeKey));

        DateTime lastQuitTime =
            DateTime.FromBinary(binaryTime);

        TimeSpan offlineTime =
            DateTime.UtcNow - lastQuitTime;

        double seconds =
            Math.Floor(offlineTime.TotalSeconds);

        int gainedResource =
            Mathf.FloorToInt(
                (float)(seconds * GameManager.Instance.GetProductionPerSecond()));

        GameManager.Instance.AddResource(gainedResource);

        Debug.Log($"Seconds : {seconds}");
        Debug.Log($"Production : {GameManager.Instance.GetProductionPerSecond()}");
        Debug.Log($"Offline Gain : {gainedResource}");
    }
}
