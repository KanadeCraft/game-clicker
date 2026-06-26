using UnityEngine;

[CreateAssetMenu(
    fileName = "Facility",
    menuName = "Clicker/Facility")]
public class FacilityDefinition : ScriptableObject
{
    [Header("Basic")]

    /// <summary>
    /// 固有ID
    /// </summary>
    public string Id;

    /// <summary>
    /// 表示名
    /// </summary>
    public string DisplayName;

    /// <summary>
    /// Tooltipに表示する説明
    /// </summary>
    [TextArea]
    public string Description;

    /// <summary>
    /// アイコン
    /// </summary>
    public Sprite Icon;

    [Header("Production")]

    /// <summary>
    /// 初期購入価格
    /// </summary>
    public int BaseCost;

    /// <summary>
    /// 購入時の価格増加量
    /// </summary>
    public int CostIncrease;

    /// <summary>
    /// 一つ当たりの毎秒生産量
    /// </summary>
    public int Production;
}