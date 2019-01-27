using UnityEngine;

public class ProxyCollider : MonoBehaviour
{
    public BaseBlock Parent { get; set; }

    void OnMouseDown()
    {
        Parent.OnClick();
    }
}
