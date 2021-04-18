namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class PopupEventTrigger : BaseEventTrigger  //Data Field
    {
        private bool isPerformance = false;
        private float deltaTime = 0;
        private float popupAlpha = 1;

        [SerializeField]
        private Image popupImge = null;
        [SerializeField]
        private Text popupText = null;
    }

    public partial class PopupEventTrigger : BaseEventTrigger  //Function Field
    {
        public override void Active()
        {
            base.Active();

            deltaTime = 0;
            popupAlpha = 1;
            isPerformance = true;
        }

        public override void Finish()
        {
            base.Finish();
        }

        private void Update()
        {
            if (isPerformance)
            {
                deltaTime += Time.deltaTime;

                if (popupImge != null)
                {
                    if (deltaTime < 1)
                    {
                        popupImge.color = new Color(
                            popupImge.color.r
                            , popupImge.color.g
                            , popupImge.color.b
                            , deltaTime);
                    }

                    if (deltaTime > 2)
                    {
                        popupAlpha -= Time.deltaTime;
                        popupImge.color = new Color(
                            popupImge.color.r
                            , popupImge.color.g
                            , popupImge.color.b
                            , popupAlpha);
                    }
                }

                if (popupText)
                {
                    if (deltaTime < 1)
                    {
                        popupText.color = new Color(
                            popupText.color.r
                            , popupText.color.g
                            , popupText.color.b
                            , deltaTime);
                    }

                    if (deltaTime > 2)
                    {
                        popupText.color = new Color(
                            popupText.color.r
                            , popupText.color.g
                            , popupText.color.b
                            , popupAlpha);
                    }

                }

                if (deltaTime > 2)
                {
                    popupAlpha -= Time.deltaTime;
                }
                if (deltaTime > 3)
                {
                    isPerformance = false;
                    popupAlpha = 1;
                    deltaTime = 0;
                    Finish();
                }

            }
        }
    }
}