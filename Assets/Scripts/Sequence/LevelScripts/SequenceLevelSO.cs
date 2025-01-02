using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.Sequence
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "SequenceLevelSO")]
    public class SequenceLevelSO : ScriptableObject
    {
        public SeququenceLevels[] sequenceLevel;
    }
    [System.Serializable]
    public class SeququenceLevels
    {
        public string correctSequence;
        public List<BoxandValue> boxAndValue=new List<BoxandValue>();
        public int levelNumber;
    }
    [System.Serializable]
    public class BoxandValue
    {
        public int value;
        public Sprite numberboxSprite;
    }
}
