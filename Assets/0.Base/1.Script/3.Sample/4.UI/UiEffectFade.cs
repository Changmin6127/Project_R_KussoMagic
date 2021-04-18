namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class UiEffectFade : BaseEventTrigger   //Data Field
    {
        private bool isPerformance = false;
        private bool active = false;
        private float deltaTime = 0;
        private float animationCurveEndTime = 0;
        private Color prevDestination = Color.white;

        [SerializeField]
        private bool isStart = false;
        [SerializeField]
        private Image thisImage = null;
        [SerializeField]
        private float speed = 1;
        [SerializeField]
        private AnimationCurve animationCurve = null;
    }

    public partial class UiEffectFade : BaseEventTrigger       //Function Field
    {
        private void Start()
        {
            animationCurveEndTime = animationCurve[animationCurve.length - 1].time;
            thisImage.enabled = false;
            thisImage.color = new Color(thisImage.color.r, thisImage.color.g, thisImage.color.b, 0);
            if (isStart)
                Active();
        }

        private void Update()
        {
            if (isPerformance)
                Performance();
        }

        public override void Active()
        {
            base.Active();
            active = true;
            thisImage.enabled = true;
            prevDestination = new Color(thisImage.color.r, thisImage.color.g, thisImage.color.b, 1);
            deltaTime = 0;
            isPerformance = true;
        }

        public override void Finish()
        {
            base.Finish();
            active = false;
            prevDestination = new Color(thisImage.color.r, thisImage.color.g, thisImage.color.b, 0);
            deltaTime = 0;
            isPerformance = true;
        }

        private void Performance()
        {
            deltaTime += Time.deltaTime * speed;

            thisImage.color = Color.Lerp(thisImage.color, prevDestination, animationCurve.Evaluate(deltaTime));
            if (active)
            {
                if (deltaTime > animationCurveEndTime)
                {
                    isPerformance = false;
                    deltaTime = 0;
                }
            }
            else
            {
                if (deltaTime > animationCurveEndTime * 0.6f)
                {
                    isPerformance = false;
                    deltaTime = 0;
                    thisImage.enabled = false;
                }
            }

        }
    }
}