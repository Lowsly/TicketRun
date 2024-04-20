using System.Collections.Generic;
using System;
public interface IBehavioralEntity
{
    bool CheckForObstacles();
    void MoveStraight();
    void AvoidObstacle();
}

public abstract class BehaviorNode {
    public abstract bool Execute(IBehavioralEntity entity);
}

public class SelectorNode : BehaviorNode {
    private List<BehaviorNode> children = new List<BehaviorNode>();

    public override bool Execute(IBehavioralEntity entity) {
        foreach (var child in children) {
            if (child.Execute(entity)) {
                return true;
            }
        }
        return false;
    }

    public void AddChild(BehaviorNode node) {
        children.Add(node);
    }
}

public class SequenceNode : BehaviorNode {
    private List<BehaviorNode> children = new List<BehaviorNode>();

    public override bool Execute(IBehavioralEntity entity) {
        foreach (var child in children) {
            if (!child.Execute(entity)) {
                return false;
            }
        }
        return true;
    }

    public void AddChild(BehaviorNode node) {
        children.Add(node);
    }
}

public class ConditionalNode : BehaviorNode {
    private Func<IBehavioralEntity, bool> condition;

    public ConditionalNode(Func<IBehavioralEntity, bool> condition) {
        this.condition = condition;
    }

    public override bool Execute(IBehavioralEntity entity) {
        return condition(entity);
    }
}

public class ActionNode : BehaviorNode {
    private Func<IBehavioralEntity, bool> action;

    public ActionNode(Func<IBehavioralEntity, bool> action) {
        this.action = action;
    }

    public override bool Execute(IBehavioralEntity entity) {
        return action(entity);
    }
}
