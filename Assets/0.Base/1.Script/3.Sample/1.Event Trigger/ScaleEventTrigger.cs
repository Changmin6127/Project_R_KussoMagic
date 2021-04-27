using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class ScaleEventTrigger : MonoBehaviour  //Data Field
{
    private bool isActive = false;
    private float deltaTime = 0;
    private float endAnimationCurve = 0;
    private Vector3 originScale;
    private Vector3 startScale;
    private Vector3 finishScale;
    private UnityEvent destinationEvent;

    [SerializeField]
    private AnimationCurve animationCurve;
    [SerializeField]
    private float time = 0;
    [SerializeField]
    private Vector3 destinationScale = Vector3.zero;
    [SerializeField]
    private UnityEvent activeEvent;
    [SerializeField]
    private UnityEvent finishEvent;
    [SerializeField]
    private UnityEvent reverseEvent;
    [SerializeField]
    private UnityEvent reverseFinishEvent;
}

public partial class ScaleEventTrigger : MonoBehaviour  //Main Function Field
{
    private void Start()
    {
        endAnimationCurve = animationCurve[animationCurve.length - 1].time;
        originScale = transform.localScale;
    }

    private void Update()
    {
        if (isActive)
            Progress();
    }
}

public partial class ScaleEventTrigger : MonoBehaviour  //Property Function Field
{
    public void Active()
    {
        startScale = originScale;
        finishScale = destinationScale;
        activeEvent?.Invoke();
        destinationEvent = finishEvent;
        deltaTime = 0;
        isActive = true;
    }

    public void Reverse()
    {
        startScale = destinationScale;
        finishScale = originScale;
        reverseEvent?.Invoke();
        destinationEvent = reverseFinishEvent;
        deltaTime = 0;
        isActive = true;
    }
    private void Progress()
    {
        deltaTime += Time.deltaTime / time;

        transform.localScale = Vector3.Lerp(startScale, finishScale, animationCurve.Evaluate(deltaTime));

        if(deltaTime > endAnimationCurve)
        {
            isActive = false;
            destinationEvent?.Invoke();
        }
    }
}