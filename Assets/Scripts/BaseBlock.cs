using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    int gridX;
    int gridY;
    public const float GridWidth = 1;

    public void Turn()
    {
        transform.localEulerAngles = new Vector3(0, -90, 0);
    }

    public void SetPosition(int x, int y)
    {
        gridX = x;
        gridY = y;
        Vector3 pos = new Vector3(gridX * GridWidth, 0, gridY * GridWidth);
        transform.position = pos;
    }
}
