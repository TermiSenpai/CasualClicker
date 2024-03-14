using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementUI : MonoBehaviour
{

    [SerializeField] GameObject adVideoBtn;
    [SerializeField] GameObject adLoadingUI;
    [SerializeField] GameObject adLoadingFail;
    [SerializeField] GameObject adShowReward;


    private void OnEnable()
    {
        AdvertisementsManager.OnStartLoading += OnStartLoading;
        AdvertisementsManager.OnFinishLoading += OnFinishLoading;
        AdvertisementsManager.OnRewardComplete += OnShowReward;
        AdvertisementsManager.OnFailLoading += OnLoadingFail;
    }

    private void OnDisable()
    {
        AdvertisementsManager.OnStartLoading -= OnStartLoading;
        AdvertisementsManager.OnFinishLoading -= OnFinishLoading;
        AdvertisementsManager.OnRewardComplete -= OnShowReward;
        AdvertisementsManager.OnFailLoading -= OnLoadingFail;

    }
    public void LoadAdBtn() => AdvertisementsManager.Instance.LoadAd();
    void OnStartLoading()
    {
        adVideoBtn.SetActive(false);
        adLoadingUI.SetActive(true);
    }

    void OnFinishLoading()
    {
        adLoadingUI.SetActive(false);
    }
    void OnShowReward()
    {
        StartCoroutine(ShowReward());
    }

    IEnumerator ShowReward()
    {
        adShowReward.SetActive(true);
        yield return new WaitForSeconds(1);
        adShowReward.SetActive(false);

    }

    void OnLoadingFail()
    {
        adLoadingFail.SetActive(true);
    }

}
