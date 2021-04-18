namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class CameraRaycast : MonoBehaviour  //Data Field
    {
    }

    public partial class CameraRaycast : MonoBehaviour  //Function Field
    {
        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(wp, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit)
                {
                    RayTargetObject touchPoint = hit.collider.GetComponent<RayTargetObject>();
                    if (touchPoint != null)
                        touchPoint.Active();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(wp, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit)
                {
                    RayTargetObject touchPoint = hit.collider.GetComponent<RayTargetObject>();
                    if (touchPoint != null)
                        touchPoint.Finish();
                }
            }
#endif
            if (Input.touchCount == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Ray2D ray = new Ray2D(wp, Vector2.zero);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                    if (hit)
                    {
                        RayTargetObject touchPoint = hit.collider.GetComponent<RayTargetObject>();
                        if (touchPoint != null)
                            touchPoint.Active();
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Ray2D ray = new Ray2D(wp, Vector2.zero);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                    if (hit)
                    {
                        RayTargetObject touchPoint = hit.collider.GetComponent<RayTargetObject>();
                        if (touchPoint != null)
                            touchPoint.Finish();
                    }
                }
            }

        }
    }
}