using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndianFontCorrector;
public class TeluguConverter : Converter
{
    public override bool Init()
    {
        Language = Language.Telugu;
        return LoadAttributesTelugu("TeluguReplacement");
    }
    public override string Convert(string text)
    {
        string Value = text;
        string AppendString = "";
        string AppendString2 = "";
        string AppendString3 = "";
        string AppendString4 = "";
        string AppendString5 = "";

     
        for (int i = 0; i < mCharacterAttributesTelugu.Count; i++)
        {
            if (mCharacterAttributesTelugu.Count > 0)
            {


                if (mCharacterAttributesTelugu[i].CharacterHexValue == "")
                {

                    for (int j = 0; j < mCharacterAttributesTelugu[i].CharacterCombinations.Count; j++)
                    {
                       
                        if (mCharacterAttributesTelugu[i].Append == true)
                        {
                            int appendValue = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue2, 16);
                            AppendString = System.Convert.ToChar(appendValue).ToString();
                            Value = Value.Replace(mCharacterAttributesTelugu[i].CharacterCombinations[j], mCharacterAttributesTelugu[i].CharName + AppendString);
                        }
                        else if (mCharacterAttributesTelugu[i].Append2 == true)
                        {
                            int appendValue = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue2, 16);
                            AppendString = System.Convert.ToChar(appendValue).ToString();

                            int appendValue2 = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharName, 16);
                            AppendString2 = System.Convert.ToChar(appendValue2).ToString();
                          
                            Value = Value.Replace(mCharacterAttributesTelugu[i].CharacterCombinations[j], AppendString + AppendString2);
                        }

                        else if (mCharacterAttributesTelugu[i].Append3 == true)
                        {
                            int appendValue = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue2, 16);
                            AppendString = System.Convert.ToChar(appendValue).ToString();

                            int appendValue2 = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue3, 16);
                            AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                            int appendValue3 = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharName, 16);
                            AppendString3 = System.Convert.ToChar(appendValue3).ToString();

                            Value = Value.Replace(mCharacterAttributesTelugu[i].CharacterCombinations[j], AppendString + AppendString2 + AppendString3);
                        
                        }
                        else if (mCharacterAttributesTelugu[i].Append4 == true)
                        {
                            int appendValue = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue2, 16);
                            AppendString = System.Convert.ToChar(appendValue).ToString();

                            int appendValue2 = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue3, 16);
                            AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                            int appendValue3 = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue4, 16);
                            AppendString3 = System.Convert.ToChar(appendValue3).ToString();

                            int appendValue4 = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharName, 16);
                            AppendString4 = System.Convert.ToChar(appendValue4).ToString();


                            Value = Value.Replace(mCharacterAttributesTelugu[i].CharacterCombinations[j], AppendString + AppendString2 + AppendString3 + AppendString4);
                        }
                        else if (mCharacterAttributesTelugu[i].Append5 == true)
                        {
                            int appendValue = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue2, 16);
                            AppendString = System.Convert.ToChar(appendValue).ToString();

                            int appendValue2 = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue3, 16);
                            AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                            int appendValue3 = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue4, 16);
                            AppendString3 = System.Convert.ToChar(appendValue3).ToString();

                            int appendValue4 = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue5, 16);
                            AppendString4 = System.Convert.ToChar(appendValue4).ToString();

                            int appendValue5 = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharName, 16);
                            AppendString5 = System.Convert.ToChar(appendValue5).ToString();


                            Value = Value.Replace(mCharacterAttributesTelugu[i].CharacterCombinations[j], AppendString + AppendString2 + AppendString3 + AppendString4 + AppendString5);
            
                        }
                        else
                        {
                            Value = Value.Replace(mCharacterAttributesTelugu[i].CharacterCombinations[j], mCharacterAttributesTelugu[i].CharName);

                        }
                    }
                }
                else
                {

                    int decValue = System.Convert.ToInt32(mCharacterAttributesTelugu[i].CharacterHexValue, 16);
                    string Converted = System.Convert.ToChar(decValue).ToString();
                    for (int j = 0; j < mCharacterAttributesTelugu[i].CharacterCombinations.Count; j++)
                    {
                        Value = Value.Replace(mCharacterAttributesTelugu[i].CharacterCombinations[j], @Converted);
                    }
                }
            }

        }
        return Value;
    }
}
