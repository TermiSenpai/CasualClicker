using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Character : MonoBehaviour
{
    [SerializeField] Vector3 downClick;
    [SerializeField] Vector3 upClick;

    public delegate void OnCharacterClick();
    public static event OnCharacterClick OnCharacterClickRelease;

    public void OnMouseDown()
    {
        transform.localScale = downClick;
        OnCharacterClickRelease?.Invoke();
        //ClickerManager.Instance.ClickImage();

    }
    public void OnMouseUp()
    {
        transform.localScale = upClick;

    }
}
