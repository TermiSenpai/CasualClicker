using UnityEngine;

public class CoinSpawnOnClick : MonoBehaviour
{
    [SerializeField] GameObject[] coins;
    [SerializeField] int index;

    private void OnEnable()
    {
        Character.OnCharacterClickRelease += SpawnCoin;
    }

    private void OnDisable()
    {
        Character.OnCharacterClickRelease -= SpawnCoin;
    }

    void SpawnCoin()
    {
        Debug.Log("coin");
        if (index >= coins.Length) index = 0;
        if (coins[index].activeInHierarchy)
        {
            index++;
            SpawnCoin();
        }
        coins[index].SetActive(true);
        index++;
    }

}
