using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame.Ultilities
{
    [CreateAssetMenu(fileName = "New Data List", menuName = "DataList")]
    public class DataList : ScriptableObject
    {
        public List<Data> Datas = new List<Data>();
    }
}
