namespace EventSystem.Base
{
    public class TimerCondition : EventCondition
    {
        private float waitTime;

        public TimerCondition(float waitTime)
        {
            this.waitTime = waitTime;
        }

        public override void OnItemStart()
        {
            eventBase.SuspendEventItem(this,waitTime);
        }

        public override void OnResumeExecution()
        {
            EndEventItem();
        }
    }
}