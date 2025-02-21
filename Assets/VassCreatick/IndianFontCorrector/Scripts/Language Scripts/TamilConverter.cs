using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndianFontCorrector;

public class TamilConverter:Converter
{
    public override bool Init()
    {
        Language = Language.Tamil;
        return LoadAttributesTamil("TamilReplacement");
    }
    public override string Convert(string text)
    {
        
        string Value = text;
        
        for (int i = 0; i < mCharacterAttributesTamil.Count; i++)
        {
           
            if (mCharacterAttributesTamil.Count > 0)
            {
                if (mCharacterAttributesTamil[i].CharacterHexValue == "")
                {
                   
                    for (int j = 0; j < mCharacterAttributesTamil[i].CharacterCombinations.Count; j++)
                    {
                        if (Value.Contains(mCharacterAttributesTamil[i].CharacterCombinations[j]))
                        {
                            Value = Value.Replace(mCharacterAttributesTamil[i].CharacterCombinations[j], mCharacterAttributesTamil[i].CharName);

                        }
                    }
                }
                else
                {

                    int decValue = System.Convert.ToInt32(mCharacterAttributesTamil[i].CharacterHexValue, 16);
                    string Converted = System.Convert.ToChar(decValue).ToString();
                    for (int j = 0; j < mCharacterAttributesTamil[i].CharacterCombinations.Count; j++)
                    {

                        Value = Value.Replace(mCharacterAttributesTamil[i].CharacterCombinations[j], @Converted);
                       

                    }
                }
            }

        }
        return Value;
    }

}
