using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.FamilyTree
{
    public class HintManager : MonoBehaviour
    {
        [SerializeField] private Transform HintParentTransform;
        [SerializeField] private Button HintButton;


        private void Start()
        {
            HintButton.onClick.AddListener(OnHintClick);
            GameManager.Instance.OnLevelStart += ScaleHintParent;
            GameManager.Instance.OnLevelWin += DeScaleHintParent;
            GameManager.Instance.OnLevelLose += DeScaleHintParent;
        }       
        private void DeScaleHintParent()
        {
            HintParentTransform.DOScale(0f, 0.5f);
        }
        private void ScaleHintParent()
        {
            HintParentTransform.DOScale(1f, 1f);
        }
        private void OnHintClick()
        {
            
        }      
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelStart -= ScaleHintParent;
            GameManager.Instance.OnLevelWin -= DeScaleHintParent;
            GameManager.Instance.OnLevelLose -= DeScaleHintParent;
        }
    }
}
