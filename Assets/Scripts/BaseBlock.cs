using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    public int size = 2;
    public bool IsTurned { get; set; }
    int gridPosX;
    int gridPosY;
    public const float GridWidth = 1;
    GameDirector gameDirector;

    void Awake()
    {
        foreach (var colliderProxy in GetComponentsInChildren<ProxyCollider>())
        {
            colliderProxy.Parent = this;
        }
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, GetPosFromGrid(), 10 * Time.deltaTime);
    }

    public void Turn()
    {
        transform.localEulerAngles = new Vector3(0, -90, 0);
        IsTurned = true;
    }

    public void SetGridPos(int x, int y)
    {
        gridPosX = x;
        gridPosY = y;
    }

    public void SnapToGrid()
    {
        transform.position = GetPosFromGrid();
    }

    Vector3 GetPosFromGrid()
    {
        return new Vector3(gridPosX * GridWidth, 0, gridPosY * GridWidth);
    }

    public void OnClick(bool isFront)
    {
        if (isFront)
        {
            gridPosX++;
        }
        else
        {
            gridPosX--;
        }
        gameDirector.OnBlockClick(this, isFront);
    }

    public void SetGameDirector(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;
    }
}
