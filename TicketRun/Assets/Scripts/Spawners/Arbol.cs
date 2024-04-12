public class SimpleBehaviorTree
{
    private ConditionNode condition;
    private ActionNode action;

    public SimpleBehaviorTree(ConditionNode condition, ActionNode action)
    {
        this.condition = condition;
        this.action = action;
    }

    public void Tick()
    {
        if (condition.Execute())  // If the condition is true
        {
            action.Execute();  // Perform the action
        }
    }
}
