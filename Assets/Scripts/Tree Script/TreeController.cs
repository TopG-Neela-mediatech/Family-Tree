using System;
using TMPro;
using UnityEngine;
using IndianFontCorrector.ConvertLanguage;


namespace TMKOC.FamilyTree
{
    public class TreeController : MonoBehaviour
    {
        [SerializeField] private DropController[] dropControllers;
        [SerializeField] private Vector3 scale_mobile;
        [SerializeField] private Vector3 scale_tablet;
        [SerializeField] private Vector3 position_mobile;
        [SerializeField] private Vector3 position_tablet;
        [SerializeField] private RelationTextLink[] relationLocaltext;
        [SerializeField] private string textLocalization;
        [SerializeField] private MemberLocalNameSO hindiLocalSO;
        [SerializeField] private MemberLocalNameSO tamilLocalSO;
        private MemberLocalNameSO localNames;
        private TMP_FontAsset localFontAsset;


        private void Awake()
        {
            SetPosition(DetectAspectRatio());
            SetLanguage();
        }
        private void SetLanguage()
        {
            textLocalization = PlayerPrefs.GetString("PlaySchoolLanguageAudio", textLocalization);          
            switch (textLocalization)
            {
                case "English":
                    break;
                case "EnglishUS":
                    break;
                case "Hindi":
                    localNames = hindiLocalSO;                    
                    LocalizeText();                   
                    break;
                case "Tamil":
                    localNames = tamilLocalSO;
                    LocalizeText();
                    break;
                default:
                    break;
            }
        }
        private void LocalizeText()
        {
            foreach (var rtmp in relationLocaltext)
            {
                LocalizeNames localText = Array.Find(localNames.localNames, relation => relation.respectiveMember == rtmp.relationShip);
                if (localText != null)
                {
                    rtmp.relationTMP.font = localNames.respectiveFontAsset;
                    string tempSt = localText.localizedName;
                    ConvertLang.Convert(tempSt);
                    rtmp.relationTMP.text = tempSt;
                }
                else
                {
                    Debug.Log("String Not Found");
                }
            }
        }
        private float DetectAspectRatio()
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            return screenAspect;
        }
        public DropController GetDropController(int key)
        {
            DropController dc = Array.Find(dropControllers, i => i.GetValue() == key);
            if (dc == null)
            {
                Debug.Log("Drop Controller Not Found");
            }
            return dc;
        }
        private void SetPosition(float screenAspect)
        {
            if (screenAspect < 1.5f)
            {
                transform.position = position_tablet;
                transform.localScale = scale_tablet;
            }
            else//16:9
            {
                transform.position = position_mobile;
                transform.localScale = scale_mobile;
            }
        }
    }
    [System.Serializable]
    public class RelationTextLink
    {
        public TextMeshProUGUI relationTMP;
        public MemberRelationShip relationShip;
    }
}
