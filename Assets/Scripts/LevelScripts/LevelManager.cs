using System;
using System.Collections.Generic;
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
        private TreeController treeController;
        private GameCategoryDataManager gameCategoryDataManager;
        private UpdateCategoryApiManager updateCategoryApiManager;
        private int currentLevelIndex;


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
            SetLevelData();
        }
        private void SetLevelData()
        {
            DisableFamilyMembers();
            SetTree();
            SetFamilyMember();
            SetHintText();
            SetRevealedMemberData();
        }
        private void SetTree()
        {
            GameObject Tree = Instantiate(levels[currentLevelIndex].treeSprite, treeParent);
            treeController = Tree.GetComponent<TreeController>();
        }
        private void SetFamilyMember()
        {
            for (int i = 0; i < levels[currentLevelIndex].memberData.Length; i++)
            {
                familyMembers[i].SetData(levels[currentLevelIndex].memberData[i].faceSprite, levels[currentLevelIndex].memberData[i].Name);
            }
            familyMembers[0].gameObject.SetActive(true);
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
        private void SetHintText()
        {
            hintText.text = "";
            for (int i = 0; i < levels[currentLevelIndex].memberData.Length; i++)
            {
                hintText.text += levels[currentLevelIndex].memberData[i].Description + "\n";
            }
        }
        private void SetRevealedMemberData()
        {
            if (treeController != null)
            {
                foreach (var revealedMember in levels[currentLevelIndex].revealedMembers)
                {
                    DropController dc = treeController.GetDropController(revealedMember.Key);
                    Debug.Log("Reached");
                    dc.SetRevealedData(revealedMember.faceSprite, revealedMember.Name);
                    dc.enabled = false;//setting trigger of drop zone false hopefully
                }
            }
        }
    }
}
