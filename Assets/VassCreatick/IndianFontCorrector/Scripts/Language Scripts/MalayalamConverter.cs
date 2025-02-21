using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndianFontCorrector;
public class MalayalamConverter : Converter
{
    public override bool Init()
    {
        Language = Language.Malayalam;
        return LoadAttributesMalayalam("MalayalamReplacer");
    }
    public override string Convert(string text)
    {
        string Value = text;
        string AppendString = "";
        string AppendString2 = "";
        string AppendString3 = "";
        List<string> tempCharacters = new List<string>{"ക", "ഖ",
    "ഗ",
    "ഘ",
    "ങ",
    "ച",
    "ഛ",
    "ജ",
    "ഝ",
    "ഞ",
    "ട",
    "ഠ",
    "ഡ",
    "ഢ",
    "ണ",
    "ത",
    "ഥ",
    "ദ",
    "ധ",
    "ന",
    "പ",
    "ഫ",
    "ബ",
    "ഭ",
    "മ",
    "യ",
    "ര",
    "ല",
    "വ",
    "ശ",
    "ഷ",
    "സ",
    "ഹ",
    "ള",
    "ഴ",
    "റ" };
        for (int i = 0; i < mCharacterAttributesMalayalam.Count; i++)
        {
            if (mCharacterAttributesMalayalam.Count > 0)
            {
                if (mCharacterAttributesMalayalam[i].CharacterHexValue == "")
                {

                    for (int j = 0; j < mCharacterAttributesMalayalam[i].CharacterCombinations.Count; j++)
                    {


                        if (Value.Contains(mCharacterAttributesMalayalam[i].CharacterCombinations[j]))
                        {

                            // Debug.Log("Character Append " + _CharacterAttributes[i].Character);
                            if (mCharacterAttributesMalayalam[i].Change == true)
                            {

                                int appendValue = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue2, 16);
                                AppendString = System.Convert.ToChar(appendValue).ToString();



                                string noreplace = "";
                                for (int k = 0; k < tempCharacters.Count; k++)
                                {

                                    if (mCharacterAttributesMalayalam[i].CharacterCombinations[j].Contains(tempCharacters[k]))
                                    {

                                        noreplace = tempCharacters[k];

                                     //   Debug.Log("noreplace" + noreplace);


                                    }


                                }


                                Value = Value.Replace(mCharacterAttributesMalayalam[i].CharacterCombinations[j], mCharacterAttributesMalayalam[i].CharName + AppendString);

                            }
                            else if (mCharacterAttributesMalayalam[i].Change2 == true)
                            {

                                int appendValue = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue2, 16);
                                AppendString = System.Convert.ToChar(appendValue).ToString();


                                string noreplace = "";

                                for (int k = 0; k < tempCharacters.Count; k++)
                                {

                                    if (mCharacterAttributesMalayalam[i].CharacterCombinations[j].Contains(tempCharacters[k]))
                                    {

                                        noreplace = tempCharacters[k];

                                    }
                                }


                                Value = Value.Replace(mCharacterAttributesMalayalam[i].CharacterCombinations[j], AppendString + noreplace);
                             //   Debug.Log("noreplace change2" + noreplace);


                            }
                            else if (mCharacterAttributesMalayalam[i].Change3 == true)
                            {

                                int appendValue = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue2, 16);
                                AppendString = System.Convert.ToChar(appendValue).ToString();




                                Value = Value.Replace(mCharacterAttributesMalayalam[i].CharacterCombinations[j], AppendString + mCharacterAttributesMalayalam[i].CharName);



                            }
                            else if (mCharacterAttributesMalayalam[i].Change4 == true)
                            {

                                int appendValue = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue2, 16);
                                AppendString = System.Convert.ToChar(appendValue).ToString();

                                int appendValue2 = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue3, 16);
                                AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                                Value = Value.Replace(mCharacterAttributesMalayalam[i].CharacterCombinations[j], AppendString + AppendString2 + mCharacterAttributesMalayalam[i].CharName);

                            }


                            else if (mCharacterAttributesMalayalam[i].Append == true)
                            {

                                int appendValue = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue2, 16);
                                AppendString = System.Convert.ToChar(appendValue).ToString();

                                Value = Value.Replace(mCharacterAttributesMalayalam[i].CharacterCombinations[j], mCharacterAttributesMalayalam[i].CharName + AppendString);


                            }
                            else if (mCharacterAttributesMalayalam[i].Append2 == true)
                            {

                                int appendValue = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue2, 16);
                                AppendString = System.Convert.ToChar(appendValue).ToString();

                                if (mCharacterAttributesMalayalam[i].CharacterHexValue3 != null && mCharacterAttributesMalayalam[i].CharacterHexValue3 != "")
                                {
                                    int appendValue2 = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue3, 16);
                                    AppendString2 = System.Convert.ToChar(appendValue2).ToString();
                                }



                                string noreplace = "";

                                for (int k = 0; k < tempCharacters.Count; k++)
                                {

                                    if (mCharacterAttributesMalayalam[i].CharacterCombinations[j].Contains(tempCharacters[k]))
                                    {

                                        noreplace = tempCharacters[k];

                                    }
                                }


                                Value = Value.Replace(mCharacterAttributesMalayalam[i].CharacterCombinations[j], AppendString + noreplace + AppendString2);


                            }
                            else if (mCharacterAttributesMalayalam[i].Append3 == true)
                            {


                                int appendValue = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue2, 16);
                                AppendString = System.Convert.ToChar(appendValue).ToString();

                                if (mCharacterAttributesMalayalam[i].CharacterHexValue3 != null && mCharacterAttributesMalayalam[i].CharacterHexValue3 != "")
                                {
                                    int appendValue2 = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue3, 16);
                                    AppendString2 = System.Convert.ToChar(appendValue2).ToString();
                                }

                                if (mCharacterAttributesMalayalam[i].CharacterHexValue4 != null && mCharacterAttributesMalayalam[i].CharacterHexValue4 != "")
                                {
                                    int appendValue3 = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue4, 16);
                                    AppendString3 = System.Convert.ToChar(appendValue3).ToString();
                                }



                                string noreplace = "";

                                for (int k = 0; k < tempCharacters.Count; k++)
                                {

                                    if (mCharacterAttributesMalayalam[i].CharacterCombinations[j].Contains(tempCharacters[k]))
                                    {

                                        noreplace = tempCharacters[k];

                                    }
                                }


                                Value = Value.Replace(mCharacterAttributesMalayalam[i].CharacterCombinations[j], AppendString + noreplace + AppendString2 + AppendString3);
                                //Debug.Log("Character Append2 " + _CharacterAttributes[i].Character);

                            }
                            else if (mCharacterAttributesMalayalam[i].Append4 == true)
                            {


                                int appendValue = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue2, 16);
                                AppendString = System.Convert.ToChar(appendValue).ToString();

                                if (mCharacterAttributesMalayalam[i].CharacterHexValue3 != null && mCharacterAttributesMalayalam[i].CharacterHexValue3 != "")
                                {
                                    int appendValue2 = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue3, 16);
                                    AppendString2 = System.Convert.ToChar(appendValue2).ToString();
                                }

                                if (mCharacterAttributesMalayalam[i].CharacterHexValue4 != null && mCharacterAttributesMalayalam[i].CharacterHexValue4 != "")
                                {
                                    int appendValue3 = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue4, 16);
                                    AppendString3 = System.Convert.ToChar(appendValue3).ToString();
                                }






                                Value = Value.Replace(mCharacterAttributesMalayalam[i].CharacterCombinations[j], AppendString + AppendString2 + AppendString3);


                            }



                        }
                    }
                }
                else
                {

                    int decValue = System.Convert.ToInt32(mCharacterAttributesMalayalam[i].CharacterHexValue, 16);
                    string Converted = System.Convert.ToChar(decValue).ToString();
                    for (int j = 0; j < mCharacterAttributesMalayalam[i].CharacterCombinations.Count; j++)
                    {

                        Value = Value.Replace(mCharacterAttributesMalayalam[i].CharacterCombinations[j], @Converted);

                    }
                }
            }

        }
        return Value;
    }
}
