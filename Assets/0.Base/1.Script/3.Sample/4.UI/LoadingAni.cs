namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class LoadingAni : MonoBehaviour     //Data Field
    {
        private bool isActive = false;
        private float deltaTime = 0;
        private float count = 0;

        [SerializeField]
        private List<GameObject> textAni = new List<GameObject>();
    }

    public partial class LoadingAni : MonoBehaviour     //Function Field
    {
        private void OnEnable()
        {
            isActive = true;
        }

        private void Update()
        {
            if (isActive)
            {
                deltaTime += Time.deltaTime;

                if (deltaTime > 0.3f)
                {
                    deltaTime = 0;
                    count++;
                    if (count > 3)
                        count = 0;
                    for (int index = 0; index < textAni.Count; index++)
                    {
                        if (count == index)
                            textAni[index].SetActive(true);
                        else
                            textAni[index].SetActive(false);
                    }
                }
            }
        }
    }
}