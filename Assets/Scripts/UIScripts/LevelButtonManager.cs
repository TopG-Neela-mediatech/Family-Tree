using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.FamilyTree
{
    public class LevelButtonManager : MonoBehaviour
    {
        [SerializeField] private Image levelImage;
        [SerializeField] private Button LevelLoadButton;
        [SerializeField] private Sprite incompleteSprite;
        [SerializeField] private Sprite completeSprite;
    }
}
