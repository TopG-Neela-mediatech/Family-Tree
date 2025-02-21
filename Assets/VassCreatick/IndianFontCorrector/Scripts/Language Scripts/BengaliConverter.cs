using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndianFontCorrector;
public class BengaliConverter : Converter
{
    public override bool Init()
    {
        Language = Language.Bengali;
        return LoadAttributesBengali("BengaliReplacement");
    }
    public override string Convert(string text)
    {
        string Value = text;
        string AppendString = "";
        string AppendString2 = "";
        List<string> BengaliCharacters = new List<string>{"ক","খ","গ","ঘ","ঙ","চ","ছ","জ","ঝ","ঞ","ট","ঠ","ড","ঢ","ণ","ত","থ","দ","ধ","ন",
            "প","ফ","ব","ভ","ম","য","র","ল","শ","ষ","স","হ" };
       

        int appendValueE = System.Convert.ToInt32("09BF", 16);
        string AppendStringE = System.Convert.ToChar(appendValueE).ToString();

        int appendValueEe = System.Convert.ToInt32("09C7", 16);
        string AppendStringEe = System.Convert.ToChar(appendValueEe).ToString();

        int appendValueM = System.Convert.ToInt32("E47F", 16);
        string AppendStringM = System.Convert.ToChar(appendValueM).ToString();

        int appendValueR = System.Convert.ToInt32("E4CB", 16);
        string AppendStringR = System.Convert.ToChar(appendValueR).ToString();

        for (int i = 0; i < mCharacterAttributesBengali.Count; i++)
        {
            if (mCharacterAttributesBengali.Count > 0)
            {
                if (mCharacterAttributesBengali[i].CharacterHexValue == "")
                {

                    for (int j = 0; j < mCharacterAttributesBengali[i].CharacterCombinations.Count; j++)
                    {
                        if (Value.Contains(mCharacterAttributesBengali[i].CharacterCombinations[j]))
                        {
                            if (mCharacterAttributesBengali[i].Append == true)
                            {
                               
                                int appendValue = System.Convert.ToInt32(mCharacterAttributesBengali[i].CharacterHexValue2, 16);
                                AppendString = System.Convert.ToChar(appendValue).ToString();
                                if (Value.Contains(AppendStringE))
                                {
                                    Value = Value.Replace(mCharacterAttributesBengali[i].CharacterCombinations[j] + AppendStringE, AppendStringM + mCharacterAttributesBengali[i].CharName + AppendString);
                                }
                                Value = Value.Replace(mCharacterAttributesBengali[i].CharacterCombinations[j], mCharacterAttributesBengali[i].CharName + AppendString);


                            }
                            else if (mCharacterAttributesBengali[i].Append2 == true)
                            {

                                int appendValue = System.Convert.ToInt32(mCharacterAttributesBengali[i].CharacterHexValue2, 16);
                                AppendString = System.Convert.ToChar(appendValue).ToString();

                                if (mCharacterAttributesBengali[i].CharacterHexValue2 != null && mCharacterAttributesBengali[i].CharacterHexValue2 != "")
                                {
                                    int appendValue2 = System.Convert.ToInt32(mCharacterAttributesBengali[i].CharacterHexValue3, 16);
                                    AppendString2 = System.Convert.ToChar(appendValue2).ToString();
                                }
                                if (Value.Contains(AppendStringE))
                                {
                                    Value = Value.Replace(mCharacterAttributesBengali[i].CharacterCombinations[j] + AppendStringE, AppendStringM + AppendString + AppendString2);
                                }
                                Value = Value.Replace(mCharacterAttributesBengali[i].CharacterCombinations[j], AppendString + AppendString2);
                               
                            }
                        }
                    }
                }
                else
                {

                    int decValue = System.Convert.ToInt32(mCharacterAttributesBengali[i].CharacterHexValue, 16);
                    string Converted = System.Convert.ToChar(decValue).ToString();
                    for (int j = 0; j < mCharacterAttributesBengali[i].CharacterCombinations.Count; j++)
                    {
                        if (Value.Contains(AppendStringE))
                        {
                            Value = Value.Replace(mCharacterAttributesBengali[i].CharacterCombinations[j] + AppendStringE, AppendStringM + @Converted);
                        }
                        Value = Value.Replace(mCharacterAttributesBengali[i].CharacterCombinations[j], @Converted);
                    }
                }
            }

        }

        for (int i = 0; i < BengaliCharacters.Count; i++)
        {
            if (Value.Contains(BengaliCharacters[i]) && Value.Contains(AppendStringE))
            {
                Value = Value.Replace(BengaliCharacters[i] + AppendStringE, AppendStringM + BengaliCharacters[i]);
            }
            if (Value.Contains(BengaliCharacters[i]) && Value.Contains(AppendStringEe))
            {
                Value = Value.Replace(BengaliCharacters[i] + AppendStringEe, AppendStringEe + BengaliCharacters[i]);
            }

            if (Value.Contains(BengaliCharacters[i]))
            {
                int appendValue = System.Convert.ToInt32("09CB", 16);
                AppendString = System.Convert.ToChar(appendValue).ToString();

                int appendValue2 = System.Convert.ToInt32("09BE", 16);
                AppendString2 = System.Convert.ToChar(appendValue2).ToString();
                if (Value.Contains(AppendString))
                {
                    Value = Value.Replace(BengaliCharacters[i] + AppendString, BengaliCharacters[i] + AppendString2);
                }

            }
            if (Value.Contains(BengaliCharacters[i]) && Value.Contains("র্"))
            {
              //  Debug.Log("Rka");
                Value = Value.Replace("র্" + BengaliCharacters[i], BengaliCharacters[i] + AppendStringR);
            }
        }
        if (Value.Contains("ম"))
        {
            int appendValue = System.Convert.ToInt32("E475", 16);
            AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("ম", AppendString);
        }
        return Value;
    }
}
