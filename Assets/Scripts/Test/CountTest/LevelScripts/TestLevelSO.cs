using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.Test
{
    [CreateAssetMenu(fileName = "TestLevelSO", menuName = "TestSO")]
    public class TestLevelSO : ScriptableObject
    {
        public LevelData level1Data;
        public Level2Data level2Data;
        public Level2Data level3Data;
        public Level2Data level4Data;
        public Level2Data level5Data;
    }
    [System.Serializable]
    public class LevelData
    {
        public int[] correctNumber;
        public LevelQuestionsData[] questionData;
        public LevelOptions[] optionData;

        [System.Serializable]
        public class LevelQuestionsData
        {
            public Sprite objectImage;
            public string objectName;
        }
    }
    [System.Serializable]
    public class Level2Data
    {
        public Sprite[] questionSprite;
        public LevelOptions[] optionData;
    }
    [System.Serializable]
    public class LevelOptions
    {
        public Sprite optionsSprite;
        public int optionNumber;
    }
}
