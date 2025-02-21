using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DemoLanguage : MonoBehaviour
{
    public TMP_InputField DemoText;
    public TMP_Dropdown Languageselect;
    string Hinditext;
    string Telugutext;
    string Tamiltext;
    string Kannadatext;
    string Malayalamtext;
    string Bengalitext;
    string Gujrathitext;
    string Marathitext;
    void Start()
    {
      
        LoadTamilJson();
        LoadHindiJson();
        LoadTeluguJson();
        LoadKannadaJson();
        LoadMalayalamJson();
        LoadBengaliJson();
        LoadGujrathiJson();
        LoadMarathiJson();
        ChangeLanguage();
    }

    void LoadHindiJson()
    {
    
        TextAsset textAsset = Resources.Load<TextAsset>("JsonTexts/HindiLang");
        if (textAsset != null)
        {
            Hinditext = textAsset.text;
        }
    }

    void LoadTeluguJson()
    {
       
        TextAsset textAsset = Resources.Load<TextAsset>("JsonTexts/TeluguLang");
        if (textAsset != null)
        {
            Telugutext = textAsset.text;
        }
    } 
    
    void LoadKannadaJson()
    {
       
        TextAsset textAsset = Resources.Load<TextAsset>("JsonTexts/KannadaLang");
        if (textAsset != null)
        {
            Kannadatext = textAsset.text;
        }
    }

    void LoadTamilJson()
    {
      
        TextAsset textAsset = Resources.Load<TextAsset>("JsonTexts/TamilLang");
        if (textAsset != null)
        {
            Tamiltext = textAsset.text;
        }
    }
    void LoadMalayalamJson()
    {

        TextAsset textAsset = Resources.Load<TextAsset>("JsonTexts/MalayalamLang");
        if (textAsset != null)
        {
            Malayalamtext = textAsset.text;
         

        }
    }

    void LoadBengaliJson()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("JsonTexts/BengaliLang");
        if (textAsset != null)
        {
            Bengalitext = textAsset.text;

        }
    }
    void LoadGujrathiJson()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("JsonTexts/GujrathiLang");
        if (textAsset != null)
        {
            Gujrathitext = textAsset.text;

        }
    }

    void LoadMarathiJson()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("JsonTexts/MarathiLang");
        if (textAsset != null)
        {
            Marathitext = textAsset.text;

        }
    }

    // This Function will Update  TextMesh Text and Inputfield and Dropdown Label characters , 
    // you need to assign the sentences to your text value and then assign the language ,
    // finally assign changelanguage to true to fix the characters
    public void ChangeLanguage()
    {
        if (Languageselect.captionText.text.ToUpper() == "TAMIL")
        {
            DemoText.text = Tamiltext;
            LanguageController.instance.Language = Language.Tamil;
            LanguageController.instance.ChangeLanguage = true;
        }

        if (Languageselect.captionText.text.ToUpper() == "ENGLISH")
        {
            DemoText.text = "Hi, How are you?";
            LanguageController.instance.Language = Language.English;
            LanguageController.instance.ChangeLanguage = true;
        }

        if (Languageselect.captionText.text.ToUpper() == "HINDI")
        {
            DemoText.text = Hinditext;
            LanguageController.instance.Language = Language.Hindi;
            LanguageController.instance.ChangeLanguage = true;
        }

        if (Languageselect.captionText.text.ToUpper() == "TELUGU")
        {
            DemoText.text = Telugutext;
            LanguageController.instance.Language = Language.Telugu;
            LanguageController.instance.ChangeLanguage = true;
        } 
        
        if (Languageselect.captionText.text.ToUpper() == "KANNADA")
        {
            DemoText.text = Kannadatext;
            LanguageController.instance.Language = Language.Kannada;
            LanguageController.instance.ChangeLanguage = true;
        }
        if (Languageselect.captionText.text.ToUpper() == "MALAYALAM")
        {
            DemoText.text = Malayalamtext;
            LanguageController.instance.Language = Language.Malayalam;
            LanguageController.instance.ChangeLanguage = true;
        }
        if (Languageselect.captionText.text.ToUpper() == "BENGALI")
        {
            DemoText.text = Bengalitext;
            LanguageController.instance.Language = Language.Bengali;
            LanguageController.instance.ChangeLanguage = true;
        }

        if (Languageselect.captionText.text.ToUpper() == "GUJRATHI")
        {
            DemoText.text = Gujrathitext;
            LanguageController.instance.Language = Language.Gujrathi;
            LanguageController.instance.ChangeLanguage = true;
        }
        if (Languageselect.captionText.text.ToUpper() == "MARATHI")
        {
            DemoText.text = Marathitext;
            LanguageController.instance.Language = Language.Marathi;
            LanguageController.instance.ChangeLanguage = true;
        }
    }

    // This function will specifically fix the characters of inputfield, 
    // you can use above function also for both, but if you need to fix inputfield characters then you can call this function 
    //call this function on EndEdit in Inputfield Inspector
    public void ChangeLanguageInput()
    {
        //List<string> languages =new List<string> { "TAMIL", "ENGLISH", "HINDI", "TELUGU", "KANNADA", "MALAYALAM", "BENGALI", "GUJRATHI", "MARATHI" };

        // string DropdownCaption = Languageselect.captionText.text.ToUpper();
        //for(int i=0;i<languages.Count;i++)
        //{
        //    if(languages[i]==DropdownCaption)
        //    {
        //        DemoText.GetComponent<LanguageReplacer>().UpdateInputfield();
        //    }
        //}
        DemoText.GetComponent<LanguageReplacer>().UpdateInputfield();
        //if (Languageselect.captionText.text.ToUpper() == "TAMIL")
        //{
          
        //    Debug.Log("Tamil Languages Selected");
         
           
        //}

        //if (Languageselect.captionText.text.ToUpper() == "ENGLISH")
        //{
           
        //    DemoText.GetComponent<LanguageReplacer>().UpdateInputfield();
        //}

        //if (Languageselect.captionText.text.ToUpper() == "HINDI")
        //{
           
        //    DemoText.GetComponent<LanguageReplacer>().UpdateInputfield();
        //}

        //if (Languageselect.captionText.text.ToUpper() == "TELUGU")
        //{
           
        //    DemoText.GetComponent<LanguageReplacer>().UpdateInputfield();
        //}

        //if (Languageselect.captionText.text.ToUpper() == "KANNADA")
        //{
        //    DemoText.GetComponent<LanguageReplacer>().UpdateInputfield();
        //}
        //if (Languageselect.captionText.text.ToUpper() == "MALAYALAM")
        //{
        //    DemoText.GetComponent<LanguageReplacer>().UpdateInputfield();
        //}
        //if (Languageselect.captionText.text.ToUpper() == "BENGALI")
        //{
        //    DemoText.GetComponent<LanguageReplacer>().UpdateInputfield();
        //}
        //if (Languageselect.captionText.text.ToUpper() == "GUJRATHI")
        //{
        //    DemoText.GetComponent<LanguageReplacer>().UpdateInputfield();
        //}

        //if (Languageselect.captionText.text.ToUpper() == "MARATHI")
        //{
        //    DemoText.GetComponent<LanguageReplacer>().UpdateInputfield();
        //}
    }

}
