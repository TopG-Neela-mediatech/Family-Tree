using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Sequence
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private SequenceLevelSO levels;
        [SerializeField] private DropDetector[] dropPrefabs;
        [SerializeField] private Dragscript[] dragPrefabs;
        [SerializeField] private Transform dropParentTransform;
        [SerializeField] private Transform dragParentTransform;
        [SerializeField] private Button checkSequenceButton;
        [SerializeField] private TutorialManager tutorialManager;
        [SerializeField] private TutorialHandOnComplete endTutorialHand;
        private Vector3 dragParentOGPosition;
        private Vector3 dropParentOGPosition;
        private int currentLevelIndex;
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
            dragParentOGPosition = dragParentTransform.localPosition;
            dropParentOGPosition = dropParentTransform.localPosition;
            GameManagerSequence.Instance.OnLevelWin += IncrementLevelOnWin;
            GameManagerSequence.Instance.OnLevelStart += StartDragandDropObjectAnimation;
            GameManagerSequence.Instance.OnLevelStart += PlayTutorial;
            GameManagerSequence.Instance.OnLevelStart += () => checkSequenceButton.gameObject.SetActive(false);
            GameManagerSequence.Instance.OnLevelWin += () => checkSequenceButton.gameObject.SetActive(false);
            GameManagerSequence.Instance.OnLevelLose += () => checkSequenceButton.gameObject.SetActive(false);
            LoadLevel();
            checkSequenceButton.onClick.AddListener(CheckSequqnce);
        }
        private void SetCurrentLevelIndex()
        {
            currentLevelIndex = gameCategoryDataManager.GetCompletedLevel;//PlayerPrefs.GetInt("CurrentLevel", 0);
            if (currentLevelIndex > levels.sequenceLevel.Length - 1)
            {
                currentLevelIndex = 0;
                gameCategoryDataManager.SaveLevel(currentLevelIndex, levels.sequenceLevel.Length);//PlayerPrefs.SetInt("CurrentLevel", currentLevelIndex);
            }
        }
        public void LoadLevel()
        {
            SetDragData();
            GameManagerSequence.Instance.InvokeLevelStart();
        }
        private void SendStars()
        {
            int star = gameCategoryDataManager.GetLoadedstar;
            if (star >= 5)
            {
                updateCategoryApiManager.SetGameDataMore(levels.sequenceLevel.Length, levels.sequenceLevel.Length, 0, 5);
            }
            else
            {
                updateCategoryApiManager.SetGameDataMore(currentLevelIndex, levels.sequenceLevel.Length, 0, star);
            }
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
        }
        private void SetDragData()
        {
            ShuffleList(levels.sequenceLevel[currentLevelIndex].boxAndValue);
            for (int i = 0; i < dragPrefabs.Length; i++)
            {
                Dragscript currentPrefab = dragPrefabs[i];
                Image numberImage = currentPrefab.GetComponent<Image>();
                numberImage.sprite = levels.sequenceLevel[currentLevelIndex].boxAndValue[i].numberboxSprite;
                currentPrefab.SetDragNumber(levels.sequenceLevel[currentLevelIndex].boxAndValue[i].value);
            }
        }
        private void StartDragandDropObjectAnimation()
        {
            dragParentTransform.DOLocalMoveX(-Screen.width, 0f);
            dropParentTransform.DOLocalMoveX(-Screen.width, 0f);
            dragParentTransform.DOLocalMoveX(dragParentOGPosition.x, 1f).OnComplete(() =>
            {
                SetDraggingForAll(true);
            });
            dropParentTransform.DOLocalMoveX(dropParentOGPosition.x, 1f);
        }
        public void EnableCheckButton()
        {
            for (int i = 0; i < dropPrefabs.Length; i++)
            {
                if (!dropPrefabs[i].IsFilled)
                {
                    checkSequenceButton.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    checkSequenceButton.gameObject.SetActive(true);

                }
            }
            if (checkSequenceButton.gameObject.activeSelf)
            {
                endTutorialHand.StartEndHint();
            }
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
        private void CheckSequqnce()
        {
            endTutorialHand.DescaleEndHand();
            string sequence = "";
            for (int i = 0; i < dropPrefabs.Length; i++)
            {
                sequence += dropPrefabs[i].GetStoredNumber().ToString();
            }
            if (sequence == levels.sequenceLevel[currentLevelIndex].correctSequence)
            {
                GameManagerSequence.Instance.InvokeLevelWin();
                GameManagerSequence.Instance.SoundManager.PlaySFX(Sounds.levelComplete);
            }
            else
            {
                GameManagerSequence.Instance.SoundManager.PlayIncorrectAudio();
                GameManagerSequence.Instance.LivesManager.ReduceLive();
                ResetDragObjectsToOGPosi();
            }
        }
        private void ResetDragObjectsToOGPosi()
        {
            for (int i = 0; i < dragPrefabs.Length; i++)
            {
                dragPrefabs[i].MoveToOGPosi();
            }
        }
        private void SetDraggingForAll(bool status)
        {
            for (int i = 0; i < dragPrefabs.Length; i++)
            {
                dragPrefabs[i].IsDraggable = status;
                ;
            }
        }
        private void IncrementLevelOnWin()
        {
            StartCoroutine(IncrementLevelOnWinAfterDelay());
        }
        private IEnumerator IncrementLevelOnWinAfterDelay()
        {
            SendStars();
            yield return new WaitForSeconds(0.5f);
            currentLevelIndex++;
            gameCategoryDataManager.SaveLevel(currentLevelIndex, levels.sequenceLevel.Length);
        }
        public void LoadNextLevel()
        {
            if (currentLevelIndex > levels.sequenceLevel.Length - 1)
            {
                currentLevelIndex = 0;
                gameCategoryDataManager.SaveLevel(currentLevelIndex, levels.sequenceLevel.Length);
                GameManagerSequence.Instance.InvokeGameEnd();
                GameManagerSequence.Instance.SoundManager.PlayFinalAudio();
                return;
            }
            LoadLevel();
        }


        public int GetCurrentLevelNumber() => currentLevelIndex + 1;


        private void OnDestroy()
        {
            GameManagerSequence.Instance.OnLevelWin -= IncrementLevelOnWin;
            GameManagerSequence.Instance.OnLevelStart -= StartDragandDropObjectAnimation;
            GameManagerSequence.Instance.OnLevelStart -= () => checkSequenceButton.gameObject.SetActive(false);
            GameManagerSequence.Instance.OnLevelWin -= () => checkSequenceButton.gameObject.SetActive(false);
            GameManagerSequence.Instance.OnLevelLose -= () => checkSequenceButton.gameObject.SetActive(false);
            GameManagerSequence.Instance.OnLevelStart -= PlayTutorial;
        }
    }
}
