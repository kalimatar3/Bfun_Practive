//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//#if USE_ADMOB
//using GoogleMobileAds.Editor;
//#endif

//namespace DuckGame.Ultilities
//{
//    [CreateAssetMenu(fileName = "GoogleMobileRefrence", menuName = "RenfreceGoogleMobileAds")]
//    public class RefenceToGoogleMoblieAds : ScriptableObject
//    {
//#if USE_ADMOB
//        [SerializeField] GoogleMobileAdsSettings googleMobileAdsSettings;

//        public string AndroidAppID
//        {
//            get
//            {
//                return googleMobileAdsSettings.GoogleMobileAdsAndroidAppId;
//            }
//            set
//            {
//                googleMobileAdsSettings.GoogleMobileAdsAndroidAppId = value;
//            }
//        }

//        public string IOSAppID
//        {
//            get
//            {
//                return googleMobileAdsSettings.GoogleMobileAdsIOSAppId;
//            }
//            set
//            {
//                googleMobileAdsSettings.GoogleMobileAdsIOSAppId = value;
//            }
//        }

//        public bool DelayAppMesurement
//        {
//            get
//            {
//                return googleMobileAdsSettings.DelayAppMeasurementInit;
//            }
//            set
//            {
//                googleMobileAdsSettings.DelayAppMeasurementInit = value;
//            }
//        }
//#endif
//    }
//}
