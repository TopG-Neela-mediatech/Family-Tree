using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using IndianFontCorrector.ConvertLanguage;


namespace TMKOC.FamilyTree
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject levelMain;
        [SerializeField] private GameObject levelUIMain;
        [SerializeField] private Transform treeParent;
        [SerializeField] private Transform familyMemeberParent;
        [SerializeField] private LevelSO[] levels;
        [SerializeField] private int gameID;
        [SerializeField] private DragScript[] familyMembers;
        [SerializeField] private TextMeshProUGUI hintText;
        [SerializeField] private float typingSpeed = 0.3f;
        [SerializeField] private Transform infoAreaTransform;
        [SerializeField] private GameObject confettiPrefab;
        [SerializeField] private GameObject infoAreaParent;
        [SerializeField] private GameObject leavesEffect;
        [SerializeField] private MemberPositionSetter memberPositionSetter;
        private Coroutine infoAreaCoroutine;
        private Vector3 familyMemberLocalPosition;
        private TreeController currenttreeController;
        private GameCategoryDataManager gameCategoryDataManager;
        private UpdateCategoryApiManager updateCategoryApiManager;
        private int currentLevelIndex;
        private int currentActiveMemberIndex;
        private DragScript currentActiveMember;
        private int attempts;
        private FamilyMember currentMemberEnum;
        private Coroutine treeCompleteCoroutine;
        private string language;
        private DescriptionLanguage descriptionLanguageEnum;


        public Vector3 GetDragPosition() => currentActiveMember.transform.localPosition;
        public Transform GetDropTransform() => currenttreeController.GetDropController(currentActiveMember.value).transform;
        private void ActivateHint(DragScript currentDraggable, DropController correctDropBox) =>
            GameManager.Instance.HandManager.StartHandTutorial(currentDraggable.transform.localPosition, correctDropBox.transform);
        private void EnableCorrectDropZone(DropController dropController) => dropController.EnableCollider();
        private void LevelStartAnimation() => AnimateInfoArea();
        public int GetLevelIndex() => currentLevelIndex;
        public void SaveFailedAttempts() => updateCategoryApiManager.SetAttemps();


        private void Awake()
        {
            #region GameID
#if PLAYSCHOOL_MAIN
             // assign varaible in this to get the  game ID from main app
             gameID  = PlayerPrefs.GetInt("currentGameId");
#endif
            #endregion
            gameCategoryDataManager = new GameCategoryDataManager(gameID, "familytree");
            updateCategoryApiManager = new UpdateCategoryApiManager(gameID);
            SetCurrentLevelIndex();
            SetMemberScaleAndPosition();
            SetDescriptionLanguageEnum();
        }
        private void Start()
        {
            GameManager.Instance.UIManager.OnMenuPressed += StopTreeComplete;
            GameManager.Instance.OnTreeComplete += this.OnTreeComplete;
            GameManager.Instance.OnLevelWin += IncrementLevel;
            GameManager.Instance.OnLevelWin += DestroyTree;
            GameManager.Instance.OnLevelWin += ResetFamilyMemberPosition;
            GameManager.Instance.OnLevelStart += LevelStartAnimation;
            GameManager.Instance.OnLevelWin += SetMemberScaleAndPosition;
            GameManager.Instance.OnLevelWin += () => attempts = 3;
            GameManager.Instance.OnTreeComplete += () => infoAreaParent.SetActive(false);
            familyMemberLocalPosition = familyMembers[0].transform.localPosition;
        }
        private void SetDescriptionLanguageEnum()
        {
            language = PlayerPrefs.GetString("PlaySchoolLanguage", "English");
            switch (language)
            {
                case "English":
                    descriptionLanguageEnum = DescriptionLanguage.English;
                    break;
                case "Hindi":
                    descriptionLanguageEnum = DescriptionLanguage.Hindi;
                    ConvertLang.SetLanguage(Language.Hindi);
                    break;
                case "Tamil":
                    descriptionLanguageEnum = DescriptionLanguage.Tamil;
                    ConvertLang.SetLanguage(Language.Tamil);
                    break;
                case "Marathi":
                    descriptionLanguageEnum = DescriptionLanguage.Marathi;
                    ConvertLang.SetLanguage(Language.Marathi);
                    break;
                case "Bengali":
                    descriptionLanguageEnum = DescriptionLanguage.Bengali;
                    ConvertLang.SetLanguage(Language.Bengali);
                    break;
                default:
                    descriptionLanguageEnum = DescriptionLanguage.English;
                    break;
            }
        }
        private void StopTreeComplete()
        {
            if (treeCompleteCoroutine != null)
            {
                StopCoroutine(treeCompleteCoroutine);
            }
        }
        private void ResetData()
        {
            currentActiveMemberIndex = -1;//increment first to acess the first member
            attempts = 3;
        }
        private void DestroyTree()
        {
            if (currenttreeController != null)
            {
                Destroy(currenttreeController.gameObject);
            }
        }
        private void SetMemberScaleAndPosition()
        {
            foreach (var member in familyMembers)
            {
                memberPositionSetter.SetFamilyMemberPositionAndScale(member.transform);
            }
        }
        private void SendStars()
        {
            int _star = gameCategoryDataManager.GetLoadedstar;
            if (_star >= 5)
            {
                updateCategoryApiManager.SetGameDataMore(currentLevelIndex, levels.Length, 5);
            }
            else
            {
                updateCategoryApiManager.SetGameDataMore(currentLevelIndex, levels.Length, _star);
            }
        }
        private void EnableLevel()
        {
            levelUIMain.SetActive(true);
            levelMain.SetActive(true);
            infoAreaParent.SetActive(true);
            leavesEffect.SetActive(true);
        }
        public void DisableLevel()
        {
            levelUIMain.SetActive(false);
            levelMain.SetActive(false);
            infoAreaParent.SetActive(false);
            leavesEffect.SetActive(false);
            DestroyTree();
            ResetFamilyMemberPosition();
        }
        private void OnTreeComplete()
        {
            levelMain.SetActive(false);
            levelUIMain.SetActive(false);
            infoAreaParent.SetActive(false);
            leavesEffect.SetActive(false);
        }
        public void LoadLevel(int levelNumber)
        {
            StartCoroutine(LoadLevelAfterDelay(levelNumber));
        }
        private IEnumerator LoadLevelAfterDelay(int levelNumber)
        {
            yield return new WaitForSeconds(1f);
            if (levelNumber > levels.Length - 1)
            {
                Debug.Log("Invalid Level Number");
                yield break;
            }
            currentLevelIndex = levelNumber;
            ResetData();
            EnableLevel();
            DisableFamilyMembers();
            SetTree();
            SetFamilyMember();
            //SetRevealedMemberData();
            GameManager.Instance.InvokeLevelStart();
        }
        private void AnimateInfoArea()
        {
            infoAreaTransform.gameObject.SetActive(false);
            infoAreaTransform.DOLocalMoveX(-Screen.width, 0f).OnComplete(() =>
            {
                infoAreaTransform.gameObject.SetActive(true);
            });
            infoAreaTransform.DOLocalMoveX(100f, 0.5f);//hard coded
        }
        private void SetTree()
        {
            GameObject tree = Instantiate(levels[currentLevelIndex].treeObject.gameObject, treeParent);
            currenttreeController = tree.GetComponent<TreeController>();
        }
        private void SetFamilyMember()
        {
            for (int i = 0; i < levels[currentLevelIndex].memberData.Length; i++)
            {
                familyMembers[i].SetData(levels[currentLevelIndex].memberData[i].faceSprite, levels[currentLevelIndex].memberData[i].Name,
                    levels[currentLevelIndex].memberData[i].Key);
            }
            EnableNextMember();//setting active first member
        }
        public void EnableNextMember()
        {
            if (currentActiveMemberIndex == levels[currentLevelIndex].memberCount - 1)
            {
                treeCompleteCoroutine = StartCoroutine(TreeEndAnimation());//tree complete here
                return;
            }
            currentActiveMemberIndex++;
            currentMemberEnum = levels[currentLevelIndex].memberData[currentActiveMemberIndex].member;
            currentActiveMember = familyMembers[currentActiveMemberIndex];//setting the reference for active member;         
            if (infoAreaCoroutine != null)
            {
                StopCoroutine(infoAreaCoroutine);
            }
            infoAreaCoroutine = StartCoroutine(SetHintText());//text animation here
            GameManager.Instance.SoundManager.PlayCurrentMemberSound(currentMemberEnum);
            Vector3 actualScale = currentActiveMember.transform.localScale;
            familyMembers[currentActiveMemberIndex].transform.DOScale(0f, 0f).OnComplete(() =>
            {
                familyMembers[currentActiveMemberIndex].gameObject.SetActive(true);
                familyMembers[currentActiveMemberIndex].transform.DOScale(actualScale, 2f).OnComplete(() =>
                {
                    familyMembers[currentActiveMemberIndex].enabled = true;//on member spawning complete
                    if (currenttreeController != null)
                    {
                        DropController dc = currenttreeController.GetDropController(currentActiveMember.value);
                        EnableCorrectDropZone(dc);//enabling the correct drop zone rest all disabled
                    }
                });
            });
        }
        private void IncrementLevel() => StartCoroutine(IncrementLevelAfterDElay());
        private IEnumerator IncrementLevelAfterDElay()
        {
            yield return new WaitForSeconds(0.1f);
            currentLevelIndex++;
            gameCategoryDataManager.SaveLevel(currentLevelIndex, levels.Length);
            SendStars();
        }
        public void LoadNextLevel()
        {
            if (currentLevelIndex > levels.Length - 1)
            {
                currentLevelIndex = 0;
                gameCategoryDataManager.SaveLevel(currentLevelIndex, levels.Length);
                GameManager.Instance.InvokeGameEnd();
                //GameManager.Instance.SoundManager.PlayFinalAudio();
                return;
            }
            LoadLevel(currentLevelIndex);
        }
        public void StartHint()
        {
            attempts--;
            if (attempts <= 0)
            {
                DropController correctDropBox = currenttreeController.GetDropController(currentActiveMember.value);
                if (!GameManager.Instance.HandManager.isPlaying)
                {
                    ActivateHint(currentActiveMember, correctDropBox);
                    attempts = 3;//Resetting attempt counter;
                }
            }
        }
        private IEnumerator TreeEndAnimation()
        {
            confettiPrefab.SetActive(true);
            yield return new WaitForSeconds(3f);
            GameManager.Instance.InvokeTreeComplete();
            confettiPrefab.SetActive(false);
        }
        private void DisableFamilyMembers()
        {
            foreach (var item in familyMembers)
            {
                item.gameObject.SetActive(false);
            }
        }
        private void ResetFamilyMemberPosition()
        {
            foreach (var item in familyMembers)
            {
                item.gameObject.transform.localPosition = familyMemberLocalPosition;
            }

        }
        private void SetCurrentLevelIndex()
        {
            currentLevelIndex = gameCategoryDataManager.GetCompletedLevel;
            if (currentLevelIndex > levels.Length - 1)
            {
                currentLevelIndex = 0;
                gameCategoryDataManager.SaveLevel(currentLevelIndex, levels.Length);
            }
        }
        private IEnumerator SetHintText()
        {
            hintText.font = GetRespectiveFont();
            hintText.text = "";
            yield return new WaitForSeconds(0.5f);
            string fullText = GetLocalizedDescritpion();
            if (fullText != null)
            {
                for (int i = 0; i < fullText.Length; i++)
                {
                    hintText.text += fullText[i];
                    if (fullText[i] == '.')
                    {
                        hintText.text += "\n";
                    }
                    if (i == fullText.Length - 1)
                    {
                        familyMembers[currentActiveMemberIndex].enabled = true;
                    }
                    yield return new WaitForSeconds(typingSpeed);
                }
            }
        }
        private string GetLocalizedDescritpion()
        {
            LocalDescription ld = Array.Find(levels[currentLevelIndex].memberData[currentActiveMemberIndex].Description,
                i => i.language == descriptionLanguageEnum);
            if (ld != null)
            {
                return ld.description;
            }
            LocalDescription la = Array.Find(levels[currentLevelIndex].memberData[currentActiveMemberIndex].Description,
                i => i.language == DescriptionLanguage.English);
            return la.description;
        }
        private TMP_FontAsset GetRespectiveFont()
        {
            FontData FD = Array.Find(levels[currentLevelIndex].fontDatas,
                i => i.language == descriptionLanguageEnum);
            if (FD != null)
            {
                return FD.fontAsset;
            }
            FontData FA = Array.Find(levels[currentLevelIndex].fontDatas,
                i => i.language == DescriptionLanguage.English);
            return FA.fontAsset;
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnTreeComplete -= this.OnTreeComplete;
            GameManager.Instance.OnLevelWin -= IncrementLevel;
            GameManager.Instance.OnLevelWin -= DestroyTree;
            GameManager.Instance.OnLevelWin -= ResetFamilyMemberPosition;
            GameManager.Instance.OnLevelStart -= LevelStartAnimation;
            GameManager.Instance.OnLevelWin -= SetMemberScaleAndPosition;
            GameManager.Instance.OnLevelWin -= () => attempts = 3;
            GameManager.Instance.OnTreeComplete -= () => infoAreaParent.SetActive(false);
            GameManager.Instance.UIManager.OnMenuPressed -= StopTreeComplete;
        }

    }
}

