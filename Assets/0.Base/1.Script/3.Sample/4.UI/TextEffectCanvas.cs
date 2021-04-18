namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class TextEffectCanvas : MonoBehaviour   //Data Field
    {
        [SerializeField]
        private Color textColor = default;
        [SerializeField]
        private float speed = 1;
        [SerializeField]
        private List<TextEffect> textList = new List<TextEffect>();
    }

    public partial class TextEffectCanvas : MonoBehaviour   //Function Field
    {
        private void Start()
        {
            for (int index = 0; index < textList.Count; index++)
            {
                textList[index].Initialize(textColor, speed);
            }
        }

        public void Active(string value)
        {
            foreach (TextEffect textPool in textList)
            {
                if (textPool.GetIsActive() == false)
                {
                    textPool.transform.SetAsLastSibling();
                    textPool.Active(value);
                    break;
                }
            }
        }

        public void Active(string value, Color color)
        {
            foreach (TextEffect textPool in textList)
            {
                if (textPool.GetIsActive() == false)
                {
                    textPool.transform.SetAsLastSibling();
                    textPool.Active(value, color);
                    break;
                }
            }
        }
    }
}