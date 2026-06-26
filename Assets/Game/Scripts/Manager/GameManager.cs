using System;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Facilities")]
    [SerializeField]
    private FacilityDatabase facilityDatabase;

    private readonly List<FacilityState> facilityStates = new();

    private void InitializeFacilityStates()
    {
        facilityStates.Clear();

        foreach (FacilityDefinition definition
            in facilityDatabase.Facilities)
        {
            facilityStates.Add(
                new FacilityState
                {
                    Id = definition.Id,
                    Count = 0
                });
        }
    }

    public FacilityDefinition GetDefinition(string id)
    {
        return facilityDatabase.Facilities.Find(
            f => f.Id == id);
    }

    public FacilityState GetState(string id)
    {
        return facilityStates.Find(
            f => f.Id == id);
    }


    public List<FacilityDefinition> Facilities
    {
        get
        {
            return facilityDatabase.Facilities;
        }
    }

    public List<FacilityState> FacilityStates
    {
        get
        {
            return facilityStates;
        }
    }

    public int GetCost(string id)
    {
        FacilityDefinition definition =
            GetDefinition(id);

        FacilityState state =
            GetState(id);

        return definition.BaseCost
            + state.Count * definition.CostIncrease;
    }



    [Header("Resource")]
    [SerializeField]
    private int resource = 0;

    private float autoMineTimer = 0f;

    //数値の外部表示用
    public int Resource => resource;

    //各種イベント関数グループ
    public event Action<int> OnResourceChanged;

    public event Action<string> OnFacilityChanged;


    /// <summary>
    /// 起動時
    /// </summary>
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitializeFacilityStates();
    }

    private void Update()
    {
        if (GetProductionPerSecond() <= 0)
        {
            return;
        }

        autoMineTimer += Time.deltaTime;

        if (autoMineTimer >= 1f)
        {
            AddResource(GetProductionPerSecond());

            autoMineTimer -= 1f;
        }
    }

    /// <summary>
    /// 現在の毎秒生産量
    /// </summary>
    public int GetProductionPerSecond()
    {
        int totalProduction = 0;

        foreach (FacilityState state in facilityStates)
        {
            FacilityDefinition definition =
                GetDefinition(state.Id);

            totalProduction +=
                state.Count *
                definition.Production;
        }

        return totalProduction;
    }

    /// <summary>
    /// 魔石片を増やす
    /// </summary>
    public void AddResource(int amount)
    {
        resource += amount;

        OnResourceChanged?.Invoke(resource);
    }

    public bool BuyFacility(string id)
    {
        FacilityDefinition definition = GetDefinition(id);
        FacilityState state = GetState(id);

        if (definition == null || state == null)
        {
            return false;
        }

        int cost = GetCost(id);

        if (resource < cost)
        {
            return false;
        }

        resource -= cost;

        state.Count++;

        OnResourceChanged?.Invoke(resource);
        OnFacilityChanged?.Invoke(id);

        return true;
    }

    /// <summary>
    /// セーブデータ読み込み
    /// </summary>
    public void LoadData(SaveData saveData)
    {
        resource =
        saveData.Resource;

        foreach (FacilityState saveState
        in saveData.Facilities)
        {
            FacilityState state =
                GetState(saveState.Id);

            if (state == null)
            {
                continue;
            }

            state.Count = saveState.Count;
        }

        OnResourceChanged?.Invoke(resource);

        foreach (FacilityState state
            in facilityStates)
        {
            OnFacilityChanged?.Invoke(state.Id);
        }
    }
}
