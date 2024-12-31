using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        private static GameManager instance;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(instance);
            }
        }


        public static GameManager Instance { get { return instance; } }
        public LevelManager LevelManager { get { return levelManager; } }


        #region Events
        public event Action OnLevelWin;
        public event Action OnLevelLose;
        public event Action OnLevelStart;
        public event Action OnGameEnd;


        public void InvokeLevelStart()=> OnLevelStart?.Invoke();
        public void InvokeLevelWin() => OnLevelWin?.Invoke();
        public void InvokeLevelLose() => OnLevelLose?.Invoke();
        public void InvokeGameEnd() => OnGameEnd?.Invoke();
        #endregion
    }
}
