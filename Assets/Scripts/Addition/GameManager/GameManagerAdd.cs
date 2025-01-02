using System;
using UnityEngine;

namespace TMKOC.CountWithMe.Add
{
    public class GameManagerAdd : MonoBehaviour
    {
        [SerializeField] private LevelManagerAdd levelManager;
        [SerializeField] private UIManagerAdd uiManager;
        [SerializeField] private LivesManagerAdd livesManager;
        [SerializeField] private StarsManagerAdd starsManager;
        [SerializeField] private AddSoundManager soundManager;
        private static GameManagerAdd instance;


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


        public static GameManagerAdd Instance { get { return instance; } }
        public LevelManagerAdd LevelManager { get { return levelManager; } }
        public UIManagerAdd UIManager { get { return uiManager; } }
        public LivesManagerAdd LivesManager { get { return livesManager; } }
        public AddSoundManager SoundManager { get { return soundManager; } }
        public StarsManagerAdd StarsManager { get { return starsManager; } }


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

