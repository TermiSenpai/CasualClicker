using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener, IUnityAdsLoadListener
{
    public delegate void AdvertisementUIDelegate();
    public static event AdvertisementUIDelegate OnStartLoading;
    public static event AdvertisementUIDelegate OnFinishLoading;
    public static event AdvertisementUIDelegate OnFailLoading;
    public static event AdvertisementUIDelegate OnRewardComplete;

    public static AdvertisementsManager Instance;
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
    readonly string _adUnitId = "Rewarded_Android"; // This will remain null for unsupported platforms

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitializeAds();
    }
    private void Start()
    {
        // Set the banner position:
        Advertisement.Banner.SetPosition(_bannerPosition);
    }

    #region Initialize
    public void InitializeAds()
    {
#if UNITY_ANDROID
        _testMode = false;
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _testMode = true;
            _gameId = _androidGameId;
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }

    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadBanner();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log(message: $"Unity Ads Initialization Failed: {error} - {message}");
    }
    #endregion

    #region Load
    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization
        Advertisement.Load(_adUnitId, this);
        OnStartLoading?.Invoke();
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        OnFinishLoading?.Invoke();
        if (adUnitId.Equals(_adUnitId))
        {
            ShowAd();
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        OnFailLoading?.Invoke();
    }
    #endregion

    #region Show
    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log("showing ad: " + adUnitId);
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        //StartCoroutine(ClickerManager.Instance.ClicksMultiplier(2));
        OnRewardComplete?.Invoke();
    }

    #endregion

    #region Banner

    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions bannerLoadOptions = new()
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        BannerLoadOptions options = bannerLoadOptions;

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId, options);
    }
    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }
    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();

    }
    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new()
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId, options);
    }
    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
    #endregion

}