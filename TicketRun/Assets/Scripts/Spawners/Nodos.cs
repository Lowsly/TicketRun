using System.Collections.Generic;
public abstract class BTNode
{
    public abstract bool Execute();
}

public class ConditionNode : BTNode
{
    private System.Func<bool> condition;

    public ConditionNode(System.Func<bool> condition)
    {
        this.condition = condition;
    }

    public override bool Execute()
    {
        return condition();
    }
}

public class ActionNode : BTNode
{
    private System.Action action;

    public ActionNode(System.Action action)
    {
        this.action = action;
    }

    public override bool Execute()
    {
        action();
        return true;
    }
}

public class Selector : BTNode
{
    private List<BTNode> children;

    public Selector(List<BTNode> children)
    {
        this.children = children;
    }

    public override bool Execute()
    {
        foreach (var child in children)
        {
            if (child.Execute())
            {
                return true;
            }
        }
        return false;
    }
}

public class Sequence : BTNode
{
    private List<BTNode> children;

    public Sequence(List<BTNode> children)
    {
        this.children = children;
    }

    public override bool Execute()
    {
        foreach (var child in children)
        {
            if (!child.Execute())
            {
                return false;
            }
        }
        return true;
    }
}
