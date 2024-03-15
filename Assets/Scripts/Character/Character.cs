using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Character : MonoBehaviour
{
    [SerializeField] Vector3 downClick;
    [SerializeField] Vector3 upClick;

    public void OnMouseDown()
    {
        transform.localScale = downClick;
        ClickerManager.Instance.ClickImage();

    }
    public void OnMouseUp()
    {
        transform.localScale = upClick;

    }
}
