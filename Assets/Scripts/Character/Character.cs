using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Character : MonoBehaviour
{
    private void OnMouseDown() => ClickerManager.Instance.ClickImage();
}
