using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class EnermyManager : MonoBehaviour  //Data Field
{
    private int dieCount = 0;

    [SerializeField]
    private List<Enermy> enermies = new List<Enermy>();
    [SerializeField]
    private UnityEvent dieFinishEvent;
}

public partial class EnermyManager : MonoBehaviour  //Function Field
{
    private void Start()
    {
        foreach (Enermy enermy in enermies)
        {
            enermy.enermyManager = this;
        }
    }

    public void DieCountUp()
    {
        dieCount++;
        if(dieCount == enermies.Count)
        {
            dieFinishEvent?.Invoke();
        }
    }
}