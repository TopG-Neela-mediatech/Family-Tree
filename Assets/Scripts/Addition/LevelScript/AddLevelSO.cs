using UnityEngine;

namespace TMKOC.CountWithMe.Add
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "AddLevelSO")]
    public class AddLevelSO : ScriptableObject
    {
        public AddLevelsData[] outerLevels;
    }
    [System.Serializable]
    public class AddLevelsData
    {
        public AddLevelData[] innerLevels;
       // public Sprite[] objectSprite;       
    }
    [System.Serializable]
    public class AddLevelData
    {
        public OptionData[] numbersToBeAdded;
        public int correctAnswer;
        public OptionData[] optionData; 
        public float cellSize1;
        public float cellSize2;        
    }
    [System.Serializable]
    public class OptionData
    {
        public Sprite numberSprite;
        public int value;
    }
}
