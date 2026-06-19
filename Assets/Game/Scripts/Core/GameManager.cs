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

    //数値の外部表示用
    public int Resource => resource;
    public int MinerCount => minerCount;

    //リソース(魔石片)用イベント関数グループ
    public event Action<int> OnResourceChanged;
    //マイナー(採掘師)用イベント関数グループ
    public event Action<int> OnMinerCountChanged;

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
}
