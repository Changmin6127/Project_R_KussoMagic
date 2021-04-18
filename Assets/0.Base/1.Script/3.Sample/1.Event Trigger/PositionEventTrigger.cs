namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class PositionEventTrigger : BaseEventTrigger   //Data Field
    {
        private bool isPerformance = false;
        private bool reverse = false;
        private float deltaTime = 0;
        private float animationCurveEndTime = 0;
        private Vector3 originPosition;
        private Vector3 prevDestination;

        [SerializeField]
        private UnityEvent reverseActive = null;
        [SerializeField]
        private UnityEvent reverseFinish = null;
        [SerializeField]
        private Transform targetTransform = null;
        [SerializeField]
        private AnimationCurve animationCurve = null;
        [SerializeField]
        private bool freezeOriginPosition = false;
        [SerializeField]
        private bool loop = false;
        [SerializeField]
        private bool startActive = false;
        [SerializeField]
        [Header("Transform값이 null일경우 Vector3로 이동")]
        private Transform destinationTarget = null;
        [SerializeField]
        [Header("Vector3 값 만큼 이동")]
        private Vector3 destination = Vector3.zero;

    }

    public partial class PositionEventTrigger : BaseEventTrigger   //Main Function Field
    {
        private void Start()
        {
            animationCurveEndTime = animationCurve[animationCurve.length - 1].time;
            originPosition = targetTransform.position;

            if (startActive)
                Active();
        }

        private void Update()
        {
            if (isPerformance)
                Performance();
        }
    }

    public partial class PositionEventTrigger : BaseEventTrigger   //Override Function Field
    {
        public override void Active()
        {
            base.Active();
            if (freezeOriginPosition)
                originPosition = targetTransform.position;
            if (destinationTarget != null)
                prevDestination = destinationTarget.position;
            else
            {
                prevDestination = targetTransform.position;
                prevDestination += destination;
            }
            deltaTime = 0;
            isPerformance = true;
        }

        public override void Finish()
        {
            base.Finish();
        }
    }

    public partial class PositionEventTrigger : BaseEventTrigger   //Property Function Field
    {
        public void ReverseActive()
        {
            reverseActive?.Invoke();
            reverse = true;
            prevDestination = originPosition;
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
            targetTransform.position = Vector3.Lerp(targetTransform.position, prevDestination, animationCurve.Evaluate(deltaTime));

            if (deltaTime > animationCurveEndTime && loop == false)
            {
                isPerformance = false;
                deltaTime = 0;
                if (reverse == false)
                    Finish();
                else
                    ReverseFinish();
            }

            if (loop)
            {
                prevDestination = targetTransform.localEulerAngles;
                prevDestination += destination;
            }
        }
    }
}