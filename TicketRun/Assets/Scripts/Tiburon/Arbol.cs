using System.Collections.Generic;
using System;

public abstract class BehaviorNode {
    public abstract bool Execute(Enemy enemy);
}

public class SelectorNode : BehaviorNode {
    private List<BehaviorNode> children = new List<BehaviorNode>();

    public override bool Execute(Enemy enemy) {
        foreach (var child in children) {
            if (child.Execute(enemy)) {
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

    public override bool Execute(Enemy enemy) {
        foreach (var child in children) {
            if (!child.Execute(enemy)) {
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
    private Func<Enemy, bool> condition;

    public ConditionalNode(Func<Enemy, bool> condition) {
        this.condition = condition;
    }

    public override bool Execute(Enemy enemy) {
        return condition(enemy);
    }
}

public class ActionNode : BehaviorNode {
    private Func<Enemy, bool> action;

    public ActionNode(Func<Enemy, bool> action) {
        this.action = action;
    }

    public override bool Execute(Enemy enemy) {
        return action(enemy);
    }
}
