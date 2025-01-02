using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe
{
    [CreateAssetMenu(fileName ="CountingLevelData",menuName="ScriptableObject/countingLevelSO")]
    public class CountingLevelSO : ScriptableObject
    {
        public CountingData[] countingData;
    }
    [System.Serializable]
    public class CountingData
    {
        public string objectName;
        public Sprite objectSprite;
        public float cellSize;
    }
}
