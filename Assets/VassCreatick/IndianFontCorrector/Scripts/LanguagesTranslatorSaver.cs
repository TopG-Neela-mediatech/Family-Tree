using IndianFontCorrector.ConvertLanguage;
using Sirenix.OdinInspector;

using System.Collections.Generic;
using TMPro;
// using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class LanguagesTranslatorSaver : MonoBehaviour
{


    public TMP_FontAsset English;
    public TMP_FontAsset Tamil;
    public TMP_FontAsset Hindi;
    public TMP_FontAsset Telugu;
    public TMP_FontAsset Malayalam;
    public TMP_FontAsset Bengali;
    public TMP_FontAsset Gujrathi;
    public TMP_FontAsset Marathi;
    public TMP_FontAsset Punjabi;


    public TMP_FontAsset currntFont;
    [ShowInInspector]
    public Dictionary<string, string[]> montlyTranslationSave = new Dictionary<string, string[]>();
    [ShowInInspector]
    public Dictionary<string, string[]> categoryTranslation = new Dictionary<string, string[]>();
    [ShowInInspector]
    public Dictionary<string, string[]> InitialiseOverAllReposts = new Dictionary<string, string[]>();

    public List<TMP_Text> runtimeMonthTranslator = new List<TMP_Text>();
    public List<TMP_Text> runtimeCategoryTranslator = new List<TMP_Text>();


    public List<TMP_Text> runtimeCategoryMedalTranslator = new List<TMP_Text>();
   
    string selectedLanguage;

    public TMP_Dropdown dropdown;
    public LanguageController languageController;
    public int dbindex;


    // public AdvancedDropdown[] advancedDropdown = new AdvancedDropdown[4];


    [Header("Overall Stats")]
    public TMP_Text[] OveralStats = new TMP_Text[5];

    public string ConvertSecondsToMinutesHours(float totalSeconds)
    {
        if (totalSeconds <= 0) return "0";

        int hours = Mathf.FloorToInt(totalSeconds / 3600);
        int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);

        string timeString = "";
        if (hours > 0) timeString += $"{hours} hrs" ;   // {GetReportTranslationByIndex("hrs")} ";
        if (minutes > 0) timeString += $"{minutes} mins" ;// {GetReportTranslationByIndex("mins")}";
        if (totalSeconds < 60) timeString = $"{totalSeconds.ToString()} sec";


        return string.IsNullOrEmpty(timeString) ? "0" : timeString.Trim();
    }




    public void UpdateStatistics(int gamePlayed , int attendancePercent , int medalsEarned ,float totalTimeSpent , int starsEarned)
    {


        OveralStats[0].text = gamePlayed.ToString();
        OveralStats[1].text = attendancePercent+ "%";
        OveralStats[2].text =  medalsEarned.ToString() + " " +  "Medals" ; //GetReportTranslationByIndex("Medals");
        string time = ConvertSecondsToMinutesHours(totalTimeSpent);
        OveralStats[3].text = time;
        OveralStats[4].text = starsEarned.ToString() + " " +  "Stars"; //GetReportTranslationByIndex("Stars");

        foreach(TMP_Text OveralStatTexts in OveralStats) 
        {
            SetFontForLanguage(OveralStatTexts);
        }

    }

    void ChangeTheDropDownTextAndFont() 
    {
        // foreach (var dropdown in advancedDropdown) 
        // {
        //     // dropdown.LetsMakeSomeDebug(currntFont);
        // }
    }
    void InitializeMonthTranslations()
    {
        // English , English Us ,Hindi , Marathi , Gujrathi  , tamil , Telugu , Punjabi , Bengali
        montlyTranslationSave = new Dictionary<string, string[]>();

        montlyTranslationSave.Add("January", new string[] { "January","January", "जनवरी", "जानेवारी", "જાન્યુઆરી", "ஜனவரி", "జనవరి", "ਜਨਵਰੀ", "জানুয়ারি" });
        montlyTranslationSave.Add("February", new string[] { "February","February",  "फरवरी", "फेब्रुवारी", "ફેબ્રુઆરી",  "பிப்ரவரி", "ఫిబ్రవరి", "ਫ਼ਰਵਰੀ", "ফেব্রুয়ারি" });
        montlyTranslationSave.Add("March", new string[] { "March","March", "मार्च", "मार्च", "માર્ચ", "மார்ச்", "మార్చి", "ਮਾਰਚ", "মার্চ" });
        montlyTranslationSave.Add("April", new string[] { "April","April", "अप्रैल", "एप्रिल", "એપ્રિલ", "ஏப்ரல்", "ఏప్రిల్", "ਅਪ੍ਰੈਲ", "এপ্রিল" });
        montlyTranslationSave.Add("May", new string[] { "May", "May","मई", "मे", "મે",  "மே", "మే", "ਮਈ", "মে" });
        montlyTranslationSave.Add("June", new string[] { "June", "June","जून", "जून", "જૂન", "ஜூன்", "జూన్", "ਜੂਨ", "জুন" });
        montlyTranslationSave.Add("July", new string[] { "July","July", "जुलाई", "जुलै", "જુલાઈ", "ஜூலை", "జూలై", "ਜੁਲਾਈ", "জুলাই" });
        montlyTranslationSave.Add("August", new string[] { "August","August", "अगस्त", "ऑगस्ट", "ઓગસ્ટ", "ஆகஸ்ட்", "ఆగస్టు", "ਅਗਸਤ", "অগাস্ট" });
        montlyTranslationSave.Add("September", new string[] { "September","September", "सितंबर", "सप्टेंबर", "સપ્ટેમ્બર", "செப்டம்பர்", "సెప్టెంబర్", "ਸਤੰਬਰ", "সেপ্টেম্বর" });
        montlyTranslationSave.Add("October", new string[] { "October","October",  "अक्टूबर", "ऑक्टोबर", "ઓક્ટોબર","அக்டோபர்", "అక్టోబర్", "ਅਕਤੂਬਰ", "অক্টোবর" });
        montlyTranslationSave.Add("November", new string[] { "November","November", "नवंबर", "नोव्हेंबर", "નવેમ્બર", "நவம்பர்", "నవంబర్", "ਨਵੰਬਰ", "নভেম্বর" });
        montlyTranslationSave.Add("December", new string[] { "December","December", "दिसंबर", "डिसेंबर", "ડિસેમ્બર", "டிசம்பர்", "డిసెంబర్", "ਦਸੰਬਰ", "ডিসেম্বর" });

        InitialiseOverAllReposts = new Dictionary<string, string[]>(); 



        InitialiseOverAllReposts.Add("Stars", new string[] { "Stars", "Stars","सितारे", "तारे", "સ્ટાર્સ",  "நட்சத்திரங்கள்", "నక్షత్రాలు", "ਤਾਰੇ", "টি তারা" });
        InitialiseOverAllReposts.Add("hrs", new string[] { "hrs","hrs", "घंटे", "तास", "કલાક",  "மணி", "గంటలు", "ਘੰਟੇ", "ঘন্টা" });
        InitialiseOverAllReposts.Add("mins", new string[] { "mins","mins", "मिनट", "मि", "મિનિટ", "நிமிடம்", "నిమిషాలు", "ਮਿੰਟ", "মিনিট" });
        InitialiseOverAllReposts.Add("Medals", new string[] { "Medals","Medals", "पदके", "पदक", "મેડલ", "பதக்கங்கள்", "పతకాలు", "ਮੈਡਲ", "টি পদক" });
       

    }


    void InitializeCategoryTranslations()
    {
        categoryTranslation = new Dictionary<string, string[]>();
        // English ,English US , Hindi , Marathi , Gujrathi  , Tamil , Telugu , punjabi , Bengali
        categoryTranslation.Add("PeekABoo", new string[] { "PeekABoo","PeekABoo",  "पीकाबू", "पीक-ए-बू", "પીકાબૂ",  "பீகாபூ", "పీకాబూ", "ਪੀਕਾਬੂ", "পিকাবু" });
        categoryTranslation.Add("Coloring", new string[] { "Coloring","Coloring", "रंगाई", "रंगकाम", "રંગવા",  "வர்ணம்", "వర్ణం", "ਰੰਗਾਈ", "রঙ করা" });
        categoryTranslation.Add("CopyThePattern", new string[] { "Copy The Pattern","Copy The Pattern", "पैटर्न कॉपी करें", "नमुना कॉपी करा", "પેટર્નની નકલ" , "மாதிரியை நகலெடு", "నమూనా కాపీ", "ਪੈਟਰਨ ਕਾਪੀ", "প্যাটার্ন অনুলিপি করুন" });
        categoryTranslation.Add("FindHiddenObject", new string[] { "Find Hidden Object","Find Hidden Object", "छुपी वस्तु ढूंढें", "लपलेली वस्तू शोधा", "છુપાયેલ વસ્તુ શોધો", "மறைந்த பொருளைக் கண்டுபிடி", "దాగిన వస్తువు కనుగొనండి", "ਛੁਪਾ ਚੀਜ਼ ਲੱਭੋ", "লুকানো বস্তুটি খুঁজে বের করুন" });
        categoryTranslation.Add("JigsawPuzzle", new string[] { "Jigsaw Puzzle", "Jigsaw Puzzle",  "जिगसॉ पजल", "जिगसॉ कोडे", "જિગસૉ પઝલ",  "ஜிக்சா புதிர்", "జిగ్సా పజల్", "ਜਿਗਸਾ ਪਜ਼ਲ", "জিগসো ধাঁধা" });
        categoryTranslation.Add("Tracing", new string[] { "Tracing", "Tracing", "अनुकरण", "अनुकरण", "અનુકરણ", "டிரேசிங்", "ట్రేసింగ్", "ਟ੍ਰੇਸਿੰਗ", "ট্রেসিং" });
        categoryTranslation.Add("FindTheLetter", new string[] { "Find The Letter","Find The Letter", "अक्षर ढूंढें", "अक्षर शोधा", "અક્ષર શોધો",  "அகரம் கண்டுபிடி", "అక్షరం కనుగొనండి", "ਅੱਖਰ ਲੱਭੋ", "অক্ষরটি খুঁজুন" });
        categoryTranslation.Add("MatchTheArrow", new string[] { "Match The Arrow","Match The Arrow", "तीर मिलाएं", "बाण जुळवा", "તીર મલાવો",  "அம்பைக் பொருத்து", "బాణం సరిపోల్చు", "ਤੀਰ ਮਿਲਾਓ", "তীরের মিল করুন" });
        categoryTranslation.Add("MatchTheShadow", new string[] { "Match The Shadow", "Match The Shadow", "छाया मिलाएं", "सावली जुळवा", "છાયા મેળવો",  "நிழலுடன் பொருத்து", "నీడను సరిపోల్చు", "ਛਾਇਆ ਨਾਲ ਮਿਲਾਓ", "ছায়ার মিল করুন" });
        categoryTranslation.Add("SpotDifference", new string[] { "Spot The Difference", "Spot The Difference", "अंतर ढूंढें", "फरक शोधा", "ફરક શોધો",  "வித்தியாசத்தை கண்டுபிடி", "తేడాను గుర్తించు", "ਫ਼ਰਕ ਲੱਭੋ", "অন্তরটি খুঁজুন" });
        categoryTranslation.Add("MatchTheColour", new string[] { "Match The Colour","Match The Color", "रंग मिलाएं", "रंग जुळवा", "રંગ મેળવો",  "நிறத்தை பொருத்து", "రంగు సరిపోల్చు", "ਰੰਗ ਮਿਲਾਓ", "রঙের মিল করুন" });
        categoryTranslation.Add("FlashCards", new string[] { "Flash Cards","Flash Cards", "फ्लैश कार्ड्स", "फ्लॅश कार्ड्स", "ફ્લેશ કાર્ડ્સ",  "ஃப்ளாஷ் கார்ட்ஸ்", "ఫ్లాష్ కార్డులు", "ਫਲੈਸ਼ ਕਾਰਡ", "ফ্ল্যাশ কার্ডস" });
        categoryTranslation.Add("BalloonPop", new string[] { "Balloon Pop","Balloon Pop", "गुब्बारा फोड़ो", "फुगा फोडा", "બલૂન ફાટવું",  "பூவைக்குத் தட்ட", "గుబ్బారాన్ని పగలగొట్టు", "ਗੁਬਾਰਾ ਫੂਟੋ", "বেলুন ফাটাও" });
        categoryTranslation.Add("Counting", new string[] { "Counting","Counting", "गिनती", "मोजणी", "ગણતરી",  "எண்ணிக்கை", "లెక్కింపు", "ਗਿਣਤੀ", "গণনা" });
        categoryTranslation.Add("ConnectTheDot", new string[] { "Connect The Dots","Connect The Dots", "बिंदु मिलाओ", "बिंदू जोडा", "બિંદુ જોડો", "बिंदु मिलाओ", "புள்ளிகளை இணைக்க", "బిందువులను కలపండి", "ਬਿੰਦੂ ਮਿਲਾਓ", "ডটস সংযুক্ত করুন" });
        categoryTranslation.Add("WorldPersonalities", new string[] { "World Personalities", "World Personalities", "विश्व हस्तियाँ", "जगप्रसिद्ध व्यक्तिमत्त्वे", "વિશ્વ પ્રસિદ્ધ વ્યક્તિત્વો", "உலக பிரபலங்கள்", "ప్రపంచ ప్రసిద్ధ వ్యక్తులు", "ਵਿਸ਼ਵ ਪ੍ਰਸਿੱਧ ਸ਼ਖ਼ਸੀਅਤਾਂ", "বিশ্বের ব্যক্তিত্ব" });
        categoryTranslation.Add("Sorting", new string[] { "Sorting", "Sorting", "वर्गीकरण", "वर्गीकरण", "શ્રેણીકરણ",  "வகைப்படுத்தல்", "వర్గీకరణ", "ਵਰਗੀਕਰਨ", "বাছাই" });
        categoryTranslation.Add("WordGame", new string[] { "Word Game", "Word Game", "शब्द खेल", "शब्द खेळ", "શબ્દ રમતો", "சொல்லாட்டம்", "పద ఆట", "ਸ਼ਬਦ ਖੇਡ", "শব্দ খেলা" });
        categoryTranslation.Add("FruitSorting", new string[] { "Fruit Sorting","Fruit Sorting", "फलों का वर्गीकरण", "फळांचे वर्गीकरण", "ફળોનું શ્રેણીકરણ",  "பழ வகைப்படுத்தல்", "పండ్ల వర్గీకరణ", "ਫਲ ਵਰਗੀਕਰਨ", "ফল বাছাই" });
        categoryTranslation.Add("IceCreamMaker", new string[] { "Ice Cream Maker", "Ice Cream Maker", "आइस क्रीम मेकर", "आईसक्रीम मेकर", "આઇસ્ક્રીમ મેકર", "ஐஸ்கிரீம் தயாரிப்பாளர்", "ఐస్ క్రీమ్ మేకర్", "ਆਇਸ ਕ੍ਰੀਮ ਮੈਕਰ", "আইসক্রিম মেকার" });
        categoryTranslation.Add("MusicalInstruments", new string[] { "Musical Instruments","Musical Instruments", "संगीत वाद्य", "संगीत वाद्ये", "સંગીત સાધનો", "இசைக் கருவிகள்", "సంగీత వాయిద్యాలు", "ਸੰਗੀਤਕ ਸਾਜ਼", "সঙ্গীত বাদ্যযন্ত্র" });
        categoryTranslation.Add("ToySorting", new string[] { "Toy Sorting","Toy Sorting", "खिलौनों का वर्गीकरण", "खेळणी वर्गीकरण", "ખિલોનાની શ્રેણીકરણ", "ஆடம்பர வகைப்படுத்தல்", "బొమ్మల వర్గీకరణ", "ਖਿਲੌਨਿਆਂ ਦਾ ਵਰਗੀਕਰਨ", "খেলনার বাছাই" });
        categoryTranslation.Add("WordSearch", new string[] { "WordSearch","WordSearch", "खिलौनों का वर्गीकरण", "खेळणी वर्गीकरण", "ખિલોનાની શ્રેણીકરણ", "ஆடம்பர வகைப்படுத்தல்", "బొమ్మల వర్గీకరణ", "ਖਿਲੌਨਿਆਂ ਦਾ ਵਰਗੀਕਰਨ", "খেলনার বাছাই" });
    }

      [Button]
    public void GetFallFont(){
  English = (TMP_FontAsset)Resources.Load("TamilFont/NotoSansTamil-Regular SDF");
        Tamil = (TMP_FontAsset)Resources.Load("TamilFont/NotoSansTamil-Regular SDF");
        Hindi = (TMP_FontAsset)Resources.Load("HindiFonts/NotoSansDevanagari-Regular SDF");
        Telugu = (TMP_FontAsset)Resources.Load("TeluguFonts/NotoSansTelugu-Regular SDF");
       //] Kannada = (TMP_FontAsset)Resources.Load("KannadaFont/NotoSans Kannada Regular SDF");
        Malayalam = (TMP_FontAsset)Resources.Load("MalayalamFont/NotoSansMalayalam-Regular SDF");
        Bengali = (TMP_FontAsset)Resources.Load("BengaliFonts/NotoSansBengali-Regular SDF");
        Gujrathi = (TMP_FontAsset)Resources.Load("GujrathiFonts/NotoSansGujarati-Regular SDF");
        Marathi = (TMP_FontAsset)Resources.Load("HindiFonts/NotoSansDevanagari-Regular SDF");
        Punjabi = (TMP_FontAsset)Resources.Load("PunjabiFont/NotoSansGurmukhi-Regular SDF");

    }
   
    private void Start()
    {
     
     dropdown.value = 1; // Skip option 0

        InitializeMonthTranslations();
        InitializeCategoryTranslations();
        selectedLanguage = PlayerPrefs.GetString("PlaySchoolLanguage");
        dbindex = dropdown.options.FindIndex(option => option.text == selectedLanguage);

        dropdown.value = dbindex;

        selectedLanguage = PlayerPrefs.GetString("PlaySchoolLanguage");



        StartCoroutine(DelayinGetFont());
    }

    public System.Collections.IEnumerator DelayinGetFont()
    {
        yield return new WaitForSeconds(0.2f);
        ChangeTheDropDownTextAndFont();
    }
    public void OnDropDownValueChanged()
    {
        // Skip option 0
        int value = dropdown.value;

        if(value == 0){
            value = 1; 
               dropdown.value = value;
        }
        
        string optionText = dropdown.options[value].text;
        dbindex = dropdown.value;
        languageController.SetCurrntLanguage(optionText);

        AgainChangeTheLanguage();
        ChangeTheDropDownTextAndFont();
    }

    public string GetReportTranslationByIndex(string rpname )
    {
        if (InitialiseOverAllReposts.ContainsKey(rpname) && dbindex >= 0 && dbindex < InitialiseOverAllReposts[rpname].Length)
        {
            return InitialiseOverAllReposts[rpname][dbindex];
        }
        return rpname;

    }
    string GetMonthTranslationByIndex(string month, int index)
    {
        if (montlyTranslationSave.ContainsKey(month) && index >= 0 && index < montlyTranslationSave[month].Length)
        {
            return montlyTranslationSave[month][index];
        }
        return month;
    }
    string GetCategoryByIndex(string month, int index)
    {
        if (categoryTranslation.ContainsKey(month) && index >= 0 && index < categoryTranslation[month].Length)
        {
            return categoryTranslation[month][index];
        }
        return month;
    }
    //FirstTime 
    public  void SetMonthTranslation(TMP_Text monthTextComponent, string monthKey)
    {
        //this is i want to do in option in Drop Down TODO
     
        string translatedMonth = GetMonthTranslationByIndex(monthKey.Split()[0], dbindex);
        runtimeMonthTranslator.Add(monthTextComponent);

        SetFontForLanguage(monthTextComponent);
        monthTextComponent.text = ConverterLang(translatedMonth) + " " + monthKey.Split()[1];


    }
    public void SetCatgegoryGameTranslation(TMP_Text CategoryGameTextComponent, string categoryName)
    {
        //this is i want to do in option in Drop Down TODO
        string translatedCategory = GetCategoryByIndex(categoryName, dbindex);
        if (CategoryGameTextComponent != null)
        {
            runtimeCategoryTranslator.Add(CategoryGameTextComponent);
        }

        SetFontForLanguage(CategoryGameTextComponent);
        CategoryGameTextComponent.text = ConverterLang(translatedCategory);
    }
    string ConverterLang(string _texts)
    {

        string val = "";
        val = ConvertLang.Convert(_texts);
        return val;
    }
    void AgainChangeTheLanguage()
    {
        foreach (TMP_Text runtimeTextStored in runtimeMonthTranslator)
        {
            SetFontForLanguage(runtimeTextStored);
            string translatedMonth = GetMonthTranslationByIndex(runtimeTextStored.gameObject.name.Split()[0], dbindex);
            runtimeTextStored.text = ConverterLang(translatedMonth);
        }

        foreach (TMP_Text runtimeTextStoredCatgeory in runtimeCategoryTranslator)
        {
           SetFontForLanguage(runtimeTextStoredCatgeory);
           // runtimeTextStoredCatgeory.font = Gujrathi;
            string translatedCategory = GetCategoryByIndex(runtimeTextStoredCatgeory.gameObject.name, dbindex);
            runtimeTextStoredCatgeory.text = ConverterLang(translatedCategory);
        }
    }

    public void ShowReportWithLocalization(GameObject reportGG) 
    {
        if (reportGG != null)
        {
       
            // reportGG.GetComponent<ReportLangTranslator>().UpdateFontWithText(currntFont);

        }
    
    }

    private void SetFontForLanguage(TMP_Text textComponent)
    {
        string selectedLanguage = PlayerPrefs.GetString("PlaySchoolLanguage");

        TMP_FontAsset newFont = selectedLanguage switch
        {
            "English" => English,
            "EnglishUS" => English,
            "Bhojpuri" => Hindi,
            "Tamil" => Tamil,
            "Hindi" => Hindi,
            "Telugu" => Telugu,
            "Malayalam" => Malayalam,
            "Bengali" => Bengali,
            "Gujrathi" => Gujrathi,
            "Marathi" => Marathi,
            "Punjabi" => Punjabi,
            _ => textComponent.font
        };

        textComponent.font = newFont;
        currntFont = newFont;
    }


}
