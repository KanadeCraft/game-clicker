using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    public int MinerCount => minerCount;
    public int MinerCost => minerCost;
    public int MachineCount => machineCount;
    public int MachineCost => machineCost;

    //各種イベント関数グループ
    public event Action<int> OnResourceChanged;
    public event Action<int> OnMinerCountChanged;
    public event Action<int> OnMinerCostChanged;
    public event Action<int> OnMachineCountChanged;
    public event Action<int> OnMachineCostChanged;


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
        return
            minerCount * 1 +
            machineCount * 5;
    }

    /// <summary>
    /// 魔石片を増やす
    /// </summary>
    public void AddResource(int amount)
    {
        resource += amount;

        OnResourceChanged?.Invoke(resource);
    }

    /// <summary>
    /// 採掘師を増やす
    /// </summary>
    public void AddMiner(int amount)
    {
        minerCount += amount;

        OnMinerCountChanged?.Invoke(minerCount);
    }

    /// <summary>
    /// 採掘師購入判定
    /// </summary>
    public bool CanBuyMiner()
    {
        return resource >= minerCost;
    }

    /// <summary>
    /// 採掘師購入
    /// </summary>
    public bool BuyMiner()
    {
        if (resource < minerCost)
        {
            return false;
        }

        resource -= minerCost;

        AddMiner(1);

        minerCost += 5;

        OnResourceChanged?.Invoke(resource);
        OnMinerCostChanged?.Invoke(minerCost);

        return true;
    }

    /// <summary>
    /// 採掘師を増やす
    /// </summary>
    public void AddMachine(int amount)
    {
        machineCount += amount;

        OnMachineCountChanged?.Invoke(machineCount);
    }

    public bool CanBuyMachine()
    {
        return resource >= machineCost;
    }

    public bool BuyMachine()
    {
        if (resource < machineCost)
        {
            return false;
        }

        resource -= machineCost;

        AddMachine(1);

        machineCost += 20;

        OnResourceChanged?.Invoke(resource);
        OnMachineCostChanged?.Invoke(machineCost);

        return true;
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
