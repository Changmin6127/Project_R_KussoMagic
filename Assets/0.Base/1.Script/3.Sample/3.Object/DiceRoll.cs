namespace Anvil
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class DiceRoll : BaseEventTrigger   //Data Field
    {
        private enum GaugeType { None, Up, Down }
        private GaugeType gaugeType = GaugeType.None;
        private bool isActive = false;
        private float deltaTime = 0;
        private float forceGauge = 0;
        private Rigidbody thisRigidbody;
        private Vector3 originPosition = Vector3.zero;
        private Quaternion originRotation = Quaternion.identity;

        [SerializeField]
        private Image forceGaugeImage = null;
        [SerializeField]
        private GameObject forceGaugeGameObject = null;
        [SerializeField]
        private List<GameObject> wallColliders = new List<GameObject>();
    }

    public partial class DiceRoll : BaseEventTrigger   //Main Function Field
    {
        private void Start()
        {
            thisRigidbody = GetComponent<Rigidbody>();
            originPosition = transform.position;
            originRotation = transform.rotation;
        }

        private void Update()
        {
            if (isActive)
                ForceSelect();
        }
    }

    public partial class DiceRoll : BaseEventTrigger   //Override Function Field
    {
        public void Initialize()
        {
            for (int index = 0; index < wallColliders.Count; index++)
            {
                wallColliders[index].SetActive(true);
            }
            thisRigidbody.velocity = Vector3.zero;
            thisRigidbody.angularVelocity = Vector3.zero;
            thisRigidbody.useGravity = false;
            transform.position = originPosition;
            transform.rotation = originRotation;
        }

        public void MovingFInish()
        {
            for (int index = 0; index < wallColliders.Count; index++)
            {
                wallColliders[index].SetActive(false);
            }
            thisRigidbody.velocity = Vector3.zero;
            thisRigidbody.angularVelocity = Vector3.zero;

        }

        public override void Active()
        {
            base.Active();

            gaugeType = GaugeType.Up;
            deltaTime = 0;
            isActive = true;
            forceGaugeGameObject.SetActive(true);
        }

        public override void Finish()
        {
            base.Finish();

            isActive = false;
            gaugeType = GaugeType.None;
            forceGauge *= 2000;
            forceGauge += 700;
            //float prevValue = Random.Range(-0.5f, 0.5f);
            //thisRigidbody.AddForce(transform.right * prevValue * forceGauge);
            //thisRigidbody.AddForce(transform.up * forceGauge);
            thisRigidbody.AddForce(transform.forward * forceGauge);
            forceGaugeImage.fillAmount = 0 / 1;
            forceGaugeGameObject.SetActive(false);
        }
    }

    public partial class DiceRoll : BaseEventTrigger   //Property Function Field
    {
        private void ForceSelect()
        {
            deltaTime += Time.deltaTime;

            float gaugeColor = 1;
            gaugeColor -= forceGauge;
            forceGaugeImage.color = new Color(forceGaugeImage.color.r, gaugeColor, forceGaugeImage.color.b);

            switch (gaugeType)
            {
                case GaugeType.Up:
                    forceGauge = deltaTime;
                    break;
                case GaugeType.Down:
                    float prevValue = 1;
                    prevValue -= deltaTime;
                    forceGauge = prevValue;
                    break;
                default: break;
            }

            if (forceGauge >= 1)
            {
                gaugeType = GaugeType.Down;
                deltaTime = 0;
            }
            if (forceGauge <= 0)
            {
                gaugeType = GaugeType.Up;
                deltaTime = 0;
            }

            ForceGauge();
        }

        private void ForceGauge()
        {
            forceGaugeImage.fillAmount = forceGauge / 1;
        }
    }
}