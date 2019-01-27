﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    private const float MouseDistanceToMoveBlock = 75;
    [SerializeField] Text levelText = null;
    [SerializeField] Text winText = null;
    [SerializeField] GameObject catPrefab = null;
    [SerializeField] List<GameObject> block2Prefabs = new List<GameObject>();
    [SerializeField] List<GameObject> block3Prefabs = new List<GameObject>();
    static int levelIndex = 0;
    HashSet<char> placedBlocks = new HashSet<char>();
    char[,] currentGrid;
    new Camera camera;
    private Vector3 prevMousePos;
    private Vector3 startMousePos;
    bool isGameOver;
    BaseBlock catBlock;
    BaseBlock clickedBlock;

    Vector3 blockForward;

    void Awake()
    {
        levelText.text = $"LEVEL {levelIndex + 1}";
        winText.gameObject.SetActive(false);
        camera = Camera.main;
        currentGrid = LevelData.GetGrid(levelIndex);
        CreateBlocks();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            clickedBlock = null;
            blockForward = Vector3.zero;
        }

        UpdateCamera();

        if (Input.GetMouseButton(0) && clickedBlock != null)
        {
            Vector3 screenVector = Input.mousePosition - startMousePos;
            float dot = Vector3.Dot(screenVector, blockForward);
            if (Mathf.Abs(dot) > MouseDistanceToMoveBlock)
            {
                if (dot > 0)
                {
                    PushFront(clickedBlock);
                }
                else
                {
                    PushBack(clickedBlock);
                }
                startMousePos = Input.mousePosition;

                if (catBlock.GridPosX == 4)
                {
                    isGameOver = true;
                    winText.gameObject.SetActive(true);
                    catBlock.GridPosX = 6;
                    Invoke("NextLevel", 2);
                }
            }
        }
    }

    private void UpdateCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = Input.mousePosition;
            startMousePos = prevMousePos;
        }
        else if (Input.GetMouseButton(0) && clickedBlock == null)
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
                InitBlock(facesRight, y, x, c, newBlock.GetComponent<BaseBlock>());
            }
        }
    }

    private void InitBlock(bool facesRight, int y, int x, char c, BaseBlock blockComponent)
    {
        if (c == 'x')
        {
            catBlock = blockComponent;
        }
        blockComponent.GridChar = c;
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

    public void OnBlockClick(BaseBlock block)
    {
        if (isGameOver)
        {
            return;
        }

        clickedBlock = block;
        Vector3 screenPointStart = camera.WorldToScreenPoint(block.transform.position);
        Vector3 screenPointEnd = camera.WorldToScreenPoint(block.transform.position + block.transform.right);
        blockForward = screenPointEnd - screenPointStart;
        blockForward.z = 0;
        blockForward.Normalize();
    }

    private bool PushFront(BaseBlock block)
    {
        if (block.IsTurned)
        {
            if (TrySetGrid(block.GridPosX, block.GridPosY + block.size, block.GridChar))
            {
                ClearGrid(block.GridPosX, block.GridPosY);
                block.GridPosY++;
                return true;
            }
        }
        else
        {
            if (TrySetGrid(block.GridPosX + block.size, block.GridPosY, block.GridChar))
            {
                ClearGrid(block.GridPosX, block.GridPosY);
                block.GridPosX++;
                return true;
            }
        }
        return false;
    }

    private bool PushBack(BaseBlock block)
    {
        if (block.IsTurned)
        {
            if (TrySetGrid(block.GridPosX, block.GridPosY - 1, block.GridChar))
            {
                ClearGrid(block.GridPosX, block.GridPosY + block.size - 1);
                block.GridPosY--;
                return true;
            }
        }
        else
        {
            if (TrySetGrid(block.GridPosX - 1, block.GridPosY, block.GridChar))
            {
                ClearGrid(block.GridPosX + block.size - 1, block.GridPosY);
                block.GridPosX--;
                return true;
            }
        }
        return false;
    }

    public bool TrySetGrid(int x, int y, char c)
    {
        if (x < 0 || x > 5 || y < 0 || y > 5)
        {
            return false;
        }
        if (currentGrid[y, x] == '0')
        {
            currentGrid[y, x] = c;
            return true;
        }
        return false;
    }

    public void ClearGrid(int x, int y)
    {
        currentGrid[y, x] = '0';
    }

    public void ChangeLevel(int increment)
    {
        levelIndex += increment;
        levelIndex %= LevelData.Grids.Count;
        if (levelIndex < 0)
        {
            levelIndex += LevelData.Grids.Count;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NextLevel()
    {
        ChangeLevel(1);
    }
}
