using System.Collections;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{
    //public static ClickerManager Instance;
    public delegate void ValueChangedDelegate(int newValue);
    public static event ValueChangedDelegate OnValueChanged;

    [SerializeField] private int baseClick;
    public int multiplier;
    [SerializeField] int clicks;
    public int Clicks
    {
        get => clicks;
        set
        {
            clicks = value;
            OnValueChanged?.Invoke(clicks);
        }
    }

    //private void Awake()
    //{
    //    if (Instance == null)
    //        Instance = this;
    //    else if (Instance != this)
    //        Destroy(gameObject);

    //    DontDestroyOnLoad(gameObject);
    //}

    private void OnEnable()
    {
        Character.OnCharacterClickRelease += OncharacterClick;
        AdvertisementsManager.OnRewardComplete += OnRewardComplete;
    }

    private void OnDisable()
    {
        Character.OnCharacterClickRelease -= OncharacterClick;        
        AdvertisementsManager.OnRewardComplete -= OnRewardComplete;
    }

    void OncharacterClick()
    {
        ClickImage();
    }
    public void ClickImage()
    {
        Clicks += baseClick * multiplier;
    }

    public IEnumerator ClicksMultiplier(int newMultiplier)
    {
        multiplier = newMultiplier;
        yield return new WaitForSeconds(30f);
        multiplier = 1;
    }

    private void OnRewardComplete()
    {
        StartCoroutine(ClicksMultiplier(2));
    }
}
