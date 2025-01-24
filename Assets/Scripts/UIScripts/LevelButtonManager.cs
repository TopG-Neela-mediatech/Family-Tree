using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace TMKOC.FamilyTree
{
    public class LevelButtonManager : MonoBehaviour
    {
        [SerializeField] private Button LevelLoadButton;
        [SerializeField] private int currentLevelButtonIndex;
        [SerializeField] private Image ButtonImage;
        private LevelState currentLevelStatus;
        public static event Action OnLevelButtonPressed;
        private int totalUnlockedLevels;


        private void DisableButton() => LevelLoadButton.enabled = false;
        private void EnableButton() => LevelLoadButton.enabled = true;
        private  void ResetTotalUnlockedLevels() => totalUnlockedLevels = 0;
        private void LoadLevel()=>StartCoroutine(LoadLevelAfterIntro());   


        private void Start()
        {
            totalUnlockedLevels = GameManager.Instance.LevelManager.GetLevelIndex();
            SetLevelStatus();
            LevelLoadButton.onClick.AddListener(LoadLevel);
            GameManager.Instance.UIManager.OnMenuPressed += SetLevelStatus;
            GameManager.Instance.UIManager.OnMenuPressed += EnableButton;
            OnLevelButtonPressed += DisableButton;
            GameManager.Instance.OnGameEnd += ResetTotalUnlockedLevels;
        }         
        private IEnumerator LoadLevelAfterIntro()
        {
            if (currentLevelStatus == LevelState.Unlocked)
            {
                OnLevelButtonPressed?.Invoke();
                DisableButton();
                float delay = GameManager.Instance.SoundManager.PlayLevelStartAudio();
                yield return new WaitForSeconds(delay-1);//-1 because delay in main loadlevel method
                GameManager.Instance.LevelManager.LoadLevel(currentLevelButtonIndex);
                GameManager.Instance.UIManager.DisableSelectionScreen();
            }
            else
            {
                Debug.Log("Level Not Unlocked Yet");
            }
        }
        private void SetLevelStatus()
        {
            int currentLevelNumber = GameManager.Instance.LevelManager.GetLevelIndex();
            if (totalUnlockedLevels < currentLevelNumber)
            {
                totalUnlockedLevels = currentLevelNumber;
            }
            if (currentLevelButtonIndex <= totalUnlockedLevels)
            {
                currentLevelStatus = LevelState.Unlocked;
                ButtonImage.color = new Color(ButtonImage.color.r, ButtonImage.color.g, ButtonImage.color.b, 1f);
            }
            else
            {
                currentLevelStatus = LevelState.Locked;
                ButtonImage.color = new Color(ButtonImage.color.r, ButtonImage.color.g, ButtonImage.color.b, 0.8f);
            }
        }
        private void OnDestroy()
        {
            GameManager.Instance.UIManager.OnMenuPressed -= SetLevelStatus;
            GameManager.Instance.UIManager.OnMenuPressed -= EnableButton;
            OnLevelButtonPressed -= DisableButton;
            GameManager.Instance.OnGameEnd -= ResetTotalUnlockedLevels;
        }
    }
    public enum LevelState
    {
        Locked,
        Unlocked
    }
}
