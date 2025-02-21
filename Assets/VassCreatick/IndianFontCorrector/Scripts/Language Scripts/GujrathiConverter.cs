using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndianFontCorrector;
public class GujrathiConverter : Converter
{
    public override bool Init()
    {
        Language = Language.Gujrathi;
        return true;
    }
    public override string Convert(string text)
    {
        string Value = text;
        
        List<string> GujrathiCharacters = new List<string>{"ક","ખ","ગ","ઘ","ઙ","ચ","છ", "જ", "ઝ", "ઞ","ટ","ઠ","ડ","ઢ" ,"ણ",
            "ત", "થ","દ","ધ","ન","પ","ફ","બ","ભ","મ","ય","ર","લ","વ","શ", "ષ" ,  "સ" ,  "હ",   "ળ" ,  "ક્ષ", "જ્ઞ" };

        List<WrongCharacters> wrongCharacters = new List<WrongCharacters>{
        new WrongCharacters(){ character = "ક્", unicode = "E751" },
        new WrongCharacters(){ character = "ખ્", unicode = "E752" },
        new WrongCharacters(){ character = "ગ્", unicode = "E753" },
        new WrongCharacters(){ character = "ઘ્", unicode = "E754" },
        new WrongCharacters(){ character = "ચ્", unicode = "E756" },
        new WrongCharacters(){ character = "ઞ્", unicode = "E759" },
        new WrongCharacters(){ character = "ણ્", unicode = "E75F" },
        new WrongCharacters(){ character = "ત્", unicode = "E760" },
        new WrongCharacters(){ character = "થ્", unicode = "E761" },
        new WrongCharacters(){ character = "ધ્", unicode = "E763" },
        new WrongCharacters(){ character = "ન્", unicode = "E764" },
        new WrongCharacters(){ character = "પ્", unicode = "E765" },
        new WrongCharacters(){ character = "ફ્", unicode = "E766" },
        new WrongCharacters(){ character = "બ્", unicode = "E767" },
        new WrongCharacters(){ character = "ભ્", unicode = "E768" },
        new WrongCharacters(){ character = "મ્", unicode = "E769" },
        new WrongCharacters(){ character = "ય્", unicode = "E76A" },
        new WrongCharacters(){ character = "લ્", unicode = "E76C" },
        new WrongCharacters(){ character = "ળ્", unicode = "E76D" },
        new WrongCharacters(){ character = "વ્", unicode = "E76E" },
        new WrongCharacters(){ character = "શ્", unicode = "E76F" },
        new WrongCharacters(){ character = "ષ્", unicode = "E770" },
        new WrongCharacters(){ character = "સ્", unicode = "E771" },
        new WrongCharacters() { character = "હ્", unicode = "E772" },
       };


        List<WrongCharacters> IRCharacters = new List<WrongCharacters>
    {
        new WrongCharacters() { character = "ક્", unicode = "E798" },
        new WrongCharacters() { character = "ખ્", unicode = "E799" },
        new WrongCharacters() { character = "ગ્", unicode = "E79A" },
        new WrongCharacters() { character = "ઘ્", unicode = "E79B" },
        new WrongCharacters() { character = "ઙ્", unicode = "E79C" },
        new WrongCharacters() { character = "ચ્", unicode = "E79D" },
        new WrongCharacters() { character = "છ્", unicode = "E79E" },
        new WrongCharacters() { character = "જ્", unicode = "E79F" },
        new WrongCharacters() { character = "ઝ્", unicode = "E7A0" },
        new WrongCharacters() { character = "ઞ્", unicode = "E7A1" },
        new WrongCharacters() { character = "ટ્", unicode = "E7A2" },
        new WrongCharacters() { character = "ઠ્", unicode = "E7A3" },
        new WrongCharacters() { character = "ડ્", unicode = "E7A4" },
        new WrongCharacters() { character = "ઢ્", unicode = "E7A5" },
        new WrongCharacters() { character = "ણ્", unicode = "E7A6" },
        new WrongCharacters() { character = "ત્", unicode = "E7A7" },
        new WrongCharacters() { character = "થ્", unicode = "E7A8" },
        new WrongCharacters() { character = "દ્", unicode = "E7A9" },
        new WrongCharacters() { character = "ધ્", unicode = "E7AA" },
        new WrongCharacters() { character = "ન્", unicode = "E7AB" },
        new WrongCharacters() { character = "પ્", unicode = "E7AC" },
        new WrongCharacters() { character = "ફ્", unicode = "E7AD" },
        new WrongCharacters() { character = "બ્", unicode = "E7AE" },
        new WrongCharacters() { character = "ભ્", unicode = "E7AF" },
        new WrongCharacters() { character = "મ્", unicode = "E7B0" },
        new WrongCharacters() { character = "ય્", unicode = "E7B1" },
        new WrongCharacters() { character = "લ્", unicode = "E7B3" },
        new WrongCharacters() { character = "ળ્", unicode = "E7B4" },
        new WrongCharacters() { character = "વ્", unicode = "E7B5" },
        new WrongCharacters() { character = "શ્", unicode = "E7B6" },
        new WrongCharacters() { character = "ષ્", unicode = "E7B7" },
        new WrongCharacters() { character = "સ્", unicode = "E7B8" },
        new WrongCharacters() { character = "હ્", unicode = "E7B9" },
    };



        List<OCharacters> ocharacters = new List<OCharacters>{
        new OCharacters(){ OCharacter = "કો", HindiCharacter = "ક",ECharacter="કી",AACharacter="કા" },
        new OCharacters(){ OCharacter = "ખો", HindiCharacter = "ખ",ECharacter="ખી",AACharacter="ખા" },
        new OCharacters(){ OCharacter = "ગો", HindiCharacter = "	ગ",ECharacter="ગી",AACharacter="ગા" },
        new OCharacters(){ OCharacter = "ઘો", HindiCharacter = "	ઘ",ECharacter= "ઘી",AACharacter="ઘા"},
        new OCharacters(){ OCharacter = "ઙો", HindiCharacter = "	ઙ",ECharacter= "ઙી",AACharacter="ઙા" },
        new OCharacters(){ OCharacter = "ચો", HindiCharacter = "	ચ",ECharacter= "ચી",AACharacter="ચા" },
        new OCharacters(){ OCharacter = "છો", HindiCharacter = "છ",ECharacter= "છી",AACharacter="છા" },
        new OCharacters(){ OCharacter = "જો", HindiCharacter = "જ",ECharacter= "જી",AACharacter="જા" },
        new OCharacters(){ OCharacter = "ઝો", HindiCharacter = "ઝ",ECharacter= "ઝી",AACharacter="ઝા" },

        new OCharacters(){ OCharacter = "ઞો", HindiCharacter = "	ઞ",ECharacter= "ઞી",AACharacter="ઞા" },
        new OCharacters(){ OCharacter = "ટો", HindiCharacter = "	ટ",ECharacter= "ટી" ,AACharacter="ટા"},
        new OCharacters(){ OCharacter = "ઠો", HindiCharacter = "	ઠ",ECharacter= "ઠી",AACharacter="ઠા" },
        new OCharacters(){ OCharacter = "ડો", HindiCharacter = "ડ",ECharacter= "ડી",AACharacter="ડા" },
        new OCharacters(){ OCharacter = "ઢો", HindiCharacter = "ઢ" ,ECharacter= "ઢી",AACharacter="ઢા"},
        new OCharacters(){ OCharacter = "ણો", HindiCharacter = "ણ",ECharacter= "ણી" ,AACharacter="ણા"},

        new OCharacters(){ OCharacter = "તો", HindiCharacter ="ત" ,ECharacter= "તી",AACharacter="તા" },
        new OCharacters(){ OCharacter = "થો", HindiCharacter = "થ",ECharacter= "થી",AACharacter="થા" },
        new OCharacters(){ OCharacter = "દો", HindiCharacter = "દ",ECharacter= "દી" ,AACharacter="દા"},
        new OCharacters(){ OCharacter = "ધો", HindiCharacter = "ધ" ,ECharacter= "ધી",AACharacter="ધા"},
        new OCharacters(){ OCharacter = "નો", HindiCharacter = "ન" ,ECharacter= "ની",AACharacter="ના"},

        new OCharacters(){ OCharacter = "પો", HindiCharacter = "પ",ECharacter= "પી",AACharacter="પા" },
        new OCharacters(){ OCharacter = "ફો", HindiCharacter = "ફ",ECharacter= "ફી" ,AACharacter="ફા"},
        new OCharacters(){ OCharacter = "બો", HindiCharacter = "બ",ECharacter= "બી" ,AACharacter="બા"},
        new OCharacters(){ OCharacter = "ભો", HindiCharacter = "ભ",ECharacter= "ભી" ,AACharacter="ભા"},
        new OCharacters(){ OCharacter = "મો", HindiCharacter = "મ",ECharacter= "મી",AACharacter="મા" },
        new OCharacters(){ OCharacter = "યો", HindiCharacter = "ય",ECharacter= "યી",AACharacter="યા" },
        new OCharacters(){ OCharacter = "લો", HindiCharacter = "લ",ECharacter= "લી",AACharacter="લા" },

        new OCharacters(){ OCharacter = "વો", HindiCharacter = "વ",ECharacter= "વી",AACharacter="વા" },
        new OCharacters(){ OCharacter = "શો", HindiCharacter = "શ",ECharacter= "શી",AACharacter="શા" },
        new OCharacters(){ OCharacter = "ષો", HindiCharacter = "ષ",ECharacter= "ષી",AACharacter="ષા" },
        new OCharacters(){ OCharacter = "સો", HindiCharacter = "સ",ECharacter= "સી",AACharacter="સા" },
        new OCharacters(){ OCharacter = "હો", HindiCharacter = "હ",ECharacter= "હી",AACharacter="હા" },
       };


        int appendValueE = System.Convert.ToInt32("0ABF", 16);
        string AppendStringE = System.Convert.ToChar(appendValueE).ToString();

        int appendValueET = System.Convert.ToInt32("E862", 16);
        string AppendStringET = System.Convert.ToChar(appendValueET).ToString();

        int appendValueIR = System.Convert.ToInt32("E74F", 16);
        string AppendStringIR = System.Convert.ToChar(appendValueIR).ToString();

        int appendValueM = System.Convert.ToInt32("E706", 16);
        string AppendStringM = System.Convert.ToChar(appendValueM).ToString();

        if (Value.Contains("જ્ઞ"))
        {
            int appendValue = System.Convert.ToInt32("E74E", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            if (Value.Contains(AppendStringE))
            {
                Value = Value.Replace("જ્ઞ" + AppendStringE, AppendStringE + AppendString);
            }
            Value = Value.Replace("જ્ઞ", AppendString);
        }

        if (Value.Contains("ગ્") && Value.Contains("ન"))
        {
            int appendValue = System.Convert.ToInt32("E82E", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ગ્" + "ન", AppendString);
        }

        if (Value.Contains("છ્") && Value.Contains("ય"))
        {
            int appendValue = System.Convert.ToInt32("E828", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("છ્" + "ય", AppendString);
        }
        if (Value.Contains("જ્") && Value.Contains("ય"))
        {
            int appendValue = System.Convert.ToInt32("E830", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("જ્" + "ય", AppendString);
        }

        if (Value.Contains("ટ્") && Value.Contains("ટ"))
        {
            int appendValue = System.Convert.ToInt32("E833", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ટ્" + "ટ", AppendString);
        }

        if (Value.Contains("ટ્") && Value.Contains("ઠ"))
        {
            int appendValue = System.Convert.ToInt32("E836", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ટ્" + "ઠ", AppendString);
        }


        if (Value.Contains("ટ્") && Value.Contains("ય"))
        {
            int appendValue = System.Convert.ToInt32("E838", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ટ્" + "ય", AppendString);
        }

        if (Value.Contains("ઠ્") && Value.Contains("ઠ"))
        {
            int appendValue = System.Convert.ToInt32("E83A", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ઠ્" + "ઠ", AppendString);
        }


        if (Value.Contains("ડ્") && Value.Contains("ડ"))
        {
            int appendValue = System.Convert.ToInt32("E83D", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ડ્" + "ડ", AppendString);
        }

        if (Value.Contains("ઢ્") && Value.Contains("ઢ"))
        {
            int appendValue = System.Convert.ToInt32("E841", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ઢ્" + "ઢ", AppendString);
        }

        if (Value.Contains("ઢ્") && Value.Contains("ય"))
        {
            int appendValue = System.Convert.ToInt32("E842", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ઢ્" + "ય", AppendString);
        }

        if (Value.Contains("ત્") && Value.Contains("ત"))
        {
            int appendValue = System.Convert.ToInt32("E843", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ત્" + "ત", AppendString);
        }

        if (Value.Contains("દ્") && Value.Contains("ગ"))
        {
            int appendValue = System.Convert.ToInt32("E844", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("દ્" + "ગ", AppendString);
        }

        if (Value.Contains("દ્") && Value.Contains("ધ"))
        {
            int appendValue = System.Convert.ToInt32("E845", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("દ્" + "ધ", AppendString);
        }
        if (Value.Contains("દ્") && Value.Contains("દ"))
        {
            int appendValue = System.Convert.ToInt32("E846", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("દ્" + "દ", AppendString);
        }

        if (Value.Contains("દ્") && Value.Contains("ન"))
        {
            int appendValue = System.Convert.ToInt32("E848", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("દ્" + "ન", AppendString);
        }

        if (Value.Contains("દ્") && Value.Contains("બ"))
        {
            int appendValue = System.Convert.ToInt32("E849", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("દ્" + "બ", AppendString);
        }

        if (Value.Contains("દ્") && Value.Contains("મ"))
        {
            int appendValue = System.Convert.ToInt32("E84B", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("દ્" + "મ", AppendString);
        }

        if (Value.Contains("દ્") && Value.Contains("ય"))
        {
            int appendValue = System.Convert.ToInt32("E84C", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("દ્" + "ય", AppendString);
        }

        if (Value.Contains("દ્") && Value.Contains("વ"))
        {
            int appendValue = System.Convert.ToInt32("E84D", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("દ્" + "વ", AppendString);
        }

        if (Value.Contains("ન્") && Value.Contains("ન"))
        {
            int appendValue = System.Convert.ToInt32("E84E", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ન્" + "ન", AppendString);
        }

        if (Value.Contains("ફ્") && Value.Contains("ય"))
        {
            int appendValue = System.Convert.ToInt32("E84F", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ફ્" + "ય", AppendString);
        }

        if (Value.Contains("ળ્") && Value.Contains("ય"))
        {
            int appendValue = System.Convert.ToInt32("E850", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ળ્" + "ય", AppendString);
        }

        if (Value.Contains("શ્") && Value.Contains("ચ"))
        {
            int appendValue = System.Convert.ToInt32("E851", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("શ્" + "ચ", AppendString);
        }

        if (Value.Contains("શ્") && Value.Contains("ન"))
        {
            int appendValue = System.Convert.ToInt32("E852", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("શ્" + "ન", AppendString);
        }
        if (Value.Contains("શ્") && Value.Contains("વ"))
        {
            int appendValue = System.Convert.ToInt32("E853", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("શ્" + "વ", AppendString);
        }
        if (Value.Contains("શ્") && Value.Contains("લ"))
        {
            int appendValue = System.Convert.ToInt32("E854", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("શ્" + "લ", AppendString);
        }

        if (Value.Contains("ષ્") && Value.Contains("ટ"))
        {
            int appendValue = System.Convert.ToInt32("E855", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("શ્" + "ટ", AppendString);
        }

        if (Value.Contains("ષ્") && Value.Contains("ઠ"))
        {
            int appendValue = System.Convert.ToInt32("E857", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ષ્" + "ઠ", AppendString);
        }
        if (Value.Contains("હ્") && Value.Contains("ણ"))
        {
            int appendValue = System.Convert.ToInt32("E85A", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("હ્" + "ણ", AppendString);
        }
        if (Value.Contains("હ્ન") && Value.Contains("ન"))
        {
            int appendValue = System.Convert.ToInt32("E85B", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("હ્" + "ન", AppendString);
        }
        if (Value.Contains("હ્") && Value.Contains("મ"))
        {
            int appendValue = System.Convert.ToInt32("E85C", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("હ્" + "મ", AppendString);
        }
        if (Value.Contains("હ્") && Value.Contains("ય"))
        {
            int appendValue = System.Convert.ToInt32("E85D", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("હ્" + "ય", AppendString);
        }
        if (Value.Contains("હ્") && Value.Contains("લ"))
        {
            int appendValue = System.Convert.ToInt32("E85E", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("હ્" + "લ", AppendString);
        }
        if (Value.Contains("હ્") && Value.Contains("વ"))
        {
            int appendValue = System.Convert.ToInt32("E85F", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("હ્" + "વ", AppendString);
        }
        if (Value.Contains("રુ"))
        {
            int appendValue = System.Convert.ToInt32("E88B", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("રુ", AppendString);
        }
      
        for (int i = 0; i < wrongCharacters.Count; i++)
        {
            for (int j = 0; j < IRCharacters.Count; j++)
            {

                if (Value.Contains(wrongCharacters[i].character) && Value.Contains(IRCharacters[j].character) && Value.Contains(AppendStringE) && Value.Contains("ર"))
                {
                   
                    int AppendValue = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString = System.Convert.ToChar(AppendValue).ToString();

                    int AppendValue2 = System.Convert.ToInt32(IRCharacters[j].unicode, 16);
                    string AppendString2 = System.Convert.ToChar(AppendValue2).ToString();

                    Value = Value.Replace(wrongCharacters[i].character + IRCharacters[j].character + "ર" + AppendStringE, AppendStringET + AppendString + AppendString2);
                }
                if (Value.Contains(wrongCharacters[i].character) && Value.Contains(IRCharacters[j].character) && Value.Contains("ર"))
                {
                    int appendValue2 = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                    int appendValue3 = System.Convert.ToInt32(IRCharacters[j].unicode, 16);
                    string AppendString3 = System.Convert.ToChar(appendValue3).ToString();

                    Value = Value.Replace(wrongCharacters[i].character + IRCharacters[j].character + "ર", AppendString2 + AppendString3);


                }
            }
        }

      
        for (int i = 0; i < GujrathiCharacters.Count; i++)
        {

            if (Value.Contains("ક્") && Value.Contains("ષ્") && Value.Contains(GujrathiCharacters[i]))
            {
                int AppendValue2 = System.Convert.ToInt32("E774", 16);
                string AppendString2 = System.Convert.ToChar(AppendValue2).ToString();

                if (Value.Contains(AppendStringE))
                {
                    Value = Value.Replace("ક્" + "ષ્" + GujrathiCharacters[i] + AppendStringE, AppendStringET + AppendString2 + GujrathiCharacters[i]);
                }
                Value = Value.Replace("ક્" + "ષ્" + GujrathiCharacters[i], AppendString2 + GujrathiCharacters[i]);
            }

        }

        if (Value.Contains("ક્") && Value.Contains("ષ્"))
        {

            int AppendValue2 = System.Convert.ToInt32("E74D", 16);
            string AppendString2 = System.Convert.ToChar(AppendValue2).ToString();

            Value = Value.Replace("ક્" + "ષ્", AppendString2);
        }
        if (Value.Contains("ક્") && Value.Contains("ષ") && Value.Contains(AppendStringE))
        {
            int AppendValue2 = System.Convert.ToInt32("E74D", 16);
            string AppendString2 = System.Convert.ToChar(AppendValue2).ToString();
            Value = Value.Replace("ક્" + "ષ" + AppendStringE, AppendStringE + AppendString2);
        }

    
        for (int i = 0; i < IRCharacters.Count; i++)
        {
            if (Value.Contains(IRCharacters[i].character) && Value.Contains(AppendStringE) && Value.Contains("ર"))
            {
                
                int AppendValue = System.Convert.ToInt32(IRCharacters[i].unicode, 16);
                string AppendString = System.Convert.ToChar(AppendValue).ToString();
                Value = Value.Replace(IRCharacters[i].character + "ર" + AppendStringE, AppendStringM + AppendString);
            }
            if (Value.Contains("ર્") && Value.Contains(IRCharacters[i].character) && Value.Contains("ર"))
            {
              
                int appendValue = System.Convert.ToInt32(IRCharacters[i].unicode, 16);
                string AppendString = System.Convert.ToChar(appendValue).ToString();

                if (Value.Contains(AppendStringE))
                {
                    Value = Value.Replace(IRCharacters[i].character + "ર" + AppendStringE, AppendStringE + AppendString);
                }

                Value = Value.Replace("ર્" + IRCharacters[i].character + "ર", AppendString + AppendStringIR);


            }

           
            if (Value.Contains(IRCharacters[i].character) && Value.Contains("ર"))
            {
               
                int AppendValue = System.Convert.ToInt32(IRCharacters[i].unicode, 16);
                string AppendString = System.Convert.ToChar(AppendValue).ToString();
                Value = Value.Replace(IRCharacters[i].character + "ર", AppendString);
            }

        }

        for (int i = 0; i < wrongCharacters.Count; i++)
        {
            for (int j = 0; j < ocharacters.Count; j++)
            {
                if (Value.Contains("ર્") && Value.Contains(wrongCharacters[i].character) && Value.Contains(ocharacters[j].OCharacter))
                {

                    int AppendValue = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString = System.Convert.ToChar(AppendValue).ToString();

                    int appendValue2 = System.Convert.ToInt32("E876", 16);
                    string AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                    Value = Value.Replace("ર્" + wrongCharacters[i].character + ocharacters[j].OCharacter, AppendString + ocharacters[j].HindiCharacter + AppendString2);

                }
            }
        }

        for (int i = 0; i < wrongCharacters.Count; i++)
        {

            for (int k = 0; k < GujrathiCharacters.Count; k++)
            {

                if (Value.Contains(wrongCharacters[i].character) && Value.Contains(GujrathiCharacters[k]) && Value.Contains(AppendStringE))
                {
                    int AppendValue2 = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString2 = System.Convert.ToChar(AppendValue2).ToString();

                    string oldvalue = wrongCharacters[i].character + GujrathiCharacters[k] + AppendStringE;
                    Value = Value.Replace(oldvalue, AppendStringET + AppendString2 + GujrathiCharacters[k]);

                }

                if (Value.Contains(wrongCharacters[i].character) && Value.Contains(GujrathiCharacters[k]))
                {
                    int AppendValue = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString = System.Convert.ToChar(AppendValue).ToString();

                    Value = Value.Replace(wrongCharacters[i].character + GujrathiCharacters[k], AppendString + GujrathiCharacters[k]);

                }
            }
        }

      
        for (int i = 0; i < wrongCharacters.Count; i++)
        {
            for (int j = 0; j < IRCharacters.Count; j++)
            {
                if (Value.Contains(wrongCharacters[i].character) && Value.Contains(IRCharacters[j].character) && Value.Contains("ર"))
                {
  
                    int appendValue = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString = System.Convert.ToChar(appendValue).ToString();

                    int appendValue2 = System.Convert.ToInt32(IRCharacters[j].unicode, 16);
                    string AppendString2 = System.Convert.ToChar(appendValue2).ToString();
                    Value = Value.Replace(wrongCharacters[i].character + IRCharacters[j].character + "ર", AppendString + AppendString2);

                }

            }

        }

        for (int i = 0; i < GujrathiCharacters.Count; i++)
        {
          
            if (Value.Contains("ર્") && Value.Contains(GujrathiCharacters[i]) && Value.Contains(AppendStringE))
            {
                 Value = Value.Replace("ર્" + GujrathiCharacters[i] + AppendStringE, AppendStringM + GujrathiCharacters[i] + AppendStringIR);
            }
            if (Value.Contains("ર્") && Value.Contains(GujrathiCharacters[i]))
            {
               Value = Value.Replace("ર્" + GujrathiCharacters[i], GujrathiCharacters[i] + AppendStringIR);
            }
        }

        for (int i = 0; i < wrongCharacters.Count; i++)
        {
            if (Value.Contains(wrongCharacters[i].character))
            {
                int appendValue = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                string AppendString = System.Convert.ToChar(appendValue).ToString();
                Value = Value.Replace(wrongCharacters[i].character, AppendString);
            }
        }

        for (int i = 0; i < GujrathiCharacters.Count; i++)
        {
            if (Value.Contains(GujrathiCharacters[i]) && Value.Contains(AppendStringE))
            {
                Value = Value.Replace(GujrathiCharacters[i] + AppendStringE, AppendStringE + GujrathiCharacters[i]);
            }
        }
        return Value;
    }
}
