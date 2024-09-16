using System.Collections;
using TMPro;
using UnityEngine;

public class PayoutChangeUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _gambleRewardAmount;

    public void ShowCashUpdate(int cashUpdate)
    {
        PlayerController.Instance.StartCoroutine(DisableAfterDelay(cashUpdate));
    }

    private IEnumerator DisableAfterDelay(int cashUpdate)
    {
        _gambleRewardAmount.text = cashUpdate.ToString();

        _gambleRewardAmount.transform.parent.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        _gambleRewardAmount.transform.parent.gameObject.SetActive(false);
    }
}
