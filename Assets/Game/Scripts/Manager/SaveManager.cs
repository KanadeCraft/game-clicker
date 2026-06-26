using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string SaveKey = "SaveData";

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
        SaveData saveData =
        new SaveData();

        saveData.Resource =
            GameManager.Instance.Resource;

        // FacilityStateをコピーして保存
        saveData.Facilities =
            new List<FacilityState>();

        foreach (FacilityState state
            in GameManager.Instance.FacilityStates)
        {
            saveData.Facilities.Add(
                new FacilityState
                {
                    Id = state.Id,
                    Count = state.Count
                });
        }

        saveData.LastQuitTime =
            DateTime.UtcNow.ToBinary();

        string json =
            JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString(
            SaveKey,
            json);

        PlayerPrefs.Save();

        Debug.Log("Save Complete");
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey(SaveKey))
        {
            return;
        }

        string json = PlayerPrefs.GetString(SaveKey);

        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        GameManager.Instance.LoadData(saveData);

        ApplyOfflineProgress(saveData.LastQuitTime);

        Debug.Log("Load Complete");

    }

    private void ApplyOfflineProgress(long binaryTime)
    {
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
