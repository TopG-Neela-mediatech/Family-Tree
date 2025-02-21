using IndianFontCorrector.ConvertLanguage;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{
    public Language Language;
    public static LanguageController instance;
    public bool ChangeLanguage = false;

    public GameObject[] allBtn;
    public Sprite selectedbtnSprites;
    public Sprite NonselectedbtnSprites;


    public TMP_FontAsset English;
    public TMP_FontAsset Tamil;
    public TMP_FontAsset Hindi;
    public TMP_FontAsset Telugu;
    //public TMP_FontAsset Kannada;
    public TMP_FontAsset Malayalam;
    public TMP_FontAsset Bengali;
    public TMP_FontAsset Gujrathi;
    public TMP_FontAsset Marathi;
    public TMP_FontAsset Punjabi;



    public Text _hindiNormalText;

    // public LanguageTranslator[] languageChanger;


    public List<TMP_Text> languageChanger;


    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {


        ChangeLanguage = true;

        //    btnOpenNew();


        //My fucn old
        //ChangeLanguagesMy();
        if (PlayerPrefs.HasKey("PlaySchoolLanguage"))
        {

            //    StartCoroutine(DelayInWork());

            Language = (Language)System.Enum.Parse(typeof(Language), PlayerPrefs.GetString("PlaySchoolLanguage"), true);
            // ChangeLanguagesMyScript();
            btnOpenNew(PlayerPrefs.GetString("PlaySchoolLanguage"));
        }
        else
        {

            SetCurrntLanguage("EnglishUS");
        }

    }



    public void SetCurrntLanguage(string currntLanguage)
    {
        Debug.Log(currntLanguage + " sdkj ");
        Language = (Language)System.Enum.Parse(typeof(Language), currntLanguage, true);


        PlayerPrefs.SetString("PlaySchoolLanguage", currntLanguage);

        if (selectedbtnSprites != null)
        {

            btnOpenNew(currntLanguage);
        }
    }



    void MeUpdateMeScipt()
    {
        foreach (var langScr in languageChanger)
        {
            string val = "";
            if (langScr != null)
            {
                // langScr.ChangeLanguage(Language);
                //  ConvertLang.SetLanguage((Language)System.Enum.Parse(typeof(Language), leanLocalization.currentLanguage.ToString(), true));
                //  val = langScr.currentText.text;
                ConvertLang.SetLanguage(Language);

                val = ConvertLang.Convert(langScr.text);

                // langScr.currentText.text = val;
            }

        }

    }


    // public IEnumerator DelayInWork()
    // {
    //     yield return null;

    //     Language = (Language)System.Enum.Parse(typeof(Language), PlayerPrefs.GetString("PlaySchoolLanguage"), true);
    //     ChangeLanguagesMyScript();
    //     btnOpenNew(PlayerPrefs.GetString("PlaySchoolLanguage"));

    // }

    public void btnOpenNew(string langString)
    {

        //     Debug.Log("dsalkndjkasndsa click btn");
        for (int i = 0; i < allBtn.Length; i++)
        {
            allBtn[i].gameObject.transform.GetChild(0).TryGetComponent(out TMP_Text tmptxt);
            allBtn[i].gameObject.transform.GetChild(0).TryGetComponent(out Text txt);

            if (allBtn[i].gameObject.name == langString)
            {
                //  Language = (Language)System.Enum.Parse(typeof(Language), leanLocalization.currentLanguage.ToString(), true);
                allBtn[i].gameObject.GetComponent<Image>().sprite = selectedbtnSprites;

                // allBtn[i].gameObject.GetComponentInChildren<TMP_Text>().color = Color.white;
                if (tmptxt != null)
                {
                    tmptxt.color = Color.white;
                }
                if (txt != null)
                {
                    txt.color = Color.white;
                }
                continue;
            }
            else
            {
                allBtn[i].gameObject.GetComponent<Image>().sprite = NonselectedbtnSprites;
                //allBtn[i].gameObject.GetComponentInChildren<Text>().color = Color.gray;

                if (tmptxt != null)
                {
                    tmptxt.color = Color.gray;
                }
                if (txt != null)
                {
                    txt.color = Color.gray;
                }

            }
        }
    }
}
