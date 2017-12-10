namespace Day09.Parser
{
    internal abstract class GroupItem
    {
        public abstract void Accept(GroupItemVisitor groupItemVisitor);
    }
}
