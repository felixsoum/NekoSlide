using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] GameObject catPrefab;
    [SerializeField] List<GameObject> block2Prefabs = new List<GameObject>();
    [SerializeField] List<GameObject> block3Prefabs = new List<GameObject>();

    LevelData levelData = new LevelData();



}
