using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    public int gridX;
    public int gridY;
    public const float GridWidth = 1;

    void Start()
    {
        if (Random.value < 0.5)
        {
            Turn();
        }
    }

    void OnValidate()
    {
        Vector3 pos = new Vector3(gridX * GridWidth, 0, gridY * GridWidth);
        transform.position = pos;
    }

    public void Turn()
    {
        transform.localEulerAngles = new Vector3(0, 90, 0);
    }
}
