using System;
using UnityEngine;

namespace TMKOC.CountWithMe.Matching
{
    public class GameManagerMatching : MonoBehaviour
    {
        [SerializeField] private LevelManagerMatching levelManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private LivesManager livesManager;
        [SerializeField] private MatchingSoundManager soundManager;
        [SerializeField] private StarManager starManager;
        private static GameManagerMatching instance;


        #region Events
        public event Action OnLevelWin;
        public event Action OnLevelLose;
        public event Action OnLevelStart;
        public event Action OnGameEnd;
        public void InvokeLevelStart() => OnLevelStart?.Invoke();
        public void InvokeLevelWin() => OnLevelWin?.Invoke();
        public void InvokeLevelLose() => OnLevelLose?.Invoke();
        public void InvokeGameEnd() => OnGameEnd?.Invoke();
        #endregion


        public static GameManagerMatching Instance { get { return instance; } }
        public LevelManagerMatching LevelManager { get { return levelManager; } }
        public UIManager UIManager { get { return uiManager; } }
        public LivesManager LivesManager { get { return livesManager; } }
        public MatchingSoundManager SoundManager { get { return soundManager; } }
        public StarManager StarManager { get { return starManager; } }


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
