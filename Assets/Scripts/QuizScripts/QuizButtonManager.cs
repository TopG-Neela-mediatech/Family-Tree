using IndianFontCorrector.ConvertLanguage;
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


        public Options value { get; private set; }


        public void EnableCorrectImage()=>correctImage.enabled = true;
        public void EnableIncorrectImage()=>incorrectImage.enabled = true;


        public void SetData(Options val, string optionText,TMP_FontAsset fontAsset)
        {
            value = val;
            this.optionText.font = fontAsset;
            string temp = ConvertLang.Convert(optionText);
            this.optionText.text =temp;
            DisableColorTint();
        }
        private void DisableColorTint()
        {
            correctImage.enabled = false;
            incorrectImage.enabled = false;
        }
    }
}
