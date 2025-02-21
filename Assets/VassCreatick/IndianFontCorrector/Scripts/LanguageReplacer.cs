using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using IndianFontCorrector.ConvertLanguage;

public class LanguageReplacer : MonoBehaviour
{
    private TMP_Text _Text;
    private TMP_InputField _InputField;
    private TMP_Dropdown _DropDown;
    private Language _Language;

    private TMP_FontAsset English;
    private TMP_FontAsset Tamil;
    private TMP_FontAsset Hindi;
    private TMP_FontAsset Telugu;
    private TMP_FontAsset Kannada;
    private TMP_FontAsset Malayalam;
    private TMP_FontAsset Bengali;
    private TMP_FontAsset Gujrathi;
    private TMP_FontAsset Marathi;

    public string Convertedvalue = "";
    public string OriginalText = "";
   
   
   
    void Start()
    {
         English = (TMP_FontAsset)Resources.Load("TamilFont/NotoSansTamil-Regular SDF");
         Tamil = (TMP_FontAsset)Resources.Load("TamilFont/NotoSansTamil-Regular SDF");
         Hindi = (TMP_FontAsset)Resources.Load("HindiFonts/NotoSansDevanagari-Regular SDF");
         Telugu = (TMP_FontAsset)Resources.Load("TeluguFonts/NotoSansTelugu-Regular SDF");
         Kannada = (TMP_FontAsset)Resources.Load("KannadaFont/NotoSans Kannada Regular SDF");
         Malayalam = (TMP_FontAsset)Resources.Load("MalayalamFont/NotoSansMalayalam-Regular SDF");
         Bengali = (TMP_FontAsset)Resources.Load("BengaliFonts/NotoSansBengali-Regular SDF");
         Gujrathi = (TMP_FontAsset)Resources.Load("GujrathiFonts/NotoSansGujarati-Regular SDF");
         Marathi = (TMP_FontAsset)Resources.Load("HindiFonts/NotoSansDevanagari-Regular SDF");
         
        _Text = GetComponent<TMP_Text>();
        _InputField = GetComponent<TMP_InputField>();
        _DropDown = GetComponent<TMP_Dropdown>();
       
    }
    void OnEnable()
    {
        if (_Text == null) _Text = GetComponent<TMP_Text>();
        if (_InputField == null) _InputField = GetComponent<TMP_InputField>();
        if (_DropDown == null) _DropDown = GetComponent<TMP_Dropdown>();

    }

   
    //void Update()
    //{
    //   if(LanguageController.instance.ChangeLanguage)
    //    {
    //        if (LanguageController.instance.Language != _Language)
    //        {
    //            LanguageController.instance.ChangeLanguage = false;
    //            ChangeLanguage(LanguageController.instance.Language);
    //        }
    //    }
   
    //}

   
    void ChangeLanguage(Language language)
    {

        if(language == Language.English)
        {
            if (_Text != null && English != null)
            {
                _Language = Language.English;
                _Text.font = English;
            }

            if (_InputField != null && English != null)
            {
                _InputField.fontAsset = English;
            }

            if (_DropDown != null && English != null)
            {
                _DropDown.captionText.font = English;
                _DropDown.itemText.font = English;
            }

        }
        if(language==Language.Tamil)
        {
            _Language = Language.Tamil;
            if (_Text != null)
            {
                _Text.font = Tamil;
            }

            if (_InputField != null && Tamil != null)
            {
                _InputField.fontAsset = Tamil;
            }

            if (_DropDown != null && Tamil != null)
            {
                _DropDown.captionText.font = Tamil;
                _DropDown.itemText.font = Tamil;
            }

        }
        else if(language == Language.Hindi)
        {
            _Language = Language.Hindi;
            if (_Text != null)
            {
                _Text.font = Hindi;
            }


            if (_InputField != null && Hindi != null)
            {
                _InputField.fontAsset = Hindi;
               
            }

            if (_DropDown != null && Hindi != null)
            {
                _DropDown.captionText.font = Hindi;
                _DropDown.itemText.font = Hindi;
            }

        }
        else if (language == Language.Telugu)
        {
            _Language = Language.Telugu;
            if (_Text != null)
            {
                _Text.font = Telugu;
            }


            if (_InputField != null && Telugu != null)
            {
                _InputField.fontAsset = Telugu;

            }
            if (_DropDown != null && Telugu != null)
            {
                _DropDown.captionText.font = Telugu;
                _DropDown.itemText.font = Telugu;
            }

        }

        else if (language == Language.Kannada)
        {
            _Language = Language.Kannada;
            if (_Text != null)
            {
                _Text.font = Kannada;
            }


            if (_InputField != null && Kannada != null)
            {
                _InputField.fontAsset = Kannada;

            }
            if (_DropDown != null && Kannada != null)
            {
                _DropDown.captionText.font = Kannada;
                _DropDown.itemText.font = Kannada;
            }

        }
        else if (language == Language.Malayalam)
        {
            _Language = Language.Malayalam;
            if (_Text != null)
            {
                _Text.font = Malayalam;
            }


            if (_InputField != null && Malayalam != null)
            {
                _InputField.fontAsset = Malayalam;

            }
            if (_DropDown != null && Malayalam != null)
            {
                _DropDown.captionText.font = Malayalam;
                _DropDown.itemText.font = Malayalam;
            }

        }
        else if (language == Language.Bengali)
        {
            _Language = Language.Bengali;
            if (_Text != null)
            {
                _Text.font = Bengali;
            }


            if (_InputField != null && Bengali != null)
            {
                _InputField.fontAsset = Bengali;

            }
            if (_DropDown != null && Bengali != null)
            {
                _DropDown.captionText.font = Bengali;
                _DropDown.itemText.font = Bengali;
            }

        }
        else if (language == Language.Gujrathi)
        {
            _Language = Language.Gujrathi;
            if (_Text != null)
            {
                _Text.font = Gujrathi;
            }


            if (_InputField != null && Gujrathi != null)
            {
                _InputField.fontAsset = Gujrathi;

            }
            if (_DropDown != null && Gujrathi != null)
            {
                _DropDown.captionText.font = Gujrathi;
                _DropDown.itemText.font = Gujrathi;
            }

        }
        else if (language == Language.Marathi)
        {
            _Language = Language.Marathi;
            if (_Text != null)
            {
                _Text.font = Marathi;
            }


            if (_InputField != null && Marathi != null)
            {
                _InputField.fontAsset = Marathi;

            }
            if (_DropDown != null && Marathi != null)
            {
                _DropDown.captionText.font = Marathi;
                _DropDown.itemText.font = Marathi;
            }

        }


        if (language != Language.English)
        {
            UpdateMe();
        }
    }

    /// <summary>
    /// Use this function to specifically call TextMeshPro InputField and replace the broken characters either in OnValueChanged or in OnEndEdit
    /// </summary>
    public void UpdateInputfield()
    {
        if (LanguageController.instance.Language == Language.Tamil)
        {
           
            if (_InputField != null && Tamil != null)
            {
               _InputField.fontAsset = Tamil;
                UpdateMe();
            }
        }

        if (LanguageController.instance.Language == Language.English)
        {
            if (_InputField != null && English != null)
            {
                _InputField.fontAsset = English;
            }
        }

        if (LanguageController.instance.Language == Language.Hindi)
        {
           
            if (_InputField != null && Hindi != null)
            {
                _InputField.fontAsset = Hindi;
                 UpdateMe();
            }
        }

        if (LanguageController.instance.Language == Language.Telugu)
        {

            if (_InputField != null && Telugu != null)
            {
                _InputField.fontAsset = Telugu;
                UpdateMe();
            }
        }

        if (LanguageController.instance.Language == Language.Kannada)
        {

            if (_InputField != null && Kannada != null)
            {
                _InputField.fontAsset = Kannada;
                UpdateMe();
            }
        }

        if (LanguageController.instance.Language == Language.Malayalam)
        {

            if (_InputField != null && Malayalam != null)
            {
                _InputField.fontAsset = Malayalam;
                UpdateMe();
            }
        }

        if (LanguageController.instance.Language == Language.Bengali)
        {

            if (_InputField != null && Bengali != null)
            {
                _InputField.fontAsset = Bengali;
                UpdateMe();
            }
        }

        if (LanguageController.instance.Language == Language.Gujrathi)
        {

            if (_InputField != null && Gujrathi != null)
            {
                _InputField.fontAsset = Gujrathi;
                UpdateMe();
            }
        }

        if (LanguageController.instance.Language == Language.Marathi)
        {

            if (_InputField != null && Marathi != null)
            {
                _InputField.fontAsset = Marathi;
                UpdateMe();
            }
        }

    }

    void UpdateMe()
    {
        string Value = "";

        if (_Text != null)
        {
            Value = _Text.text;
            OriginalText = _Text.text;
         
        }
        else if (_InputField != null)
        {
            Value = _InputField.text;
        }
        else if (_DropDown != null)
        {
            Value = _DropDown.captionText.text;

        }

            ConvertLang.SetLanguage(_Language);
            Value = ConvertLang.Convert(Value);
       
       
        if (_Text != null)
        {
            _Text.text = Value;
            Convertedvalue = Value;
        }
        if (_InputField != null)
        {
            _InputField.text = Value;
            Convertedvalue = Value;
        }

        if (_DropDown != null)
        {
            _DropDown.captionText.text = Value;
            Convertedvalue = Value;
        }
    }
}



