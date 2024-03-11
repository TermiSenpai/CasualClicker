using System.Collections;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{
    public static ClickerManager instance;
    [SerializeField] int clicks;
    public int multiplier;

    private void Awake()
    {
        instance = this;
    }
    public void ClickImage()
    {
        clicks += 1 * multiplier;
    }

    public IEnumerator ClicksMultiplier(int value)
    {
        multiplier = value;
        yield return new WaitForSeconds(30f);
        multiplier = 1;
    }
}
