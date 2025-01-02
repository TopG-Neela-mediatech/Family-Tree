using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace TMKOC.CountWithMe.Matching
{
    public class LevelManagerMatching : MonoBehaviour
    {
        [SerializeField] private MatchingLevelSO[] matchingLevels;
        [SerializeField] private Image[] objectNumberImages;
        [SerializeField] private Image[] numberPrefab;
        [SerializeField] private DragScriptMatching[] draggedObject;
        [SerializeField] private GetNumber[] getNumber;
        [SerializeField] private Button checkButton;
        [SerializeField] private Transform matchingImageParentTransform;
        [SerializeField] private Transform numberParentTransform;
        [SerializeField] private TutorialManager tutorialManager;
        private int currentLevelIndex;
        private string correctString;
        private GameCategoryDataManager gameCategoryDataManager;
        private UpdateCategoryApiManager updateCategoryApiManager;
        public int gameID;


        private void Awake()
        {
            #region GameID
#if PLAYSCHOOL_MAIN
             // assign varaible in this to get the  game ID from main app
             gameID  = PlayerPrefs.GetInt("currentGameId");
#endif
            #endregion
            gameCategoryDataManager = new GameCategoryDataManager(gameID, PlayerPrefs.GetString("currentGameName", "a"));
            updateCategoryApiManager = new UpdateCategoryApiManager(gameID);
            SetCurrentLevelIndex();
        }
        private void Start()
        {
            checkButton.onClick.AddListener(CheckMatch);

            GameManagerMatching.Instance.OnLevelStart += ScaleUpAndEnableCheckButton;
            GameManagerMatching.Instance.OnLevelWin += ScaleDownAndDisableCheckButton;
            GameManagerMatching.Instance.LivesManager.OnLivesOver += ScaleDownAndDisableCheckButton;
            GameManagerMatching.Instance.OnLevelStart += StartLevelAnimation;
            GameManagerMatching.Instance.OnLevelWin += EndLevelAnimation;
            GameManagerMatching.Instance.LivesManager.OnLivesOver += EndLevelAnimation;
            GameManagerMatching.Instance.OnLevelStart += PlayTutorial;
            numberParentTransform.DOLocalMoveX(-Screen.width, 0f);
            StartCoroutine(LoadLevelWithDelay());
            GameManagerMatching.Instance.OnLevelWin += () => StartCoroutine(IncrementLevelOnWin());
        }


        public void LoadLevel() => SetLevelData();
        private void ScaleUpMatchingImage() => matchingImageParentTransform.DOScale(1f, 1f);
        private void DeScaleUpMatchingImage() => matchingImageParentTransform.DOScale(0f, 1f);


        public void LoadNextLevel()
        {
            if (currentLevelIndex > matchingLevels.Length - 1)
            {
                currentLevelIndex = 0;
                gameCategoryDataManager.SaveLevel(currentLevelIndex, matchingLevels.Length);
                GameManagerMatching.Instance.InvokeGameEnd();
                GameManagerMatching.Instance.SoundManager.PlayFinalAudio();
                EndLevelAnimation();
                return;
            }
            LoadLevel();
        }
        private IEnumerator IncrementLevelOnWin()
        {
            SendStars();
            yield return new WaitForSeconds(0.5f);
            currentLevelIndex++;
            gameCategoryDataManager.SaveLevel(currentLevelIndex, matchingLevels.Length);
        }
        private void AnimateDragNumberOnStart()
        {
            numberParentTransform.DOLocalMoveX(-Screen.width, 0f);
            numberParentTransform.DOLocalMoveX(0f, 1f).OnComplete(() =>
            {
                EnableDragging();
            });
        }
        private void PlayTutorial()
        {
            StartCoroutine(PlayTutorialAfterDelay());
        }
        private IEnumerator PlayTutorialAfterDelay()
        {
            if (currentLevelIndex < 3)
            {
                yield return new WaitForSeconds(1.5f);                
                tutorialManager.StartHint();
            }
            yield break;
        }
        private void AnimateDragNumberOnEnd()
        {
            DisableDragging();
            numberParentTransform.DOLocalMoveX(+Screen.width, 1f);
        }
        private void EnableDragging()
        {
            for (int i = 0; i < draggedObject.Length; i++)
            {
                draggedObject[i].enabled = true;
            }
        }
        private void DisableDragging()
        {
            for (int i = 0; i < draggedObject.Length; i++)
            {
                draggedObject[i].enabled = false;
            }
        }
        private void SendStars()
        {
            int star = gameCategoryDataManager.GetLoadedstar;
            if (star >= 5)
            {
                updateCategoryApiManager.SetGameDataMore(matchingLevels.Length, matchingLevels.Length, 0, 5);
            }
            else
            {
                updateCategoryApiManager.SetGameDataMore(currentLevelIndex, matchingLevels.Length, 0, star);
            }
        }
        public int GetCurrentLevelNumber() => currentLevelIndex + 1;
        private void SetCurrentLevelIndex()
        {
            currentLevelIndex = gameCategoryDataManager.GetCompletedLevel;
            if (currentLevelIndex > matchingLevels.Length - 1)
            {
                currentLevelIndex = 0;
                gameCategoryDataManager.SaveLevel(currentLevelIndex, matchingLevels.Length);
            }
        }
        private void SetLevelData()
        {
            ShuffleList(matchingLevels[currentLevelIndex].MatchingData);
            SetCorrectMatching(matchingLevels[currentLevelIndex]);
            ShuffleList(matchingLevels[currentLevelIndex].slidingNumbers);
            for (int i = 0; i < 3; i++)//hard coded 3
            {
                int random = UnityEngine.Random.Range(0, 3);//hardcoded 3;
                objectNumberImages[i].sprite = matchingLevels[currentLevelIndex].MatchingData[i].objectImage[random];
                numberPrefab[i].sprite = matchingLevels[currentLevelIndex].slidingNumbers[i].numberImage;
                draggedObject[i].currentNumber = matchingLevels[currentLevelIndex].slidingNumbers[i].number;
            }
            GameManagerMatching.Instance.InvokeLevelStart();
        }
        private void SetCorrectMatching(MatchingLevelSO currentLevelSO)
        {
            correctString = "";
            for (int i = 0; i < 3; i++)//hard coded 3
            {
                correctString += (currentLevelSO.MatchingData[i].correspondingNumber.ToString());
            }
            Debug.Log(correctString);
        }
        private void ShuffleList<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
        private void CheckMatch()
        {
            string currentMatching = "";
            for (int i = 0; i < 3; i++)//hard coded 3
            {
                currentMatching += getNumber[i].currentNumber.ToString();
            }
            if (currentMatching == correctString)
            {
                GameManagerMatching.Instance.InvokeLevelWin();
                GameManagerMatching.Instance.SoundManager.PlaySFX(Sounds.levelComplete);
            }
            else
            {
                GameManagerMatching.Instance.SoundManager.PlayIncorrectAudio();
                GameManagerMatching.Instance.LivesManager.ReduceLive();
            }
        }
        private void ScaleUpAndEnableCheckButton()
        {
            checkButton.gameObject.SetActive(true);
            checkButton.enabled = true;
        }
        private void ScaleDownAndDisableCheckButton()
        {
            checkButton.enabled = false;
            checkButton.gameObject.SetActive(false);
        }
        private void StartLevelAnimation()
        {
            ScaleUpMatchingImage();
            AnimateDragNumberOnStart();
        }
        private void EndLevelAnimation()
        {
            DeScaleUpMatchingImage();
            AnimateDragNumberOnEnd();
        }
        private IEnumerator LoadLevelWithDelay()
        {
            yield return new WaitForSeconds(0.1f);
            LoadLevel();
        }
        private void OnDestroy()
        {
            GameManagerMatching.Instance.OnLevelWin += () => StartCoroutine(IncrementLevelOnWin());
            GameManagerMatching.Instance.OnLevelStart -= ScaleUpAndEnableCheckButton;
            GameManagerMatching.Instance.OnLevelWin -= ScaleDownAndDisableCheckButton;
            GameManagerMatching.Instance.LivesManager.OnLivesOver -= ScaleDownAndDisableCheckButton;
            GameManagerMatching.Instance.OnLevelStart -= StartLevelAnimation;
            GameManagerMatching.Instance.OnLevelWin -= EndLevelAnimation;
            GameManagerMatching.Instance.LivesManager.OnLivesOver -= EndLevelAnimation;
            GameManagerMatching.Instance.OnLevelStart -= PlayTutorial;
        }
    }
}
