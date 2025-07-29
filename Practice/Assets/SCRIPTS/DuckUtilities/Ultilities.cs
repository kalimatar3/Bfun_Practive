using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DuckGame.Ultilities
{
    public static class Ultilities
    {
        // Returns the number with the greatest absolute value
        public static float MaxAbs(params float[] nums)
        {
            float result = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (Mathf.Abs(nums[i]) > Mathf.Abs(result))
                {
                    result = nums[i];
                }
            }

            return result;
        }

        // Returns the topmost parent of a Transform with a certain component
        public static T GetTopmostParentComponent<T>(this Transform tr) where T : Component
        {
            T getting = null;

            while (tr.parent != null)
            {
                if (tr.parent.GetComponent<T>() != null)
                {
                    getting = tr.parent.GetComponent<T>();
                }

                tr = tr.parent;
            }

            return getting;
        }


        public static float CalculateAngle(Quaternion transformA, Quaternion transformB)
        {

            var forwardA = transformA * Vector3.forward;
            var forwardB = transformB * Vector3.forward;

            float angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
            float angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;

            var angleDiff = Mathf.DeltaAngle(angleA, angleB);

            return angleDiff;

        }

        /// <summary>
        /// Calculates the approach angle of an object towrads another object
        /// </summary>
        /// <param name="forward"></param>
        /// <param name="targetDirection"></param>
        /// <param name="up"></param>
        /// <returns></returns>
        public static float ChaseAngle(Vector3 forward, Vector3 targetDirection, Vector3 up)
        {
            // Calculate the approach angle
            float approachAngle = Vector3.Dot(Vector3.Cross(up, forward), targetDirection);

            // If the angle is higher than 0, we approach from the left ( so we must rotate right )
            if (approachAngle > 0f)
            {
                return 1f;
            }
            else if (approachAngle < 0f) //Otherwise, if the angle is lower than 0, we approach from the right ( so we must rotate left )
            {
                return -1f;
            }
            else // Otherwise, we are within the angle range so we don't need to rotate
            {
                return 0f;
            }
        }

        public static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, Vector2 focusPoint)
        {
            Vector2 contentSize = scrollView.content.rect.size;
            Vector2 viewportSize = ((RectTransform)scrollView.content.parent).rect.size;
            Vector2 contentScale = scrollView.content.localScale;

            contentSize.Scale(contentScale);
            focusPoint.Scale(contentScale);

            Vector2 scrollPosition = scrollView.normalizedPosition;
            if (scrollView.horizontal && contentSize.x > viewportSize.x)
                scrollPosition.x = Mathf.Clamp01((focusPoint.x - viewportSize.x * 0.5f) / (contentSize.x - viewportSize.x));
            if (scrollView.vertical && contentSize.y > viewportSize.y)
                scrollPosition.y = Mathf.Clamp01((focusPoint.y - viewportSize.y * 0.5f) / (contentSize.y - viewportSize.y));

            return scrollPosition;
        }

        public static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, RectTransform item)
        {
            Vector2 itemCenterPoint = scrollView.content.InverseTransformPoint(item.transform.TransformPoint(item.rect.center));

            Vector2 contentSizeOffset = scrollView.content.rect.size;
            contentSizeOffset.Scale(scrollView.content.pivot);

            return scrollView.CalculateFocusedScrollPosition(itemCenterPoint + contentSizeOffset);
        }

        public static void FocusAtPoint(this ScrollRect scrollView, Vector2 focusPoint)
        {
            scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(focusPoint);
        }

        public static void FocusOnItem(this ScrollRect scrollView, RectTransform item)
        {
            scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(item);
        }

        private static IEnumerator LerpToScrollPositionCoroutine(this ScrollRect scrollView, Vector2 targetNormalizedPos, float speed)
        {
            Vector2 initialNormalizedPos = scrollView.normalizedPosition;

            float t = 0f;
            while (t < 1f)
            {
                scrollView.normalizedPosition = Vector2.LerpUnclamped(initialNormalizedPos, targetNormalizedPos, 1f - (1f - t) * (1f - t));

                yield return null;
                t += speed * Time.unscaledDeltaTime;
            }

            scrollView.normalizedPosition = targetNormalizedPos;
        }

        public static IEnumerator FocusAtPointCoroutine(this ScrollRect scrollView, Vector2 focusPoint, float speed)
        {
            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(focusPoint), speed);
        }

        public static IEnumerator FocusOnItemCoroutine(this ScrollRect scrollView, RectTransform item, float speed)
        {
            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(item), speed);
        }
    }
}