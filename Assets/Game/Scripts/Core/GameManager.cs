using System;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private List<FacilityData> facilities = new List<FacilityData>();

    private void InitializeFacilities()
    {
        if (facilities.Count > 0)
        {
            return;
        }

        facilities.Add(
            new FacilityData
            {
                Id = "Miner",
                DisplayName = "採掘師",
                Count = 0,
                Cost = 10,
                Production = 1,
                CostIncrease = 5
            });

        facilities.Add(
            new FacilityData
            {
                Id = "Machine",
                DisplayName = "魔石採掘機",
                Count = 0,
                Cost = 100,
                Production = 5,
                CostIncrease = 20
            });
    }

    public FacilityData GetFacility(string id)
    {
        return facilities.Find(f => f.Id == id);
    }

    

    [Header("Resource")]
    [SerializeField]
    private int resource = 0;

    [Header("Workers")]
    [SerializeField]
    private int minerCount = 0;

    [Header("Machines")]
    [SerializeField]
    private int machineCount = 0;

    //各強化の初期価格
    [SerializeField]
    private int minerCost = 10;
    [SerializeField]
    private int machineCost = 100;

    private float autoMineTimer = 0f;

    //数値の外部表示用
    public int Resource => resource;
    public int MinerCount => GetFacility("Miner").Count;
    public int MinerCost => GetFacility("Miner").Cost;
    public int MachineCount => GetFacility("Machine").Count;
    public int MachineCost => GetFacility("Machine").Cost;

    //各種イベント関数グループ
    public event Action<int> OnResourceChanged;
    public event Action<int> OnMinerCountChanged;
    public event Action<int> OnMinerCostChanged;
    public event Action<int> OnMachineCountChanged;
    public event Action<int> OnMachineCostChanged;

    public event Action<FacilityData> OnFacilityChanged;


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
        InitializeFacilities();
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

        foreach (FacilityData facility in facilities)
        {
            totalProduction +=
                facility.Count *
                facility.Production;
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
        FacilityData facility = GetFacility(id);

        if (facility == null)
        {
            return false;
        }

        if (resource < facility.Cost)
        {
            return false;
        }

        resource -= facility.Cost;

        facility.Count++;

        facility.Cost += facility.CostIncrease;

        OnResourceChanged?.Invoke(resource);
        OnFacilityChanged?.Invoke(facility);

        return true;
    }

    /// <summary>
    /// 購入判定
    /// </summary>
    public bool CanBuyMiner()
    {
        return resource >= GetFacility("Miner").Cost;
    }

    public bool CanBuyMachine()
    {
        return resource >= GetFacility("Machine").Cost;
    }

    /// <summary>
    /// 購入
    /// </summary>
    public bool BuyMiner()
    {
        return BuyFacility("Miner");
    }

    public bool BuyMachine()
    {
        return BuyFacility("Machine");
    }

    /// <summary>
    /// セーブデータ読み込み
    /// </summary>
    public void LoadData(int resource,int minerCount,int minerCost,int machineCount,int machineCost)
    {
        this.resource = resource;
        this.minerCount = minerCount;
        this.minerCost = minerCost;
        this.machineCount = machineCount;
        this.machineCost = machineCost;

        OnResourceChanged?.Invoke(this.resource);
        OnMinerCountChanged?.Invoke(this.minerCount);
        OnMinerCostChanged?.Invoke(this.minerCost);
        OnMachineCountChanged?.Invoke(this.machineCount);
        OnMachineCostChanged?.Invoke(this.machineCost);
    }
}
