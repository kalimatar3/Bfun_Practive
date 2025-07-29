using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Reflection;
using System;

namespace DuckGame.Ultilities
{
    public enum DataType { Float, String, Int };

    [System.Serializable]
    public class Data
    {
        [TitleGroup("DATA", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        [GUIColor(0, 1, 0)]
        public string dataName;
        public DataType dataType;
        [ShowIf("dataType", DataType.Float)]
        public float valueFloat;
        [ShowIf("dataType", DataType.String)]
        public string valueString;
        [ShowIf("dataType", DataType.Int)]
        public int valueInt;
    }

    [HideMonoScript]
    public class SaveManager : MonoBehaviour
    {
        [HideLabel]
        [PreviewField(100, ObjectFieldAlignment.Center)]
        public Sprite Icon;
        
        [TitleGroup("DATA MANAGER", "@DuckGame", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        [SerializeField] string dataAccessName;
    #if UNITY_EDITOR
        [SerializeField] [ReadOnly] MonoScript dataAccessScript;
    #endif
        [OnValueChanged("OnListDataChanged")]
        [SerializeField] DataList ListDatas;
        [Searchable]
        [SerializeField] List<Data> Datas = new List<Data>();

    #if UNITY_EDITOR
        [OnInspectorInit]
        void Property()
        {
            if (dataAccessScript != null)
            {
                var prop = dataAccessScript.GetClass().GetProperties();
                for (int i = 0; i < dataAccessScript.GetClass().GetProperties().Length; i++)
                {
                    switch (Datas[i].dataType)
                    {
                        case DataType.Float:
                            Datas[i].valueFloat = (float)prop[i].GetValue(null);
                            break;
                        case DataType.String:
                            Datas[i].valueString = (string)prop[i].GetValue(null);
                            break;
                        case DataType.Int:
                            Datas[i].valueInt = (int)prop[i].GetValue(null);
                            break;
                    }
                }
            }
        }
        void OnListDataChanged()
        {
            if(ListDatas != null)
            {
                Datas = ListDatas.Datas;
                PlayerPrefs.DeleteAll();
                SAVEDATA();
            }
        }

        bool CheckEmptyString()
        {
            bool NoEmptyString = true;
            foreach (Data data in Datas)
            {
                if (data.dataName == null)
                    NoEmptyString = false;
            }

            return NoEmptyString;
        }

        [BoxGroup()]
        [Button(ButtonSizes.Large), GUIColor(0, 1, 0)]
        void SAVEDATA() 
        {   
            if (CheckEmptyString())
            {
                string filePathAndName = "Assets/DuckUtilities/SaveManager/"  + dataAccessName + ".cs";
                using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
                {
                    streamWriter.WriteLine("using UnityEngine;");
                    streamWriter.WriteLine("namespace DuckGame.Ultilities");
                    streamWriter.WriteLine("{");
                    streamWriter.WriteLine("public class " + dataAccessName);
                    streamWriter.WriteLine("{");
                    foreach (Data data in Datas)
                    {
                        object value = null;
                        switch (data.dataType)
                        {
                            case DataType.Float:
                                value = data.valueFloat;
                                break;
                            case DataType.String:
                                value = data.valueString;
                                break;
                            case DataType.Int:
                                value = data.valueInt;
                                break;
                        }

                        streamWriter.WriteLine("\tpublic static " + data.dataType.ToString().ToLower() + " " + data.dataName);
                        streamWriter.WriteLine("\t{");
                        streamWriter.WriteLine("\t\tget { return PlayerPrefs.Get" + data.dataType.ToString() + "(\"" + data.dataName + "\", " + (value is string ? "\"" + value + "\"" : value) + "); }");
                        streamWriter.WriteLine("\t\tset { PlayerPrefs.Set" + data.dataType.ToString() + "(\"" + data.dataName + "\", value); }");
                        streamWriter.WriteLine("\t}");
                    }
                    streamWriter.WriteLine("}");
                    streamWriter.WriteLine("}");
                }
                AssetDatabase.Refresh();

                string[] prefabsName = AssetDatabase.FindAssets("t:monoscript", new string[] { "Assets/Utilities/SaveManager/" });
                foreach (var guid in prefabsName)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    MonoScript mono = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                    if (mono.name == dataAccessName)
                        dataAccessScript = mono;
                }
            }
            else
                Debug.LogError("Data name must not empty");

            EditorUtility.SetDirty(ListDatas);
            ListDatas.Datas = Datas;
        }


        [BoxGroup()]
        [Button(ButtonSizes.Medium), GUIColor(1, 0, 0)]
        void CLEARDATA()
        {
            PlayerPrefs.DeleteAll();
            Property();
            if (dataAccessScript != null)
            {
                int index = Datas.Count - dataAccessScript.GetClass().GetProperties().Length;
                if (index != 0 && index > 0)
                {
                    Datas.RemoveRange(Datas.Count - index, index);
                }
            }
        }
    #endif
    }
}
