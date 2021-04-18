namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;

    public partial class UiEffectScale : BaseEventTrigger   //Data Field
    {
        private bool isPerformance = false;
        private bool active = false;
        private float deltaTime = 0;
        private float animationCurveEndTime = 0;
        private Vector3 originScale = Vector3.zero;
        private Vector3 prevDestination = Vector3.zero;

        [SerializeField]
        private Image thisImage = null;
        [SerializeField]
        private float speed = 1;
        [SerializeField]
        private AnimationCurve animationCurve = null;
    }

    public partial class UiEffectScale : BaseEventTrigger       //Function Field
    {
        private void Start()
        {
            animationCurveEndTime = animationCurve[animationCurve.length - 1].time;
            thisImage.enabled = false;
            originScale = transform.localScale;
            transform.localScale = Vector3.zero;
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
            prevDestination = originScale;
            deltaTime = 0;
            isPerformance = true;
        }

        public override void Finish()
        {
            base.Finish();
            active = false;
            prevDestination = Vector3.zero;
            deltaTime = 0;
            isPerformance = true;
        }

        private void Performance()
        {
            deltaTime += Time.deltaTime * speed;

            transform.localScale = Vector3.Lerp(transform.localScale, prevDestination, animationCurve.Evaluate(deltaTime));
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