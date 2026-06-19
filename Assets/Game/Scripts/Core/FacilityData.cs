using System;

[Serializable]
public class FacilityData
{
    /// <summary>
    /// Id、名前、所持数、購入コスト、一個当たりの増加数
    /// </summary>
    public string Id;

    public string DisplayName;

    public int Count;

    public int Cost;

    public int Production;
}