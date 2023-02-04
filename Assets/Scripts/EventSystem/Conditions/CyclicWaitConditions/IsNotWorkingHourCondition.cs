using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class IsNotWorkingHourCondition : ActionExecuteCondition
{
    
    private GameLoop gameLoop;

    public IsNotWorkingHourCondition(GameLoop gameLoop)
    {
        this.gameLoop = gameLoop;
    }

    public override bool EvaluateCondition()
    {
        return !gameLoop.IsWorkingTime();
    }
}
