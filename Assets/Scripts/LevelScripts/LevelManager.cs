using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;


namespace TMKOC.FamilyTree
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform treeParent;
        [SerializeField] private Transform familyMemeberParent;
        [SerializeField] private LevelSO[] levels;
        [SerializeField] private int gameID;
        [SerializeField] private DragScript[] familyMembers;
        [SerializeField] private TextMeshProUGUI hintText;
        [SerializeField] private float typingSpeed = 0.3f;
        private TreeController currenttreeController;
        private GameCategoryDataManager gameCategoryDataManager;
        private UpdateCategoryApiManager updateCategoryApiManager;
        private int currentLevelIndex;
        private int currentActiveMemberIndex;
        private DragScript currentActiveMember;
        private int attempts;


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
            currentLevelIndex = 0;
            currentActiveMemberIndex = -1;//increment first to acess the first member
            SetLevelData();
            attempts = 3;
        }
        private void SetLevelData()
        {
            DisableFamilyMembers();
            SetTree();
            SetFamilyMember();
            //SetRevealedMemberData();
        }
        private void SetTree()
        {
            Instantiate(levels[currentLevelIndex].treeObject.gameObject, treeParent);
            currenttreeController = levels[currentLevelIndex].treeObject;
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
                Debug.Log("Win");
                return;
            }
            currentActiveMemberIndex++;
            currentActiveMember = familyMembers[currentActiveMemberIndex];//setting the reference for active member;
            StartCoroutine(SetHintText());//text animation here
            familyMembers[currentActiveMemberIndex].transform.DOScale(0f, 0f).OnComplete(() =>
            {
                familyMembers[currentActiveMemberIndex].gameObject.SetActive(true);
                familyMembers[currentActiveMemberIndex].transform.DOScale(1f, 1f).OnComplete(() =>
                {
                    if (currentActiveMemberIndex == 0)
                    {
                        GameManager.Instance.InvokeLevelStart();//level start here
                    }
                });
            });
        }
        public void StartHint()
        {
            if (attempts <= 3)
            {
                DropController correctDropBox = currenttreeController.GetDropController(currentActiveMember.value);
                ActivateHint(currentActiveMember, correctDropBox);
            }
        }
        public void ReduceAttempts() => attempts--;
        private void ActivateHint(DragScript currentDraggable, DropController correctDropBox)
        {
            currentDraggable.enabled = false;
            correctDropBox.DisableChecking();
            currentDraggable.transform.SetParent(treeParent);
            currentDraggable.transform.DOLocalMove(correctDropBox.transform.localPosition, 1f).OnComplete(() =>
            {
                currentDraggable.transform.SetParent(familyMemeberParent);
                DropController.canCheck = true;
                EnableNextMember();
                attempts = 3;//Resetting attempt counter;
            });
        }
        private void DisableFamilyMembers()
        {
            foreach (var item in familyMembers)
            {
                item.gameObject.SetActive(false);
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
            hintText.text = "";
            yield return new WaitForSeconds(0.5f);
            string fullText = levels[currentLevelIndex].memberData[currentActiveMemberIndex].Description;
            for (int i = 0; i < fullText.Length; i++)
            {
                hintText.text += fullText[i];
                if (fullText[i] == '.')
                {
                    // Add a new line after the full stop
                    hintText.text += "\n";
                }
                if (i == fullText.Length - 1)
                {
                    familyMembers[currentActiveMemberIndex].enabled = true;

                }
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        /* private void SetRevealedMemberData()
         {
             if (currenttreeController != null)
             {
                 foreach (var revealedMember in levels[currentLevelIndex].revealedMembers)
                 {
                     DropController dc = currenttreeController.GetDropController(revealedMember.Key);
                     dc.SetRevealedData(revealedMember.faceSprite, revealedMember.Name);
                     dc.enabled = false;//setting trigger of drop zone false hopefully
                 }
             }
         }*/
    }
}
