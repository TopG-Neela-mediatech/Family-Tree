using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.FamilyTree
{
    public class QuizButtonManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI optionText;
        [SerializeField] private Image correctImage;
        [SerializeField] private Image incorrectImage;


        public int value { get; private set; }


        public void EnableCorrectImage()=>correctImage.enabled = true;
        public void EnableIncorrectImage()=>incorrectImage.enabled = true;


        public void SetData(int val, string optionText)
        {
            value = val;
            this.optionText.text = optionText;
            DisableColorTint();
        }
        private void DisableColorTint()
        {
            correctImage.enabled = false;
            incorrectImage.enabled = false;
        }
    }
}
