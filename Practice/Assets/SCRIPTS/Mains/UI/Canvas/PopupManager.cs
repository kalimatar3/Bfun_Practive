using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuckGame.Ultilities;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Clouds.Ultilities
{
    [System.Serializable]
    public class Popup
    {
        [TitleGroup("POPUP", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        [GUIColor(0, 1, 1)]
        public string popupName;
        public bool IsShowed = false;
        public GameObject panelGO;
        public Transform SpawnPoint;
        public Basepanel Panel { get { return panelGO.GetComponent<Basepanel>(); }}
    }
    public class PopupManager : Singleton<PopupManager>
    {

        public static CanvasGUI Popup
        {
            get
            {
                return GUIManager.Instance.currentPopupGUI;
            }
        }

        public static CanvasGUI PopupMultiScene
        {
            get
            {
                return GUIManager.Instance.popupGUIMultiScene;
            }
        }
    }
}
