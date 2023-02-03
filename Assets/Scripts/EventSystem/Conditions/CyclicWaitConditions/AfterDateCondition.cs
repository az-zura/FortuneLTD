using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class AfterDateCondition : ActionExecuteCondition
{
    private float time;
    private GameLoop gameLoop;

    public AfterDateCondition(float time, GameLoop gameLoop)
    {
        this.time = time;
        this.gameLoop = gameLoop;
    }

    public override bool EvaluateCondition()
    {
        return gameLoop.GetTime() > time;
    }
}
