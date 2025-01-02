using System;
using UnityEngine;

namespace TMKOC.CountWithMe.AddTest
{
    public class AddTestGameManager : MonoBehaviour
    {
        [SerializeField] private AddTestLevelManager levelManager;
        [SerializeField] private AddTestUIManager testUIManager;
        [SerializeField] private AddTestSoundManager soundManager;
        private static AddTestGameManager instance;


        #region Events
        public event Action OnLevelEnd;
        public event Action OnLevelStart;
        public event Action OnGameEnd;
        public void InvokeLevelStart() => OnLevelStart?.Invoke();
        public void InvokeLevelEnd() => OnLevelEnd?.Invoke();
        public void InvokeGameEnd() => OnGameEnd?.Invoke();
        #endregion


        public static AddTestGameManager Instance { get { return instance; } }
        public AddTestLevelManager LevelManager { get { return levelManager; } }
        public AddTestUIManager UIManager { get { return testUIManager; } }
        //public AddTestSoundManager SoundManager { get { return soundManager; } }


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
