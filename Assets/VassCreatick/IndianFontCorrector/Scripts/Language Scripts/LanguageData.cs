using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

// Start is called before the first frame update
[Serializable]
public class CharacterAttributesTamil
{
    [JsonProperty] public string Character { get; private set; }
    [JsonProperty] public List<string> CharacterCombinations { get; private set; }
    [JsonProperty] public string CharacterHexValue { get; private set; }
    [JsonProperty] public string CharName { get; private set; }
}

[Serializable]
public class CharacterAttributesHindi
{
    [JsonProperty] public string Character { get; private set; }

    [JsonProperty] public bool Append { get; private set; }
    [JsonProperty] public bool AppendBefore { get; private set; }
    [JsonProperty] public bool AppendFirst { get; private set; }
    [JsonProperty] public bool AppendStart { get; private set; }
    [JsonProperty] public List<string> CharacterCombinations { get; private set; }
    [JsonProperty] public string CharacterHexValue { get; private set; }
    [JsonProperty] public string CharacterHexValue2 { get; private set; }
    [JsonProperty] public string CharRetain { get; private set; }
    [JsonProperty] public string CharRetain2 { get; private set; }
    [JsonProperty] public string CharName { get; private set; }
}
[Serializable]
public class CharacterAttributesTelugu
{
    [JsonProperty] public string Character { get; private set; }
    [JsonProperty] public bool Append { get; private set; }
    [JsonProperty] public bool Append2 { get; private set; }
    [JsonProperty] public bool Append3 { get; private set; }
    [JsonProperty] public bool Append4 { get; private set; }
    [JsonProperty] public bool Append5 { get; private set; }
    [JsonProperty] public List<string> CharacterCombinations { get; private set; }
    [JsonProperty] public string CharacterHexValue { get; private set; }
    [JsonProperty] public string CharacterHexValue2 { get; private set; }
    [JsonProperty] public string CharacterHexValue3 { get; private set; }
    [JsonProperty] public string CharacterHexValue4 { get; private set; }
    [JsonProperty] public string CharacterHexValue5 { get; private set; }
    [JsonProperty] public string CharName { get; private set; }
}

[Serializable]
public class CharacterAttributesKannada
{
    [JsonProperty] public string Character { get; private set; }
    [JsonProperty] public bool Append { get; private set; }
    [JsonProperty] public bool Append2 { get; private set; }
    [JsonProperty] public bool Append3 { get; private set; }
    [JsonProperty] public bool Append4 { get; private set; }
    [JsonProperty] public bool Append5 { get; private set; }
    [JsonProperty] public List<string> CharacterCombinations { get; private set; }
    [JsonProperty] public string CharacterHexValue { get; private set; }
    [JsonProperty] public string CharacterHexValue2 { get; private set; }
    [JsonProperty] public string CharacterHexValue3 { get; private set; }
    [JsonProperty] public string CharacterHexValue4 { get; private set; }
    [JsonProperty] public string CharacterHexValue5 { get; private set; }
    [JsonProperty] public string CharName { get; private set; }
}

[Serializable]
public class CharacterAttributesMalayalam
{
    [JsonProperty] public string Character { get; private set; }
    [JsonProperty] public bool Change { get; private set; }
    [JsonProperty] public bool Change2 { get; private set; }
    [JsonProperty] public bool Change3 { get; private set; }
    [JsonProperty] public bool Change4 { get; private set; }
    [JsonProperty] public bool Append { get; private set; }
    [JsonProperty] public bool Append2 { get; private set; }
    [JsonProperty] public bool Append3 { get; private set; }
    [JsonProperty] public bool Append4 { get; private set; }
    [JsonProperty] public List<string> CharacterCombinations { get; private set; }
    [JsonProperty] public string CharacterHexValue { get; private set; }
    [JsonProperty] public string CharacterHexValue2 { get; private set; }
    [JsonProperty] public string CharacterHexValue3 { get; private set; }
    [JsonProperty] public string CharacterHexValue4 { get; private set; }
    [JsonProperty] public string CharName { get; private set; }
}

[Serializable]
public class CharacterAttributesBengali
{
    [JsonProperty] public string Character { get; private set; }
    [JsonProperty] public bool Append { get; private set; }
    [JsonProperty] public bool Append2 { get; private set; }
    [JsonProperty] public List<string> CharacterCombinations { get; private set; }
    [JsonProperty] public string CharacterHexValue { get; private set; }
    [JsonProperty] public string CharacterHexValue2 { get; private set; }
    [JsonProperty] public string CharacterHexValue3 { get; private set; }
    [JsonProperty] public string CharName { get; private set; }
}

public enum Language
{
    None = -1,
    English,
    EnglishUS,
    Tamil,
    Hindi,
    Telugu,
    Kannada,
    Malayalam,
    Bengali,
    Gujrathi,
    Marathi,

    Punjabi

}

