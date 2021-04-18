namespace Anvil
{
    using UnityEngine;

    public class DirectionPlane : MonoBehaviour
    {
        private void Update()
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

            float rayLength;

            if (GroupPlane.Raycast(cameraRay, out rayLength))

            {

                Vector3 pointTolook = cameraRay.GetPoint(rayLength);

                transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

            }
        }
    }

}