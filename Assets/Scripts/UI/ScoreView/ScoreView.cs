using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ingosstrakh.UI.ScoreView
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textTMP;
        [SerializeField] private TextMeshProUGUI textOutlineTMP;
        [SerializeField] private float offsetPerChar = 50f;

        [Header("CounterAnimations")] 
        [SerializeField] private AnimationCurve easeOutQuad;
        [SerializeField] private AnimationCurve easeOutBack;
        
        [Header("StarAnimation")]
        [SerializeField] private Animator starAnimator;
        [SerializeField] private string showTrigger;
        private bool isAnimationPlaying;
        private Coroutine CounterScaleAnimationCoroutine;
        private Coroutine CounterIncrementAnimationCoroutine;
        
        public void SetScore(int current, int delta)
        {
            if (!isAnimationPlaying)
            {
                starAnimator.SetTrigger(showTrigger);
                isAnimationPlaying = true;
                if (CounterScaleAnimationCoroutine != null)
                {
                    StopCoroutine(CounterScaleAnimationCoroutine);
                }
                CounterScaleAnimationCoroutine = StartCoroutine(CounterScaleAnimationProcess());
                if (CounterIncrementAnimationCoroutine != null)
                {
                    StopCoroutine(CounterIncrementAnimationCoroutine);
                }
                CounterIncrementAnimationCoroutine = StartCoroutine(CounterIncrementAnimationProcess(current, delta));
            }
        }

        public void SetScoreSilent(int score)
        {
            SetTextRect(score.ToString());
            textTMP.text = score.ToString();
            textOutlineTMP.text = score.ToString();
        }

        private IEnumerator CounterScaleAnimationProcess()
        {
            textOutlineTMP.transform.localScale = Vector3.one;
            var evaluatedTime = 0f;
            while (evaluatedTime < 1f)
            {
                var scale = easeOutBack.Evaluate(evaluatedTime);
                textOutlineTMP.transform.localScale = new Vector3(scale, scale, 1);
                evaluatedTime += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            textOutlineTMP.transform.localScale = Vector3.one;
        }
        private IEnumerator CounterIncrementAnimationProcess(int current, int delta)
        {
            SetTextRect((current + delta).ToString());
            var key = easeOutQuad.keys[0];
            key.value = current;
            easeOutQuad.MoveKey(0, key);
            key = easeOutQuad.keys[1];
            key.value = current + delta;
            easeOutQuad.MoveKey(1, key);
            var evaluatedTime = 0f;
            while (evaluatedTime < 1f)
            {
                var number = Convert.ToInt32(easeOutQuad.Evaluate(evaluatedTime)).ToString();
                textOutlineTMP.text = number;
                textTMP.text = number;
                evaluatedTime += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        private void SetTextRect(string text)
        {
            if (text.Length > textOutlineTMP.text.Length)
            {
                textOutlineTMP.GetComponent<LayoutElement>().minWidth = text.Length * offsetPerChar;
            }
        }
        private void ResetAnimationTrigger()
        {
            starAnimator.ResetTrigger(showTrigger);
            isAnimationPlaying = false;
        }
    }
}