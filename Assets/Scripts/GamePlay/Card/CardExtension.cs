using UnityEngine;
using System;
using System.Collections;
using CardModel;
namespace GameCard
{
    public static class CardExtension
    {
        // Extension method to flip a card with onComplete callback
        public static IEnumerator FlipWithCallback(this Card card, Action flip, Action onComplete)
        {
            flip?.Invoke();// Start the flip animation

            while (!card.IsFliped) // Wait for the card to finish flipping
            {
                yield return null;
            }

            // Card has finished flipping, invoke the onComplete callback
            onComplete?.Invoke();
        }

        public static IEnumerator DoRotateSpecific(this Transform transform, float rotationSpeed=1f, Action onStart = null, Action onMiddle = null, Action onComplete = null)
        {
            float t = 0;
            bool isMiddleCalled = false;
           float rotationDestination = 180;
            Quaternion originalRotation = Quaternion.Euler(0f, 0, 0f);
            Quaternion desireRotation = Quaternion.Euler(0f, rotationDestination, 0f);

            onStart?.Invoke();
            while (t < 1)
            {
                t += Time.deltaTime * rotationSpeed;
                transform.rotation = Quaternion.Slerp(originalRotation, desireRotation, t);

                //if (t >= 0.5f && t - Time.deltaTime < 0.5f)

                //if (t >= 0.5f && t < 0.5f + Time.deltaTime)
                if (t >= 0.5f && !isMiddleCalled)
                {
                    isMiddleCalled = true;
                    // You are at the middle of the rotation, trigger the event here
                    onMiddle?.Invoke();
                }
                yield return null;
            }
            // reset and prevention miss leading calls
            t = 0;
            onComplete?.Invoke();

        }

        public static IEnumerator DoRotationNormal(this Transform transform,  float rotationSpeed=1f, Action onStart = null, Action onMiddle = null, Action onComplete = null)
        {
            float t = 0;
            bool isMiddleCalled = false;
            float rotationDestination = 0;
            Quaternion originalRotation = Quaternion.Euler(0f, 180, 0f);
            Quaternion desireRotation = Quaternion.Euler(0f, rotationDestination, 0f);

            onStart?.Invoke();
            while (t < 1)
            {
                t += Time.deltaTime * rotationSpeed;
                transform.rotation = Quaternion.Slerp(originalRotation, desireRotation, t);

                //if (t >= 0.5f && t - Time.deltaTime < 0.5f)

                //if (t >= 0.5f && t < 0.5f + Time.deltaTime)
                if (t >= 0.5f && !isMiddleCalled)
                {
                    isMiddleCalled = true;
                    // You are at the middle of the rotation, trigger the event here
                    onMiddle?.Invoke();
                }
                yield return null;
            }
            // reset and prevention miss leading calls
            t = 0;
            onComplete?.Invoke();

        }

        public static bool IsRotationComplete(this Transform transform, Quaternion targetRotation, float tolerance = 0.1f)
        {
            return Quaternion.Angle(transform.rotation, targetRotation) < tolerance;
        }

        public static IEnumerator DoScale(this Transform transform, Vector3 rotationDestination, float rotationSpeed, Action onStart = null, Action onMiddle = null, Action onComplete = null)
        {
            float t = 0;
            bool isMiddleCalled = false;
            Vector3 originalScale = transform.localScale*0.5f;

            Vector3 desireScale = rotationDestination;

            onStart?.Invoke();
            while (t < 1)
            {
                t += Time.deltaTime * rotationSpeed;
                transform.localScale = Vector3.Lerp(originalScale, desireScale, t);

                //if (t >= 0.5f && t - Time.deltaTime < 0.5f)

                //if (t >= 0.5f && t < 0.5f + Time.deltaTime)
                if (t >= 0.5f && !isMiddleCalled)
                {
                    isMiddleCalled = true;
                    // You are at the middle of the rotation, trigger the event here
                    onMiddle?.Invoke();
                }
                yield return null;
            }
            // reset and prevention miss leading calls
            t = 0;
            onComplete?.Invoke();

        }

    }
}