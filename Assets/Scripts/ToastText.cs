using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Toast
{
    public class ToastText : MonoBehaviour
    {
        [SerializeField] private TMP_Text toastText;

        public void ShowToast(string text="Card Miss Matched", float duration=3f)
        {
            StopCoroutine(ShowToastCor(text, duration, toastText));
            StartCoroutine(ShowToastCor(text, duration, toastText));
        }

        public void ShowToast(string text,
            float duration, TMP_Text txt)
        {
            StopCoroutine(ShowToastCor(text, duration, txt));
            StartCoroutine(ShowToastCor(text, duration, txt));
        }

        private IEnumerator ShowToastCor(string text,
            float duration, TMP_Text txt)
        {
            txt.text = text;
            txt.enabled = true;
            txt.color = Color.white;
            //Fade in
            yield return FadeInAndOut(txt, true, 0.5f);

            //Wait for the duration
            float counter = 0;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                yield return null;
            }

            //Fade out
            yield return FadeInAndOut(txt, false, 0.5f);

            txt.enabled = false;
        }

        private static IEnumerator FadeInAndOut(Graphic targetText, bool fadeIn, float duration)
        {
            //Set Values depending on if fadeIn or fadeOut
            float a, b;
            if (fadeIn)
            {
                a = 0f;
                b = 1f;
            }
            else
            {
                a = 1f;
                b = 0f;
            }

            var currentColor = targetText.color;
            var counter = 0f;

            while (counter < duration)
            {
                counter += Time.deltaTime;
                var alpha = Mathf.Lerp(a, b, counter / duration);
                targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                yield return null;
            }
        }
    }
}