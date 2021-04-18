namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class EffectDeleteTime : MonoBehaviour  //Data Field
    {
        private float deltaTime = 0;
        private float effectAlpha = 1;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;
    }

    public partial class EffectDeleteTime : MonoBehaviour  //Function Field
    {
        private void OnEnable()
        {
            deltaTime = 0;
            effectAlpha = 1;
        }

        private void Update()
        {
            deltaTime += Time.deltaTime;
            effectAlpha -= Time.deltaTime * 2.5f;

            spriteRenderer.color = new Color(255, 255, 255, effectAlpha);

            if (deltaTime > 0.5f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}