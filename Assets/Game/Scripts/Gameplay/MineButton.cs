using UnityEngine;

public class MineButton : MonoBehaviour
{
    //仮置き
    [SerializeField]
    private int clickPower = 1;

    public void OnClickMine()
    {
        GameManager.Instance.AddResource(clickPower);
    }
}
