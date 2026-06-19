using UnityEngine;

public class BuyMinerButton : MonoBehaviour
{
    /// <summary>
    /// 採掘師購入ボタン
    /// </summary>
    public void OnClickBuyMiner()
    {
        GameManager.Instance.BuyMiner();
    }
}
