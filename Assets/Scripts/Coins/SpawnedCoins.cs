using System.Collections;
using UnityEngine;

public class SpawnedCoins : MonoBehaviour
{
    [SerializeField] SpriteRenderer coinSprite;
    [SerializeField] Vector2 direction;
    [SerializeField] float speed;
    private void OnEnable()
    {
        coinSprite.color = Color.white;
        transform.position = new Vector2(0, 1);
        StartCoroutine(DecreaseAlphaCoin());
        direction = new Vector2(Random.Range(-1f, 1f), 1f);
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }


    IEnumerator DecreaseAlphaCoin()
    {
        Color alpha = coinSprite.color;
        while (alpha.a > 0)
        {
            alpha.a -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            coinSprite.color = alpha;
        }
        gameObject.SetActive(false);
    }
}
