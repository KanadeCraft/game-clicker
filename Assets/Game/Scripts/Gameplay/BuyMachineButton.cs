using UnityEngine;

public class BuyMachineButton : MonoBehaviour
{
    public void OnClickBuyMachine()
    {
        GameManager.Instance.BuyMachine();
    }
}
