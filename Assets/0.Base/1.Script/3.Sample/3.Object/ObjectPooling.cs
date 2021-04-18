namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class ObjectPooling : MonoBehaviour  //Data Field
    {
        private List<GameObject> objectPooling = new List<GameObject>();

        [SerializeField]
        private string objectName = string.Empty;
        [SerializeField]
        [Header("SceneObject 밑에 EmptyObject 추가하여 사용")]
        private GameObject targetObject = null;
        [SerializeField]
        private int addValue = 0;
    }

    public partial class ObjectPooling : MonoBehaviour  //Function Field
    {
        private void Start()
        {
            ObjectPoolAdd(addValue);
        }

        public string GetObjectName()
        {
            return objectName;
        }

        private void ObjectPoolAdd(int addValue)
        {
            for (int i = 0; i < addValue; i++)
            {
                GameObject objectPool = (GameObject)Instantiate(targetObject);
                objectPool.transform.parent = transform;
                objectPool.SetActive(false);
                objectPooling.Add(objectPool);
            }
        }

        public void ObjectAdd(Vector3 position)
        {
            foreach (GameObject objectPool in objectPooling)
            {
                if (objectPool.activeSelf == false)
                {
                    objectPool.transform.position = position;
                    objectPool.SetActive(true);
                    break;
                }
            }
        }
        public void ObjectAdd(Transform transform)
        {
            foreach (GameObject objectPool in objectPooling)
            {
                if (objectPool.activeSelf == false)
                {
                    objectPool.transform.position = transform.position;
                    objectPool.SetActive(true);
                    break;
                }
            }
        }
    }
}