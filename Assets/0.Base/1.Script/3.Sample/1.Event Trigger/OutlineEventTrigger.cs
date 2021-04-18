namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class OutlineEventTrigger : BaseEventTrigger     //Data Field
    {
        private bool isActive = false;
        private float deltaTime = 0;
        private float animationCurveEndTime;

        [SerializeField]
        private SpriteRenderer outlineEffect = null;
        [SerializeField]
        private AnimationCurve animationCurve = null;
        [SerializeField]
        private bool startActive = false;
        [SerializeField]
        private float performanceTime = 3;
    }

    public partial class OutlineEventTrigger : BaseEventTrigger     //Override Function Field
    {
        public override void Active()
        {
            base.Active();
            deltaTime = 0;
            isActive = true;
        }

        public override void Finish()
        {
            base.Finish();
            isActive = false;
            deltaTime = 0;
        }
    }

    public partial class OutlineEventTrigger : BaseEventTrigger     //Main Function Field
    {
        private void Start()
        {
            animationCurveEndTime = animationCurve[animationCurve.length - 1].time;

            if (startActive)
                Active();
        }

        private void Update()
        {
            if (isActive)
            {
                deltaTime += Time.deltaTime / performanceTime;

                outlineEffect.color = new Color(outlineEffect.color.r, outlineEffect.color.g, outlineEffect.color.b, animationCurve.Evaluate(deltaTime));

                if (deltaTime > animationCurveEndTime)
                {
                    deltaTime = 0;
                }
            }
        }
    }
}