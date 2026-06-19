using UnityEngine;

public class BuyMinerButton : MonoBehaviour
{
    [SerializeField]
    private int minerAmount = 1;

    /// <summary>
    /// 採掘師購入ボタン
    /// </summary>
    public void OnClickBuyMiner()
    {
        GameManager.Instance.AddMiner(minerAmount);
    }
}
