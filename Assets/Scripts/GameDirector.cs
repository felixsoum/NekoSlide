using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] GameObject catPrefab;
    [SerializeField] List<GameObject> block2Prefabs = new List<GameObject>();
    [SerializeField] List<GameObject> block3Prefabs = new List<GameObject>();

    LevelData levelData = new LevelData();
    HashSet<char> placedBlocks = new HashSet<char>();
    char[,] currentGrid;
    new Camera camera;
    private Vector3 prevMousePos;

    void Awake()
    {
        camera = Camera.main;
        currentGrid = levelData.GetGrid();
        CreateBlocks();
    }

    void Update()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {

        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            float deltaX = Input.mousePosition.x - prevMousePos.x;
            prevMousePos = Input.mousePosition;
            camera.transform.RotateAround(new Vector3(2.5f, 0, 2.5f), Vector3.up, 10 * deltaX * Time.deltaTime);
        }
    }

    private void CreateBlocks()
    {
        placedBlocks.Clear();
        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                char c = currentGrid[y, x];
                if (c == '0' || placedBlocks.Contains(c))
                {
                    continue;
                }

                placedBlocks.Add(c);
                bool facesRight = IsSameBlockRight(c, y, x);
                GameObject blockPrefab = null;
                if (c == 'x')
                {
                    blockPrefab = catPrefab;
                }
                else
                {
                    bool isBlockType2;
                    if (facesRight)
                    {
                        isBlockType2 = !IsSameBlockRight(c, y + 1, x);
                    }
                    else
                    {
                        isBlockType2 = !IsSameBlockUp(c, y, x + 1);
                    }

                    if (isBlockType2)
                    {
                        blockPrefab = block2Prefabs[UnityEngine.Random.Range(0, block2Prefabs.Count)];
                    }
                    else
                    {
                        blockPrefab = block3Prefabs[UnityEngine.Random.Range(0, block3Prefabs.Count)];
                    }
                }

                GameObject newBlock = Instantiate(blockPrefab, transform);
                InitBlock(facesRight, y, x, newBlock.GetComponent<BaseBlock>());
            }
        }
    }

    private void InitBlock(bool facesRight, int y, int x, BaseBlock blockComponent)
    {
        blockComponent.SetGameDirector(this);
        blockComponent.SetGridPos(x, y);
        blockComponent.SnapToGrid();
        if (facesRight)
        {
            blockComponent.Turn();
        }
    }

    bool IsSameBlockRight(char c, int x, int y)
    {
        if (x >= 5)
        {
            return false;
        }
        return currentGrid[x + 1, y] == c;
    }

    private bool IsSameBlockUp(char c, int x, int y)
    {
        if (y >= 5)
        {
            return false;
        }
        return currentGrid[x, y + 1] == c;
    }

    public void OnBlockClick(BaseBlock block, bool isFront)
    {
    }
}
