using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndianFontCorrector;

namespace IndianFontCorrector.ConvertLanguage
{
    public static class ConvertLang
    {
        private static Converter mconverLang = null;

        public static bool SetLanguage(Language languageId)
        {
            if (mconverLang != null)
            {

                if (mconverLang.Language == languageId)
                {
                    Debug.LogWarning($"Already initialized with {languageId}");
                    return false;
                }
                else
                {
                    // Debug.Log("Languages  " + languageId);
                }
                mconverLang?.Unload();
            }



            if (languageId == Language.Tamil)
            {
                mconverLang = new TamilConverter();
            }
            if (languageId == Language.Hindi)
            {
                mconverLang = new HindiConverter();
            }
            if (languageId == Language.Telugu)
            {
                mconverLang = new TeluguConverter();
            }
            if (languageId == Language.Kannada)
            {
                mconverLang = new KannadaConverter();
            }
            if (languageId == Language.Malayalam)
            {
                mconverLang = new MalayalamConverter();
            }
            if (languageId == Language.Bengali)
            {
                mconverLang = new BengaliConverter();
            }
            if (languageId == Language.Gujrathi)
            {
                mconverLang = new GujrathiConverter();
            }
            if (languageId == Language.Marathi)
            {
                mconverLang = new HindiConverter();
            }
            // if (languageId == Language.Bhojpuri)
            // {
            //     mconverLang = new HindiConverter();

            // }
            //  else
            //   Debug.LogError($"Implement support for {languageId}");

            mconverLang?.Init();
            return true;
        }

        public static string Convert(string text)
        {
            if (mconverLang == null)
            {
                //  Debug.LogError("Error! SetLanguage must be called before Convertion");
                return text;
            }

            return mconverLang.Convert(text);
        }
    }
}
