using UnityEngine;

public class ProxyCollider : MonoBehaviour
{
    [SerializeField] bool isFront = true;
    [SerializeField] BaseBlock parentBlock;

    void OnMouseDown()
    {
        parentBlock.OnClick(isFront);
    }
}
