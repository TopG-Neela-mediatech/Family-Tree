using System;
using System.Collections;
using UnityEngine;


namespace TMKOC.CountWithMe.Test
{
    public class TestGameManager : MonoBehaviour
    {
        [SerializeField] private TestLevelManager levelManager;
        [SerializeField] private TestUIManager testUIManager;
        [SerializeField] private TestSoundManager soundManager;       
        private static TestGameManager instance;


        #region Events
        public event Action OnLevelEnd;
        public event Action OnLevelStart;
        public event Action OnGameEnd;
        public void InvokeLevelStart() => OnLevelStart?.Invoke();
        public void InvokeLevelEnd() => OnLevelEnd?.Invoke();
        public void InvokeGameEnd() => OnGameEnd?.Invoke();        
        #endregion


        public static TestGameManager Instance { get { return instance; } }
        public TestLevelManager LevelManager { get { return levelManager; } }
        public TestUIManager UIManager { get { return testUIManager; } }
        public TestSoundManager SoundManager { get {return soundManager; } }
        

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            Application.targetFrameRate = 60;
        }
    }
}

