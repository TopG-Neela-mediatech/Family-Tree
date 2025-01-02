using System;
using UnityEngine;

namespace TMKOC.CountWithMe.Sequence
{
    public class GameManagerSequence : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private LivesManager livesManager;
        [SerializeField] private StarManager starsManager;
        [SerializeField] private SoundManagerSequence soundManager;
        private static GameManagerSequence instance;
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


        public static GameManagerSequence Instance { get { return instance; } }
        public LevelManager LevelManager { get { return levelManager; } }
        public UIManager UIManager { get { return uiManager; } }
        public LivesManager LivesManager { get { return livesManager; } }
        public StarManager StarsManager { get {return starsManager; } }
        public SoundManagerSequence SoundManager { get {return soundManager; } }


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
