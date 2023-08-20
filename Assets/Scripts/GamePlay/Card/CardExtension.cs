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

        public static IEnumerator DoRotation(this Transform transform, bool isNormalFace, float rotationSpeed, Action onComplete)
        {
            float t = 0;
            Quaternion originalRotation = isNormalFace ? Quaternion.Euler(0f, 0f, 0f) : Quaternion.Euler(0f, 180f, 0f);
            Quaternion desireRotation = isNormalFace ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);

            while (t < 1)
            {
                t += Time.deltaTime * rotationSpeed;
                transform.rotation = Quaternion.Slerp(originalRotation, desireRotation, t);
                yield return null;
            }

            onComplete?.Invoke();
        }

        public static bool IsRotationComplete(this Transform transform, Quaternion targetRotation, float tolerance = 0.1f)
        {
            return Quaternion.Angle(transform.rotation, targetRotation) < tolerance;
        }
    }
}