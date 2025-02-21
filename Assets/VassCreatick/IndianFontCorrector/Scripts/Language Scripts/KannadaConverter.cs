using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndianFontCorrector;
public class KannadaConverter : Converter
{
    public override bool Init()
    {
        Language = Language.Kannada;
        return LoadAttributesKannada("KannadaReplacement");
    }
    public override string Convert(string text)
    {
        string Value = text;
        string AppendString = "";
        string AppendString2 = "";
        string AppendString3 = "";
        string AppendString4 = "";
        string AppendString5 = "";

     
        for (int i = 0; i < mCharacterAttributesKannada.Count; i++)
        {
            if (mCharacterAttributesKannada.Count > 0)
            {


                if (mCharacterAttributesKannada[i].CharacterHexValue == "")
                {

                    for (int j = 0; j < mCharacterAttributesKannada[i].CharacterCombinations.Count; j++)
                    {
                       
                        if (mCharacterAttributesKannada[i].Append == true)
                        {
                            int appendValue = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue2, 16);
                            AppendString = System.Convert.ToChar(appendValue).ToString();
                            Value = Value.Replace(mCharacterAttributesKannada[i].CharacterCombinations[j], mCharacterAttributesKannada[i].CharName + AppendString);
                        }
                        else if (mCharacterAttributesKannada[i].Append2 == true)
                        {
                            int appendValue = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue2, 16);
                            AppendString = System.Convert.ToChar(appendValue).ToString();

                            int appendValue2 = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharName, 16);
                            AppendString2 = System.Convert.ToChar(appendValue2).ToString();
                          
                            Value = Value.Replace(mCharacterAttributesKannada[i].CharacterCombinations[j], AppendString + AppendString2);
                        }

                        else if (mCharacterAttributesKannada[i].Append3 == true)
                        {
                            int appendValue = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue2, 16);
                            AppendString = System.Convert.ToChar(appendValue).ToString();

                            int appendValue2 = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue3, 16);
                            AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                            int appendValue3 = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharName, 16);
                            AppendString3 = System.Convert.ToChar(appendValue3).ToString();

                            Value = Value.Replace(mCharacterAttributesKannada[i].CharacterCombinations[j], AppendString + AppendString2 + AppendString3);
                        
                        }
                        else if (mCharacterAttributesKannada[i].Append4 == true)
                        {
                            int appendValue = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue2, 16);
                            AppendString = System.Convert.ToChar(appendValue).ToString();

                            int appendValue2 = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue3, 16);
                            AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                            int appendValue3 = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue4, 16);
                            AppendString3 = System.Convert.ToChar(appendValue3).ToString();

                            int appendValue4 = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharName, 16);
                            AppendString4 = System.Convert.ToChar(appendValue4).ToString();


                            Value = Value.Replace(mCharacterAttributesKannada[i].CharacterCombinations[j], AppendString + AppendString2 + AppendString3 + AppendString4);
                        }
                        else if (mCharacterAttributesKannada[i].Append5 == true)
                        {
                            int appendValue = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue2, 16);
                            AppendString = System.Convert.ToChar(appendValue).ToString();

                            int appendValue2 = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue3, 16);
                            AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                            int appendValue3 = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue4, 16);
                            AppendString3 = System.Convert.ToChar(appendValue3).ToString();

                            int appendValue4 = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue5, 16);
                            AppendString4 = System.Convert.ToChar(appendValue4).ToString();

                            int appendValue5 = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharName, 16);
                            AppendString5 = System.Convert.ToChar(appendValue5).ToString();


                            Value = Value.Replace(mCharacterAttributesKannada[i].CharacterCombinations[j], AppendString + AppendString2 + AppendString3 + AppendString4 + AppendString5);
            
                        }
                        else
                        {
                            Value = Value.Replace(mCharacterAttributesKannada[i].CharacterCombinations[j], mCharacterAttributesKannada[i].CharName);

                        }
                    }
                }
                else
                {

                    int decValue = System.Convert.ToInt32(mCharacterAttributesKannada[i].CharacterHexValue, 16);
                    string Converted = System.Convert.ToChar(decValue).ToString();
                    for (int j = 0; j < mCharacterAttributesKannada[i].CharacterCombinations.Count; j++)
                    {
                        Value = Value.Replace(mCharacterAttributesKannada[i].CharacterCombinations[j], @Converted);
                    }
                }
            }

        }
        return Value;
    }
}
