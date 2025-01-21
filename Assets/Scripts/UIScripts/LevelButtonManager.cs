using UnityEngine;
using UnityEngine.UI;


namespace TMKOC.FamilyTree
{
    public class LevelButtonManager : MonoBehaviour
    {
        [SerializeField] private Button LevelLoadButton;
        [SerializeField] private int currentLevelButtonIndex;
        private LevelState currentLevelStatus;

        private void Start()
        {
            SetLevelStatus();
            LevelLoadButton.onClick.AddListener(LoadLevel);
            GameManager.Instance.UIManager.OnMenuPressed += SetLevelStatus;
        }
        private void LoadLevel()
        {
            if (currentLevelStatus == LevelState.Unlocked)
            {
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
            int totalUnlockedLevels = GameManager.Instance.LevelManager.GetLevelIndex();
            if (currentLevelButtonIndex <= totalUnlockedLevels)
            {
                currentLevelStatus = LevelState.Unlocked;
            }
            else
            {
                currentLevelStatus = LevelState.Locked;
            }
        }
        private void OnDestroy()
        {
            GameManager.Instance.UIManager.OnMenuPressed -= SetLevelStatus;
        }
    }
    public enum LevelState
    {
        Locked,
        Unlocked
    }
}
