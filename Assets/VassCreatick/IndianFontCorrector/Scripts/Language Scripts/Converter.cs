using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;

namespace IndianFontCorrector
{
    public class Converter
    {
        // Start is called before the first frame update
        protected List<CharacterAttributesTamil> mCharacterAttributesTamil = new List<CharacterAttributesTamil>();
        protected List<CharacterAttributesHindi> mCharacterAttributesHindi = new List<CharacterAttributesHindi>();
        protected List<CharacterAttributesTelugu> mCharacterAttributesTelugu = new List<CharacterAttributesTelugu>();
        protected List<CharacterAttributesKannada> mCharacterAttributesKannada = new List<CharacterAttributesKannada>();
        protected List<CharacterAttributesMalayalam> mCharacterAttributesMalayalam = new List<CharacterAttributesMalayalam>();
        protected List<CharacterAttributesBengali> mCharacterAttributesBengali = new List<CharacterAttributesBengali>();
        public Language Language { get; protected set; }
        public static Converter converter = new Converter();

        public virtual bool Init()
        {
            return true;
        }

        public virtual void Unload()
        {
            mCharacterAttributesHindi = null;
            mCharacterAttributesTamil = null;
            mCharacterAttributesTelugu = null;
            mCharacterAttributesKannada = null;
            mCharacterAttributesMalayalam = null;
            mCharacterAttributesBengali = null;
            

        }

        protected virtual bool LoadAttributesHindi(string fileName)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(fileName);
            if (textAsset != null)
            {
                mCharacterAttributesHindi = JsonConvert.DeserializeObject<List<CharacterAttributesHindi>>(textAsset.text);
                if (mCharacterAttributesHindi != null)
                {
                    //Debug.Log($"CharacterAttributes in Hindi: " + mCharacterAttributesHindi.Count);
                    return true;
                }
                else
                {
                    Debug.LogError("Failed to deserialize languague data");
                    return false;
                }
            }
            else
            {
                Debug.LogError($"Failed to load {fileName}");
                return false;
            }
        }

        protected virtual bool LoadAttributesTamil(string fileName)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(fileName);
            if (textAsset != null)
            {
                mCharacterAttributesTamil = JsonConvert.DeserializeObject<List<CharacterAttributesTamil>>(textAsset.text);
                if (mCharacterAttributesTamil != null)
                {
                    // Debug.Log($"CharacterAttributes in Tamil: " + mCharacterAttributesTamil.Count);
                    return true;
                }
                else
                {
                    Debug.LogError("Failed to deserialize languague data");
                    return false;
                }
            }
            else
            {
                Debug.LogError($"Failed to load {fileName}");
                return false;
            }
        }

        protected virtual bool LoadAttributesTelugu(string fileName)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(fileName);
            if (textAsset != null)
            {
                mCharacterAttributesTelugu = JsonConvert.DeserializeObject<List<CharacterAttributesTelugu>>(textAsset.text);
                if (mCharacterAttributesTelugu != null)
                {

                    return true;
                }
                else
                {
                    Debug.LogError("Failed to deserialize languague data");
                    return false;
                }
            }
            else
            {
                Debug.LogError($"Failed to load {fileName}");
                return false;
            }
        }

        protected virtual bool LoadAttributesKannada(string fileName)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(fileName);
            if (textAsset != null)
            {
                mCharacterAttributesKannada = JsonConvert.DeserializeObject<List<CharacterAttributesKannada>>(textAsset.text);
                if (mCharacterAttributesKannada != null)
                {

                    return true;
                }
                else
                {
                    Debug.LogError("Failed to deserialize languague data");
                    return false;
                }
            }
            else
            {
                Debug.LogError($"Failed to load {fileName}");
                return false;
            }
        }
        protected virtual bool LoadAttributesMalayalam(string fileName)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(fileName);
            if (textAsset != null)
            {
                mCharacterAttributesMalayalam = JsonConvert.DeserializeObject<List<CharacterAttributesMalayalam>>(textAsset.text);
                if (mCharacterAttributesMalayalam != null)
                {

                    return true;
                }
                else
                {
                    Debug.LogError("Failed to deserialize languague data");
                    return false;
                }
            }
            else
            {
                Debug.LogError($"Failed to load {fileName}");
                return false;
            }
        }

        protected virtual bool LoadAttributesBengali(string fileName)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(fileName);
            if (textAsset != null)
            {
                mCharacterAttributesBengali = JsonConvert.DeserializeObject<List<CharacterAttributesBengali>>(textAsset.text);
                if (mCharacterAttributesBengali != null)
                {

                    return true;
                }
                else
                {
                    Debug.LogError("Failed to deserialize languague data");
                    return false;
                }
            }
            else
            {
                Debug.LogError($"Failed to load {fileName}");
                return false;
            }
        }



        public virtual string Convert(string text)
        {
            // Debug.LogError("Do not call base class. Derived class should handle all convertion.");
            Debug.LogError("Do not call base class. Derived class should handle all convertion.");
            return text;

        }

    }

}

   



