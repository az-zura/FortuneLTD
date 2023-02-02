using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class IsWorkingHourCondition : ActionExecuteCondition
{
    
    private GameLoop gameLoop;

    public IsWorkingHourCondition(GameLoop gameLoop)
    {
        this.gameLoop = gameLoop;
    }

    public override bool EvaluateCondition()
    {
        return gameLoop.IsWorkingTime();
    }
}
