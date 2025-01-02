using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Collect
{
    public class CollectLevelManager : MonoBehaviour
    {
        [SerializeField] private CollectLevelSO levelSO;
        [SerializeField] private CollectDrag[] collectOptions;
        [SerializeField] private TextMeshProUGUI correctObjectText;
        [SerializeField] private Button checkButton;
        [SerializeField] private string gameNameString;
        [SerializeField] private TutorialManager tutorialManager;
        [SerializeField] private Transform optionParentTransform;
        [SerializeField] private Transform textParentTransfom;
        [SerializeField] private Image[] tempImages;
        [SerializeField] private TutorialHandOnComplete endTutorialHand;
        private int currentLevelIndex;
        private int correctValue;
        public static event Action IncorrectChoice;
        public static event Action StartAnimationOver;
        private GameCategoryDataManager gameCategoryDataManager;
        private UpdateCategoryApiManager updateCategoryApiManager;
        public int gameID;


        public int GetCurrentLevelNumber() => currentLevelIndex + 1;
        private void Awake()
        {
            #region GameID
#if PLAYSCHOOL_MAIN
             // assign varaible in this to get the  game ID from main app
             gameID  = PlayerPrefs.GetInt("currentGameId");
#endif
            #endregion
            gameCategoryDataManager = new GameCategoryDataManager(gameID, PlayerPrefs.GetString("currentGameName", gameNameString));
            updateCategoryApiManager = new UpdateCategoryApiManager(gameID);
            SetCurrentLevelIndex();
        }
        private void Start()
        {
            GameManagerCollect.Instance.OnLevelWin += IncrementLevelOnWin;
            GameManagerCollect.Instance.OnLevelWin += ScaleDownAndDisableCheckButton;
            GameManagerCollect.Instance.OnLevelStart += LevelStartAnimation;
            GameManagerCollect.Instance.OnGameEnd += ScaleDownAndDisableCheckButton;
            LoadLevel();
            checkButton.onClick.AddListener(CheckIfCorrect);
        }


        public void LoadLevel() => SetLevelData();


        private void IncrementLevelOnWin()
        {
            StartCoroutine(IncrementLevelOnWinAfterDelay());
        }
        private IEnumerator IncrementLevelOnWinAfterDelay()
        {
            SendStars();
            yield return new WaitForSeconds(0.5f);
            currentLevelIndex++;
            gameCategoryDataManager.SaveLevel(currentLevelIndex, levelSO.levelData.Length);
        }
        private void PlayTutorial()
        {
            StartCoroutine(PlayTutorialAfterDelay());
        }
        private IEnumerator PlayTutorialAfterDelay()
        {
            if (currentLevelIndex > 2)
            {
                yield break;
            }
            yield return new WaitForSeconds(1.5f);
            tutorialManager.StartHint();
        }
        private void SendStars()
        {
            int star = gameCategoryDataManager.GetLoadedstar;
            if (star >= 5)
            {
                updateCategoryApiManager.SetGameDataMore(levelSO.levelData.Length, levelSO.levelData.Length, 0, 5);
            }
            else
            {
                updateCategoryApiManager.SetGameDataMore(currentLevelIndex, levelSO.levelData.Length, 0, star);
            }
        }
        private void SetLevelData()
        {
            for (int i = 0; i < 3; i++)//hard coded 3
            {
                collectOptions[i].GetImage().sprite = levelSO.levelData[currentLevelIndex].optionList[i].dragObjectSprite;//set options sprite
                collectOptions[i].currentNumber = levelSO.levelData[currentLevelIndex].optionList[i].dragID;//set gargabe value from so                
            }
            int random = UnityEngine.Random.Range(0, 3);
            ObjectData correctObjectdata = levelSO.levelData[currentLevelIndex].levelObjectData;
            collectOptions[random].GetImage().sprite = correctObjectdata.spriteDatas[random].objectSprite;//set correct sprite in options;
            collectOptions[random].currentNumber = correctObjectdata.correctID;//setting correct id that is 1
            for (int i = 0; i < 3; i++)//set temp Images 
            {
                tempImages[i].sprite = collectOptions[i].GetImage().sprite;
            }
            correctValue = correctObjectdata.correctID * correctObjectdata.count;
            correctObjectText.text = "Collect " + correctObjectdata.count + " " + correctObjectdata.spriteDatas[random].objectName + "S";//setting correct text
            GameManagerCollect.Instance.InvokeLevelStart();                                                                                                                              //
        }
        private void CheckIfCorrect()
        {
            if (CollectDrop.sum == correctValue)
            {
                GameManagerCollect.Instance.InvokeLevelWin();
                GameManagerCollect.Instance.SoundManager.PlaySFX(Sounds.levelComplete);
            }
            else
            {
                GameManagerCollect.Instance.SoundManager.PlayIncorrectAudio();
                GameManagerCollect.Instance.LivesManager.ReduceLive();
                ScaleDownAndDisableCheckButton();
                IncorrectChoice?.Invoke();
            }
        }
        public void ScaleUpCheckAndEnableButton()
        {
            checkButton.gameObject.SetActive(true);
            checkButton.enabled = true;
            if (checkButton.enabled)
            {
                endTutorialHand.StartEndHint();
            }
        }
        private void SetCurrentLevelIndex()
        {
            currentLevelIndex = gameCategoryDataManager.GetCompletedLevel;
            if (currentLevelIndex > levelSO.levelData.Length - 1)
            {
                currentLevelIndex = 0;
                gameCategoryDataManager.SaveLevel(currentLevelIndex, levelSO.levelData.Length);
            }
        }
        private void ScaleDownAndDisableCheckButton()
        {
            checkButton.enabled = false;
            checkButton.gameObject.SetActive(false);
        }
        public void LoadNextLevel()
        {
            if (currentLevelIndex > levelSO.levelData.Length - 1)
            {
                currentLevelIndex = 0;
                gameCategoryDataManager.SaveLevel(currentLevelIndex, levelSO.levelData.Length);
                GameManagerCollect.Instance.InvokeGameEnd();
                GameManagerCollect.Instance.SoundManager.PlayFinalAudio();
                //EndLevelAnimation();
                return;
            }
            LoadLevel();
        }
        private void LevelStartAnimation()
        {
            float x1 = optionParentTransform.localPosition.x;
            float x2 = textParentTransfom.localPosition.x;
            optionParentTransform.DOLocalMoveX(-Screen.width, 0f);
            textParentTransfom.DOLocalMoveX(Screen.width, 0f);
            optionParentTransform.DOLocalMoveX(x1, 1f);
            textParentTransfom.DOLocalMoveX(x2, 1f).OnComplete(() =>
            {
                StartAnimationOver?.Invoke();
            });
            PlayTutorial();
        }
        private void OnDestroy()
        {
            GameManagerCollect.Instance.OnLevelWin -= IncrementLevelOnWin;
            GameManagerCollect.Instance.OnLevelWin -= ScaleDownAndDisableCheckButton;
            GameManagerCollect.Instance.OnLevelStart -= LevelStartAnimation;
            GameManagerCollect.Instance.OnGameEnd -= ScaleDownAndDisableCheckButton;
        }
    }
}
