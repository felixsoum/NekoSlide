using UnityEngine;

public class ProxyCollider : MonoBehaviour
{
    [SerializeField] bool isFront = true;
    public BaseBlock Parent { get; set; }

    void OnMouseDown()
    {
        Parent.OnClick(isFront);
    }
}
