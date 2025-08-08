using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Networking;
using TMPro;
using System.Linq;

namespace Clouds.Ultilities
{
    public enum LANGUAGE{ English , Arabic, French, Spanish, Portuguese, Hindi, Indonesian, Filipino, Turkish, Thai, Malay, Japanese, Vietnamese}

    public class TranslatorManager : Singleton<TranslatorManager>
    {
        TextMeshProUGUI[] allTexts;
        public LANGUAGE from;
        [ReadOnly] public List<string> input = new List<string>();
        string translateString;
        public LANGUAGE to;
        [ReadOnly]public string[] output;

        Dictionary<LANGUAGE, string> languageDic = new Dictionary<LANGUAGE, string>(){
        {LANGUAGE.English, "en"} , {LANGUAGE.Arabic, "ar"}, {LANGUAGE.French, "fr"} , {LANGUAGE.Spanish, "es"},
        {LANGUAGE.Portuguese, "pt"} , {LANGUAGE.Hindi, "hi"}, {LANGUAGE.Indonesian, "id"} , {LANGUAGE.Filipino, "tl"},
        {LANGUAGE.Turkish, "tr"} , {LANGUAGE.Thai, "th"}, {LANGUAGE.Malay, "ms"} , {LANGUAGE.Japanese, "ja"} , {LANGUAGE.Vietnamese, "vi"}
        };


        IEnumerator GetRequest() {
            string url = String.Format
            ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                languageDic[from], languageDic[to], Uri.EscapeUriString(translateString));
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
    
                string[] pages = url.Split('/');
                int page = pages.Length - 1;
                string finalString = webRequest.downloadHandler.text.TrimStart('[','"');
                string result = finalString.Substring(0, finalString.IndexOf('"'));
                output = result.Split(",");
            }
        }

        public void GetAllTextInput()
        {
            List<TextMeshProUGUI> listTexts = new List<TextMeshProUGUI>();
            listTexts.AddRange(GUIManager.Instance.gameObject.GetComponentsInChildren<TextMeshProUGUI>(true));
            allTexts = listTexts.Where(x => x.text.Any(x => char.IsLetter(x))).ToArray();
            foreach(TextMeshProUGUI text in allTexts)
            {
                input.Add(text.text);
            }
        }

        [Button(ButtonSizes.Large)] [GUIColor(0,1,0)]
        public void Translate()
        {
            GetAllTextInput();
            if(input.Count > 0)
            {
                translateString = System.String.Join(',', input);
                StartCoroutine(GetRequest());
            }
            else 
                Debug.LogError("There's nothing to translate!!");
        }
    }
}

