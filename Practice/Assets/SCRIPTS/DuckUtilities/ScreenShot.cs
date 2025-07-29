// using DuckGame.Ultilities;
// using Sirenix.OdinInspector;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ScreenShot : MonoBehaviour
// {
//     [Button] 
//     void TakeScreenShot()
//     {
//         ScreenCapture.CaptureScreenshot("ScreenShot.png");
//     }

//     [Button]
//     void MultipleScreenShot()
//     {
//         StartCoroutine(IScreenShotMultiple());
//     }

//     IEnumerator IScreenShotMultiple()
//     {
//         for (int i = 0; i < transform.childCount; i++)
//         {
//             if (i > 0)
//             {
//                 transform.GetChild(i - 1).gameObject.SetActive(false);
//             }
//             transform.GetChild(i).gameObject.SetActive(true);
//             if (transform.GetChild(i).GetComponent<VehicleUpgrade>())
//                 transform.GetChild(i).GetComponent<VehicleUpgrade>().ChangeVehicleBodyColor(DuckGame.Ultilities.DataManager.Instance.allColorDics[DuckGame.Ultilities.DataManager.Instance.vehicleUnlockData.vehicleUnlockDics[transform.GetChild(i).name].defaultColor], false);
//             yield return new WaitForSeconds(0.5f);
//             ScreenCapture.CaptureScreenshot(transform.GetChild(i).name + ".png");
//             print("ScreenShot Taken!!!");
//             yield return new WaitForSeconds(0.15f);
//         }
//     }
// }
