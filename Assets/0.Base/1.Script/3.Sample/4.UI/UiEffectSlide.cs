namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class UiEffectSlide : BaseEventTrigger   //Data Field
    {
        private bool isPerformance = false;
        private bool active = false;
        private float deltaTime = 0;
        private float animationCurveEndTime = 0;
        private Vector3 originLocalPosition = Vector3.zero;
        private Vector3 performanceLocalPosition = Vector3.zero;
        private Vector3 prevDestination = Vector3.zero;

        [SerializeField]
        private Image thisImage = null;
        [SerializeField]
        private float speed = 1;
        [SerializeField]
        private Vector3 moveDistance = Vector3.zero;
        [SerializeField]
        private AnimationCurve animationCurve = null;
    }

    public partial class UiEffectSlide : BaseEventTrigger       //Function Field
    {
        private void Start()
        {
            animationCurveEndTime = animationCurve[animationCurve.length - 1].time;
            thisImage.enabled = false;
            originLocalPosition = transform.localPosition;
            performanceLocalPosition = originLocalPosition + moveDistance;
            transform.localPosition = performanceLocalPosition;
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
            prevDestination = originLocalPosition;
            deltaTime = 0;
            isPerformance = true;
        }

        public override void Finish()
        {
            base.Finish();
            active = false;
            prevDestination = performanceLocalPosition;
            deltaTime = 0;
            isPerformance = true;
        }

        private void Performance()
        {
            deltaTime += Time.deltaTime * speed;

            transform.localPosition = Vector3.Lerp(transform.localPosition, prevDestination, animationCurve.Evaluate(deltaTime));
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