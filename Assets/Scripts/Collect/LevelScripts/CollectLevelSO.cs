using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.Collect
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "CollectLevelSO")]
    public class CollectLevelSO : ScriptableObject
    {
        public LevelData[] levelData;
    }
    [System.Serializable]
    public class LevelData
    {
        public ObjectData levelObjectData;
        public DragData[] optionList;
    }
    [System.Serializable]
    public class ObjectData
    {
        public ObjectSpriteData[] spriteDatas;
        public int correctID;
        public int count;
    }
    [System.Serializable]
    public class ObjectSpriteData
    {
        public Sprite objectSprite;
        public string objectName;
    }   
    [System.Serializable]
    public class DragData
    {
        public Sprite dragObjectSprite;
        public int dragID;
    }
}
