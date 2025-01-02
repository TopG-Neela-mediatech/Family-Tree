using System;
using UnityEngine;

namespace TMKOC.CountWithMe
{
    public class GameManagerCounting : MonoBehaviour
    {
        [SerializeField] private CountingLevelManager countingLevelManager;
        [SerializeField] private CountingSoundManager countingSoundManager;       
        private static GameManagerCounting instance;
       
       


        public static GameManagerCounting Instance { get { return instance; } }
        public CountingLevelManager CountingLevelManager { get { return countingLevelManager; } }
        public CountingSoundManager CountingSoundManager { get { return countingSoundManager; } }
      


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


        public event Action OnLevelEnd;
        public event Action OnLevelStart;
        public event Action OnGameEnd;


        public void InvokeLevelStart() {OnLevelStart?.Invoke(); }
        public void InvokeLevelEnd() => OnLevelEnd?.Invoke(); 
        public void InvokeGameEnd() => OnGameEnd?.Invoke();
    }
}
