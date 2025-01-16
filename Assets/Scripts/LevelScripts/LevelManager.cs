using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;


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
        private TreeController currenttreeController;
        private GameCategoryDataManager gameCategoryDataManager;
        private UpdateCategoryApiManager updateCategoryApiManager;
        private int currentLevelIndex;
        private int currentActiveMemberIndex;
        private DragScript currentActiveMember;
        private int attempts;


        public Vector3 GetDragPosition() => currentActiveMember.transform.localPosition;
        public Transform GetDropTransform() => currenttreeController.GetDropController(currentActiveMember.value).transform;
        private void ActivateHint(DragScript currentDraggable, DropController correctDropBox) =>
            GameManager.Instance.HandManager.StartHandTutorial(currentDraggable.transform.localPosition, correctDropBox.transform);
        private void DestroyTree() => Destroy(currenttreeController.gameObject);
        private void EnableCorrectDropZone(DropController dropController) => dropController.EnableCollider();
        private void LevelStartAnimation() => AnimateInfoArea();


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
            SetMemberScaleAndPosition();
        }
        private void Start()
        {
            GameManager.Instance.OnTreeComplete += this.OnTreeComplete;
            GameManager.Instance.OnLevelWin += IncrementLevel;
            GameManager.Instance.OnLevelWin += DestroyTree;
            GameManager.Instance.OnLevelWin += ResetFamilyMemberPosition;
            GameManager.Instance.OnLevelStart += LevelStartAnimation;
            currentLevelIndex = 0;
            SetLevelData();
        }
        private void ResetData()
        {
            currentActiveMemberIndex = -1;//increment first to acess the first member
            attempts = 3;
        }
        private void SetMemberScaleAndPosition()
        {
            foreach (var member in familyMembers)
            {
                memberPositionSetter.SetFamilyMemberPositionAndScale(member.transform);
            }
        }
        private void EnableLevel()
        {
            levelUIMain.SetActive(true);
            levelMain.SetActive(true);
            infoAreaParent.SetActive(true);
            leavesEffect.SetActive(true);
        }
        private void OnTreeComplete()
        {
            levelMain.SetActive(false);
            levelUIMain.SetActive(false);
            infoAreaParent.SetActive(false);
            leavesEffect.SetActive(false);
        }
        private void SetLevelData()
        {
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
            infoAreaTransform.DOLocalMoveX(-Screen.width, 0f);
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
                StartCoroutine(TreeEndAnimation());//tree complete here
                return;
            }
            currentActiveMemberIndex++;
            currentActiveMember = familyMembers[currentActiveMemberIndex];//setting the reference for active member;         
            StartCoroutine(SetHintText());//text animation here
            Vector3 actualScale = currentActiveMember.transform.localScale;
            familyMembers[currentActiveMemberIndex].transform.DOScale(0f, 0f).OnComplete(() =>
            {
                familyMembers[currentActiveMemberIndex].gameObject.SetActive(true);
                familyMembers[currentActiveMemberIndex].transform.DOScale(actualScale, 2f).OnComplete(() =>
                {
                    familyMembers[currentActiveMemberIndex].enabled = true;//on member spawning complete
                    DropController dc = currenttreeController.GetDropController(currentActiveMember.value);
                    EnableCorrectDropZone(dc);//enabling the correct drop zone rest all disabled
                });
            });
        }
        private void IncrementLevel()
        {
            currentLevelIndex++;
            gameCategoryDataManager.SaveLevel(currentLevelIndex, levels.Length);
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
            SetLevelData();
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
                item.gameObject.transform.localPosition = Vector3.zero;
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
        /*private void SetRevealedMemberData()
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
                    hintText.text += "\n";
                }
                if (i == fullText.Length - 1)
                {
                    familyMembers[currentActiveMemberIndex].enabled = true;
                }
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnTreeComplete -= this.OnTreeComplete;
            GameManager.Instance.OnLevelWin -= IncrementLevel;
            GameManager.Instance.OnLevelWin -= DestroyTree;
            GameManager.Instance.OnLevelWin -= ResetFamilyMemberPosition;
            GameManager.Instance.OnLevelStart -= LevelStartAnimation;
        }

    }
}
/*private void ActivateHint2(DragScript currentDraggable, DropController correctDropBox) { 

           currentDraggable.enabled = false;
           correctDropBox.DisableChecking();

           currentDraggable.transform.SetParent(currenttreeController.transform);//use tree controller as parent as another level in heirarchy is added
           currentDraggable.transform.DOLocalMove(correctDropBox.transform.localPosition, 1f).OnComplete(() =>
           {
               currentDraggable.transform.SetParent(familyMemeberParent);
               DropController.canCheck = true;
               EnableNextMember();
               attempts = 3;
           });
       }
*/

