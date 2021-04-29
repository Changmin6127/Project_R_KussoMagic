namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class MatchCollisionEventTrigger : BaseEventTrigger   //Data Field
    {
        [SerializeField]
        private int matchNumber = 0;
        [SerializeField]
        private MatchGroupEventTrigger matchGroupEventTrigger = null;
    }

    public partial class MatchCollisionEventTrigger : BaseEventTrigger   //Function Field
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            MatchObject matchObject = collision.collider.GetComponent<MatchObject>();

            if (matchObject != null)
            {
                if (matchNumber == matchObject.GetMatchNumber())
                {
                    Active();
                    if (matchGroupEventTrigger != null)
                        matchGroupEventTrigger.Active();
                }
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            MatchObject matchObject = collision.collider.GetComponent<MatchObject>();

            if (matchObject != null)
            {
                if (matchNumber == matchObject.GetMatchNumber())
                {
                    Active();
                    if (matchGroupEventTrigger != null)
                        matchGroupEventTrigger.Active();
                }
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            MatchObject matchObject = collision.collider.GetComponent<MatchObject>();

            if (matchObject != null)
            {
                if (matchNumber == matchObject.GetMatchNumber())
                {
                    Finish();
                    if (matchGroupEventTrigger != null)
                        matchGroupEventTrigger.Finish();
                }
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            MatchObject matchObject = collision.collider.GetComponent<MatchObject>();

            if (matchObject != null)
            {
                if (matchNumber == matchObject.GetMatchNumber())
                {
                    Finish();
                    if (matchGroupEventTrigger != null)
                        matchGroupEventTrigger.Finish();
                }
            }
        }

    }
}