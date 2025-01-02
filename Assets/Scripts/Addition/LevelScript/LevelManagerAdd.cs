using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Add
{
    public class LevelManagerAdd : MonoBehaviour
    {
        [SerializeField] private Transform numberParentTransform;
        [SerializeField] private Transform[] individualNumberTransform;
        [SerializeField] private Transform optionParentTransform;
        [SerializeField] private Image[] numberImages;
        [SerializeField] private Image answerImage;
        [SerializeField] private GridLayoutGroup gridLayoutGroup1;
        [SerializeField] private GridLayoutGroup gridLayoutGroup2;
        [SerializeField] private ObjectHandler[] objectPrefabs1;
        [SerializeField] private ObjectHandler[] objectPrefabs2;
        [SerializeField] private OptionHandler[] optionsPrefabs;
        [SerializeField] private AddLevelSO levels;
        [SerializeField] private Sprite emptyBoxSprite;
        [SerializeField] private TutorialHandOnComplete tutorialHand;
        [SerializeField] private Sprite[] objectSprites;
        [SerializeField] private Image optionParentImage;
        [SerializeField] private GameObject conffetiBurst;
        public event Action OnLevelStartAnimationOver;
        private int currentLevelIndex;
        private int value1;
        private int value2;
        private GameCategoryDataManager gameCategoryDataManager;
        private UpdateCategoryApiManager updateCategoryApiManager;
        public int gameID;


        public int correctAnswer { get; private set; }
        public int GetCurrentLevelNumber() => currentLevelIndex + 1;
        private void IncrementLevelOnWin() => StartCoroutine(IncrementLevelOnWinAfterDelay());
        public void LoadLevel() => SetLevelData();
        public void EnableConfetti() => conffetiBurst.SetActive(true);
        private void DisableConfetti() => conffetiBurst?.SetActive(false);


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
            LevelEndAnimation();
            GameManagerAdd.Instance.OnLevelStart += () => StartCoroutine(LevelStartAnimation());
            GameManagerAdd.Instance.OnLevelWin += LevelEndAnimation;
            GameManagerAdd.Instance.OnLevelWin += IncrementLevelOnWin;
            GameManagerAdd.Instance.OnLevelLose += LevelEndAnimation;
            GameManagerAdd.Instance.OnGameEnd += LevelEndAnimation;
            GameManagerAdd.Instance.OnLevelWin += DisableConfetti;
            LoadLevel();
        }
        private void SetCurrentLevelIndex()
        {
            currentLevelIndex = gameCategoryDataManager.GetCompletedLevel;
            if (currentLevelIndex > levels.outerLevels.Length - 1)
            {
                currentLevelIndex = 0;
                gameCategoryDataManager.SaveLevel(currentLevelIndex, levels.outerLevels.Length);
            }
        }
        private void DisableAllObjects()
        {
            foreach (var item in objectPrefabs1)
            {
                item.gameObject.SetActive(false);
            }
            foreach (var item in objectPrefabs2)
            {
                item.gameObject.SetActive(false);
            }
        }

        private void SendStars()
        {
            int star = gameCategoryDataManager.GetLoadedstar;
            if (star >= 5)
            {
                updateCategoryApiManager.SetGameDataMore(levels.outerLevels.Length, levels.outerLevels.Length, 0, 5);
            }
            else
            {
                updateCategoryApiManager.SetGameDataMore(currentLevelIndex, levels.outerLevels.Length, 0, star);
            }
        }
        /*private void PlayTutorial()
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
        }*/

        private IEnumerator IncrementLevelOnWinAfterDelay()
        {
            SendStars();
            yield return new WaitForSeconds(0.5f);
            currentLevelIndex++;
            gameCategoryDataManager.SaveLevel(currentLevelIndex, levels.outerLevels.Length);
        }
        public void LoadNextLevel()
        {
            if (currentLevelIndex > levels.outerLevels.Length - 1)
            {
                currentLevelIndex = 0;
                gameCategoryDataManager.SaveLevel(currentLevelIndex, levels.outerLevels.Length);
                GameManagerAdd.Instance.InvokeGameEnd();
                GameManagerAdd.Instance.SoundManager.PlayFinalAudio();
                return;
            }
            LoadLevel();
        }
        private void SetLevelData()
        {
            DisableAllObjects();
            int randomLevel = UnityEngine.Random.Range(0, levels.outerLevels[currentLevelIndex].innerLevels.Length);
            correctAnswer = levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].correctAnswer;
            for (int i = 0; i < numberImages.Length; i++)//loop setting numbers to be added sprite
            {
                numberImages[i].sprite = levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].numbersToBeAdded[i].numberSprite;
            }
            for (int i = 0; i < optionsPrefabs.Length; i++)//loop setting options
            {
                optionsPrefabs[i].currentValue = levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].optionData[i].value;
                optionsPrefabs[i].SetOptionImage(levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].optionData[i].numberSprite);
            }
            int random = UnityEngine.Random.Range(0, objectSprites.Length);
            value1 = levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].numbersToBeAdded[0].value;
            value2 = levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].numbersToBeAdded[1].value;
            for (int i = 0; i < value1; i++)//loop setting first set of prefabs starting from 1
            {
                objectPrefabs1[i].SetObjectImage(objectSprites[random]);
                objectPrefabs1[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < value2; i++)//loop setting second set of prefabs starting from 1
            {
                objectPrefabs2[i].SetObjectImage(objectSprites[random]);
                objectPrefabs2[i].gameObject.SetActive(true);
            }
            SetGridLayoutData(randomLevel);
            StartCoroutine(InvokeLevelStartAfterDelay());
        }
        private void SetGridLayoutData(int randomLevel)
        {
            gridLayoutGroup1.cellSize = new Vector2(levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].cellSize1,
                levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].cellSize1);//setting cell size
            gridLayoutGroup2.cellSize = new Vector2(levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].cellSize2,
                levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].cellSize2);
            gridLayoutGroup1.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup1.constraintCount = SetGridConstraint(levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].
                numbersToBeAdded[0].value);
            gridLayoutGroup2.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup2.constraintCount = SetGridConstraint(levels.outerLevels[currentLevelIndex].innerLevels[randomLevel].
                numbersToBeAdded[1].value);
        }
        private int SetGridConstraint(int count)
        {
            switch (count)
            {
                case 1: return 1;
                case 2: return 2;
                case 3: return 2;
                case 4: return 2;
                case 5: return 3;
                case 6: return 3;
                case 7: return 3;
            }
            return 3;
        }
        private IEnumerator InvokeLevelStartAfterDelay()
        {
            yield return new WaitForSeconds(0f);
            GameManagerAdd.Instance.InvokeLevelStart();
        }
        private IEnumerator LevelStartAnimation()
        {
            answerImage.sprite = emptyBoxSprite;//setting the emptyboxsprite on levelstart
            yield return new WaitForSeconds(2f);
            numberParentTransform.DOScale(1f, 0f).OnComplete(() =>
            {
                GameManagerAdd.Instance.SoundManager.PlayCurrentNumberSound(value1);
                individualNumberTransform[0].DOScale(1f, 0.7f).OnComplete(() =>//first number
                {
                    GameManagerAdd.Instance.SoundManager.PlaySFX(Sounds.signSound);
                    individualNumberTransform[1].DOScale(1f, 0.7f).OnComplete(() =>//plus sign
                    {
                        GameManagerAdd.Instance.SoundManager.PlayCurrentNumberSound(value2);
                        individualNumberTransform[2].DOScale(1f, 0.7f).OnComplete(() =>//second number
                        {
                            GameManagerAdd.Instance.SoundManager.PlaySFX(Sounds.equalsToSound);
                            individualNumberTransform[3].DOScale(1f, 0.7f).OnComplete(() =>//equal sign
                            {
                                individualNumberTransform[4].DOScale(1f, 0.7f).OnComplete(() =>
                                {
                                    OnLevelStartAnimationOver?.Invoke();
                                    optionParentTransform.DOLocalMoveX(0, 0.5f).OnComplete(() =>//options come after this
                                    {
                                        optionParentImage.enabled = true;
                                    });
                                    tutorialHand.StartEndHint();
                                });
                            });
                        });
                    });
                });
            }); ;
        }
        public void ScaleOnEnd(Transform optionTransform)
        {
            GameManagerAdd.Instance.SoundManager.PlayCurrentNumberSound(value1);
            individualNumberTransform[0].DOScale(new Vector3(1.25f, 1.25f), 0.7f).OnComplete(() =>//first number
            {
                individualNumberTransform[0].DOScale(Vector3.one, 0.25f);
                GameManagerAdd.Instance.SoundManager.PlaySFX(Sounds.signSound);
                individualNumberTransform[1].DOScale(new Vector3(1.25f, 1.25f), 0.7f).OnComplete(() =>//plus sign
                {
                    individualNumberTransform[1].DOScale(Vector3.one, 0.25f);
                    GameManagerAdd.Instance.SoundManager.PlayCurrentNumberSound(value2);
                    individualNumberTransform[2].DOScale(new Vector3(1.25f, 1.25f), 0.7f).OnComplete(() =>//second sign
                    {
                        individualNumberTransform[2].DOScale(Vector3.one, 0.25f);
                        GameManagerAdd.Instance.SoundManager.PlaySFX(Sounds.equalsToSound);
                        individualNumberTransform[3].DOScale(new Vector3(1.25f, 1.25f), 0.7f).OnComplete(() =>//equal sign
                        {
                            individualNumberTransform[3].DOScale(Vector3.one, 0.25f);
                            GameManagerAdd.Instance.SoundManager.PlayCurrentNumberSound(correctAnswer);
                            optionTransform.DOScale(new Vector3(1.5f, 1.5f), 0.7f).OnComplete(() =>//the button thats on equal sign 1.5 because already scaled up
                            {
                                optionTransform.DOScale(new Vector3(1.3f, 1.3f), 0.25f);
                                StartCoroutine(InvokeWinAfterDelay());
                            });
                        });
                    });
                });
            });
        }
        private IEnumerator InvokeWinAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            GameManagerAdd.Instance.InvokeLevelWin();
        }
        private void LevelEndAnimation()
        {
            optionParentImage.enabled = false;
            numberParentTransform.DOScale(0f, 0f);
            optionParentTransform.DOLocalMoveX(Screen.width, 0f);
            individualNumberTransform[0].DOScale(0f, 0f);
            individualNumberTransform[1].DOScale(0f, 0f);
            individualNumberTransform[2].DOScale(0f, 0f);
            individualNumberTransform[3].DOScale(0f, 0f);
            individualNumberTransform[4].DOScale(0f, 0f);
        }
        private void OnDestroy()
        {
            GameManagerAdd.Instance.OnLevelStart -= () => StartCoroutine(LevelStartAnimation());
            GameManagerAdd.Instance.OnLevelWin -= LevelEndAnimation;
            GameManagerAdd.Instance.OnLevelLose -= LevelEndAnimation;
            GameManagerAdd.Instance.OnGameEnd -= LevelEndAnimation;
            GameManagerAdd.Instance.OnLevelWin -= DisableConfetti;
        }
    }
}
