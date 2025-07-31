using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelSO", menuName = "ScriptableObject/LevelSO")]
public class LevelDataSO : ScriptableObject
{
    public List<LevelData> levelDatas;
}
