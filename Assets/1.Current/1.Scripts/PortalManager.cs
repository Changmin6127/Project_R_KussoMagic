using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PortalManager : MonoBehaviour  //Data Field
{
    private List<Portal> portals = new List<Portal>();
}

public partial class PortalManager : MonoBehaviour  //Function Field
{
    public void SignupPortal(Portal _portal)
    {
        portals.Add(_portal);
    }
    public void AllActive(Portal _deactivePortal)
    {
        foreach (Portal portal in portals)
        {
            if(portal != _deactivePortal)
            {
                portal.Deactive();
            }
        }
    }
}