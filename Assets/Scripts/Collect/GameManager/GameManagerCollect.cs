using System;
using TMKOC.CountWithMe.Sequence;
using UnityEngine;

namespace TMKOC.CountWithMe.Collect
{
    public class GameManagerCollect : MonoBehaviour
    {
        [SerializeField] private CollectLevelManager levelManager;
        [SerializeField] private UIManagerCollect uiManager;
        [SerializeField] private CollectLivesManager livesManager;
        [SerializeField] private StarsManager starsManager;
        // [SerializeField] private StarManager starsManager;
        [SerializeField] private CollectSoundManager soundManager;
        private static GameManagerCollect instance;
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


        public static GameManagerCollect Instance { get { return instance; } }
        public CollectLevelManager LevelManager { get { return levelManager; } }
        public UIManagerCollect UIManager { get { return uiManager; } }
        public CollectLivesManager LivesManager { get { return livesManager; } }
        public CollectSoundManager SoundManager { get { return soundManager; } }
        public StarsManager StarsManager { get {return starsManager; } }


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

