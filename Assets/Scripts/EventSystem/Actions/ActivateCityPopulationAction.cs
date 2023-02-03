using System.Collections;
using System.Collections.Generic;
using EventSystem.Base;
using UnityEngine;

public class ActivateCityPopulationAction : ActionBase
{

    private PopulateCity populateCity;
    private bool suspend;

    public ActivateCityPopulationAction(PopulateCity populateCity, bool suspend)
    {
        this.populateCity = populateCity;
        this.suspend = suspend;
    }

    public override void OnItemStart()
    {
        populateCity.setSuspended(suspend);
        EndEventItem();
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }
}
