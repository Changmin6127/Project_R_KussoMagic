using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private Color _color = Color.white;
    [SerializeField]
    private float _radius = 0.1f;

    void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }
#endif
}
