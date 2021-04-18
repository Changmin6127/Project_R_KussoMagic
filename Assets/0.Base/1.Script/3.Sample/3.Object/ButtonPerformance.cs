namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class ButtonPerformance : BaseEventTrigger   //Data Field
    {
        private bool isPerformance = false;
        private bool reverse = false;
        private float deltaTime = 0;
        private float animationCurveEndTime = 0;
        private Vector3 originScale;
        private Vector3 prevDestination;

        [SerializeField]
        private UnityEvent reverseActive = null;
        [SerializeField]
        private UnityEvent reverseFinish = null;
        [SerializeField]
        private AnimationCurve animationCurve = null;

        private Vector3 destination = Vector3.zero;

    }

    public partial class ButtonPerformance : BaseEventTrigger   //Main Function Field
    {
        private void Start()
        {
            animationCurveEndTime = animationCurve[animationCurve.length - 1].time;
            originScale = transform.localScale;
            destination = originScale * 0.1f;
        }

        private void Update()
        {
            if (isPerformance)
                Performance();
        }
    }

    public partial class ButtonPerformance : BaseEventTrigger   //Override Function Field
    {
        public override void Active()
        {
            base.Active();

            reverse = false;
            prevDestination = originScale;
            prevDestination += destination;
            deltaTime = 0;
            isPerformance = true;
        }

        public override void Finish()
        {
            isPerformance = false;
            base.Finish();
        }
    }

    public partial class ButtonPerformance : BaseEventTrigger   //Property Function Field
    {
        public void ReverseActive()
        {
            reverseActive?.Invoke();
            reverse = true;
            prevDestination = originScale;
            deltaTime = 0;
            isPerformance = true;
        }

        public void ReverseFinish()
        {
            reverseFinish?.Invoke();
        }

        private void Performance()
        {
            deltaTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, prevDestination, animationCurve.Evaluate(deltaTime));

            if (deltaTime > animationCurveEndTime)
            {
                isPerformance = false;
                deltaTime = 0;
                if (reverse == false)
                    Finish();
                else
                    ReverseFinish();
            }
        }
    }
}