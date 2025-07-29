using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using TMPro;


#if USE_FIREBASE
using Firebase.Analytics;
#endif

namespace DuckGame.Ultilities
{
    public enum LOGTYPE{Normal, Warning, Error};
    public static class DuckHelper
    {
        public static Vector3 GetDegreeTransform(Transform tp)
        {
            Vector3 angle = tp.eulerAngles;
            float x = angle.x;
            float y = angle.y;
            float z = angle.z;

            if (Vector3.Dot(tp.up, Vector3.up) >= 0f)
            {
                if (angle.x >= 0f && angle.x <= 90f)
                {
                    x = angle.x;
                }
                if (angle.x >= 270f && angle.x <= 360f)
                {
                    x = angle.x - 360f;
                }
            }
            if (Vector3.Dot(tp.up, Vector3.up) < 0f)
            {
                if (angle.x >= 0f && angle.x <= 90f)
                {
                    x = 180 - angle.x;
                }
                if (angle.x >= 270f && angle.x <= 360f)
                {
                    x = 180 - angle.x;
                }
            }

            if (angle.y > 180)
            {
                y = angle.y - 360f;
            }

            if (angle.z > 180)
            {
                z = angle.z - 360f;
            }

            return new Vector3(x, y, z);
        }

        public static string NameRemoveClone(string name)
        {
            return name.Replace("(Clone)", "").Trim();
        }

        public static List<GameObject> GetAllChilds(GameObject root)
        {
            List<GameObject> result = new List<GameObject>();
            if (root.transform.childCount > 0)
            {
                foreach (Transform VARIABLE in root.transform)
                {
                    Searcher(result, VARIABLE.gameObject);
                }
            }
            return result;
        }

        static void Searcher(List<GameObject> list, GameObject root)
        {
            list.Add(root);
            if (root.transform.childCount > 0)
            {
                foreach (Transform VARIABLE in root.transform)
                {
                    Searcher(list, VARIABLE.gameObject);
                }
            }
        }

        public static IEnumerator TextAddEffect(TextMeshProUGUI text, string world, float delay = 0.05f, Action onDone = null)
        {
            text.text = "";
            foreach (char letter in world)
            {
                text.text += letter;
                yield return new WaitForSeconds(delay);
            }
            onDone?.Invoke();
        }

        public static void Save<T>(T data, string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, data);
                }
            }
            catch
            {
                // Forward exceptions
                throw;
            }
        }

        public static T Load<T>(string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();
                    return (T)formatter.Deserialize(fileStream);
                }
            }
            catch
            {
                // Forward exceptions
                throw;
                // return default;
            }
        }

        public static void SetRectLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }

        public static void SetRectRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }

        public static void SetRectTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }

        public static void SetRectBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }

        public static string RemoveBracketDuplicateName(string nameDuplicate)
        {
            return Regex.Replace(nameDuplicate, @" \(.*\)", "");
        }

        public static Transform SearchTranformsByname(string name, List<GameObject> all)
        {
            return all.Where((go) => go.name == name).First().transform;
        }
        public static List<string> ShuffleListWithOrderBy(List<string> list)
        {
            System.Random random = new System.Random();
            return list.OrderBy(x => random.Next()).ToList();
        }

        public static void LogEditor(string log, LOGTYPE logType = LOGTYPE.Normal)
        {
#if UNITY_EDITOR
            switch (logType)
            {
                case LOGTYPE.Normal:
                    Debug.Log("Duck_Log_Editor => " + log);
                    break;
                case LOGTYPE.Warning:
                    Debug.LogWarning("Duck_Log_Editor => " + log);
                    break;
                case LOGTYPE.Error:
                    Debug.LogError("Duck_Log_Editor => " + log);
                    break;
            }
#endif
        }

        public static void LogGame(string log, LOGTYPE logType = LOGTYPE.Normal)
        {
            switch (logType)
            {
                case LOGTYPE.Normal:
                    Debug.Log("Duck_Log_Game => " + log);
                    break;
                case LOGTYPE.Warning:
                    Debug.LogWarning("Duck_Log_Game => " + log);
                    break;
                case LOGTYPE.Error:
                    Debug.LogError("Duck_Log_Game => " + log);
                    break;
            }
        }

        public static void LogGame(int log, LOGTYPE logType = LOGTYPE.Normal)
        {
            switch (logType)
            {
                case LOGTYPE.Normal:
                    Debug.Log("Duck_Log_Game => " + log);
                    break;
                case LOGTYPE.Warning:
                    Debug.LogWarning("Duck_Log_Game => " + log);
                    break;
                case LOGTYPE.Error:
                    Debug.LogError("Duck_Log_Game => " + log);
                    break;
            }
        }

        public static void LogGame(float log, LOGTYPE logType = LOGTYPE.Normal)
        {
            switch (logType)
            {
                case LOGTYPE.Normal:
                    Debug.Log("Duck_Log_Game => " + log);
                    break;
                case LOGTYPE.Warning:
                    Debug.LogWarning("Duck_Log_Game => " + log);
                    break;
                case LOGTYPE.Error:
                    Debug.LogError("Duck_Log_Game => " + log);
                    break;
            }
        }

        public static void LogGame(double log, LOGTYPE logType = LOGTYPE.Normal)
        {
            switch (logType)
            {
                case LOGTYPE.Normal:
                    Debug.Log("Duck_Log_Game => " + log);
                    break;
                case LOGTYPE.Warning:
                    Debug.LogWarning("Duck_Log_Game => " + log);
                    break;
                case LOGTYPE.Error:
                    Debug.LogError("Duck_Log_Game => " + log);
                    break;
            }
        }

        public static void LogGame(bool log, LOGTYPE logType = LOGTYPE.Normal)
        {
            switch (logType)
            {
                case LOGTYPE.Normal:
                    Debug.Log("Duck_Log_Game => " + log);
                    break;
                case LOGTYPE.Warning:
                    Debug.LogWarning("Duck_Log_Game => " + log);
                    break;
                case LOGTYPE.Error:
                    Debug.LogError("Duck_Log_Game => " + log);
                    break;
            }
        }

        public static float Clamp0360(float eulerAngles)
        {
            float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
            if (result < 0)
            {
                result += 360f;
            }
            return result;
        }

        public static void LogFB(string titleName, string paraName, string paraValue)
        {
            LogGame("LOG FB : -->" + titleName + "<--" + "[" + paraName + "] => " + paraValue);
#if USE_FIREBASE
                if (FirebaseManager.AnalyticStatus != FirebaseStatus.Initialized)
                {
                    Debug.LogWarning("[Firebase] AnalyticStatus: " + FirebaseManager.AnalyticStatus);
                    return;
                }

                FirebaseAnalytics.LogEvent(titleName,
                    new Parameter[] {
                new Parameter(paraName, paraValue) });
                
                Debug.Log(titleName + ", " + paraName  + " : " + paraValue);
#endif

#if USE_APPSFLYER
                Dictionary<string, string> eventValues = new Dictionary<string, string>();
                eventValues.Add(paraName, paraValue);
                AppsFlyerSDK.AppsFlyer.sendEvent(titleName, eventValues);
#endif
        }

        public static void LogFB2(string titleName, string para1Name, string para1Value, string para2Name, string para2Value)
        {
#if USE_FIREBASE
                if (FirebaseManager.AnalyticStatus != FirebaseStatus.Initialized)
                {
                    Debug.LogWarning("[Firebase] AnalyticStatus: " + FirebaseManager.AnalyticStatus);
                    return;
                }

                FirebaseAnalytics.LogEvent(titleName,
                    new Parameter[] {
                new Parameter(para1Name, para1Value),
                new Parameter(para2Name, para2Value) });

                Debug.Log(titleName + ", " + para1Name  + " : " + para1Value + ", " + para2Name + " : " + para2Value);
        
#endif
        }

        public static void LogFB4(string titleName, string para1Name, string para1Value, string para2Name, int para2Value, string para3Name, string para3Value, string para4Name, int para4Value)
        {
#if USE_FIREBASE
                if (FirebaseManager.AnalyticStatus != FirebaseStatus.Initialized)
                {
                    Debug.LogWarning("[Firebase] AnalyticStatus: " + FirebaseManager.AnalyticStatus);
                    return;
                }

                FirebaseAnalytics.LogEvent(titleName,
                    new Parameter[] {
                new Parameter(para1Name, para1Value),
                new Parameter(para2Name, para2Value),
                new Parameter(para3Name, para3Value),
                new Parameter(para4Name, para4Value) });

                Debug.Log(titleName + ", " + para1Name  + " : " + para1Value + ", " + para2Name  + " : " + para2Value + ", " + para3Name + " : " + para3Value + ", " + para4Name + " : " + para4Value);
        
#endif
        }

        public static void LogLevelUpFBDefault(int level)
        {
#if USE_FIREBASE
            FirebaseManager.LogLevelUp(level, "");
#endif
        }

        public static void LogEarnVitualCurrencyFBDefault(int value, string name)
        {
#if USE_FIREBASE
           // FirebaseManager.LogEarnVirtualCurrency(value, name);
#endif
        }

        public static void LogSpentVitualCurrencyFBDefault(int value, string name)
        {
#if USE_FIREBASE
            //FirebaseManager.LogSpendVirtualCurrency(value, name);
#endif
        }

        public static void LogEvent(string name)
        {
            LogGame("LOG EVENT : -->" + name + "<--");
#if USE_FIREBASE
            FirebaseManager.LogEvent(name);
#endif
        }


        /// <summary>
        /// Counts the bounding box corners of the given RectTransform that are visible from the given Camera in screen space.
        /// </summary>
        /// <returns>The amount of bounding box corners that are visible from the Camera.</returns>
        /// <param name="rectTransform">Rect transform.</param>
        /// <param name="camera">Camera.</param>
        private static int CountRectCornersVisibleFrom(this RectTransform rectTransform, Camera camera)
        {
            Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height); // Screen space bounds (assumes camera renders across the entire screen)
            Vector3[] objectCorners = new Vector3[4];
            rectTransform.GetWorldCorners(objectCorners);

            int visibleCorners = 0;
            Vector3 tempScreenSpaceCorner; // Cached
            for (var i = 0; i < objectCorners.Length; i++) // For each corner in rectTransform
            {
                tempScreenSpaceCorner = camera.WorldToScreenPoint(objectCorners[i]); // Transform world space position of corner to screen space
                if (screenBounds.Contains(tempScreenSpaceCorner)) // If the corner is inside the screen
                {
                    visibleCorners++;
                }
            }
            return visibleCorners;
        }

        /// <summary>
        /// Determines if this RectTransform is fully visible from the specified camera.
        /// Works by checking if each bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
        /// </summary>
        /// <returns><c>true</c> if is fully visible from the specified camera; otherwise, <c>false</c>.</returns>
        /// <param name="rectTransform">Rect transform.</param>
        /// <param name="camera">Camera.</param>
        public static bool IsRectFullyVisibleFrom(this RectTransform rectTransform, Camera camera)
        {
            return CountRectCornersVisibleFrom(rectTransform, camera) == 4; // True if all 4 corners are visible
        }

        /// <summary>
        /// Determines if this RectTransform is at least partially visible from the specified camera.
        /// Works by checking if any bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
        /// </summary>
        /// <returns><c>true</c> if is at least partially visible from the specified camera; otherwise, <c>false</c>.</returns>
        /// <param name="rectTransform">Rect transform.</param>
        /// <param name="camera">Camera.</param>
        public static bool IsRectVisibleFrom(this RectTransform rectTransform, Camera camera)
        {
            return CountRectCornersVisibleFrom(rectTransform, camera) > 0; // True if any corners are visible
        }
        public static List<Transform> GetAllChildren(this Transform parent)
        {
            List<Transform> result = new List<Transform>();
            AddChildrenRecursive(parent, result);
            return result;
        }
        private static void AddChildrenRecursive(Transform current, List<Transform> list)
        {
            foreach (Transform child in current)
            {
                list.Add(child);
                AddChildrenRecursive(child, list);
            }
        }
        public static Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos)
        {
            //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            Vector2 movePos;

            //Convert the screenpoint to ui rectangle local point
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
            //Convert the local point to world point
            return movePos;
        }

        public static IEnumerator CountText(TextMeshProUGUI text, int targetValue, float duration, string addiionfront = null, string additionback = null)
        {
            float elapsed = 0f;
            int startValue = 0;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / duration);
                int currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, progress));
                text.text = currentValue.ToString();
                yield return null;
            }

            text.text = addiionfront + targetValue.ToString() + additionback; // Đảm bảo kết thúc là giá trị đúng
        }
    }
}
