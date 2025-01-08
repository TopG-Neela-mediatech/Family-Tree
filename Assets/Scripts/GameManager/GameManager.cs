using System;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private HintManager hintManager;
        [SerializeField] private UIManager uiManager;
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
        public HintManager HintManager { get { return hintManager; } }
        public UIManager UIManager { get { return uiManager; } }

        #region Events
        public event Action OnLevelWin;
        public event Action OnTreeComplete;
        public event Action OnLevelStart;
        public event Action OnGameEnd;


        public void InvokeLevelStart() => OnLevelStart?.Invoke();
        public void InvokeLevelWin() => OnLevelWin?.Invoke();
        public void InvokeTreeComplete() => OnTreeComplete?.Invoke();
        public void InvokeGameEnd() => OnGameEnd?.Invoke();
        #endregion
    }
}
