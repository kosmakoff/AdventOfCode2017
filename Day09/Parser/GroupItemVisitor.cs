namespace Day09.Parser
{
    internal abstract class GroupItemVisitor
    {
        public abstract void Visit(Group group);

        public abstract void Visit(Garbage garbage);
    }
}
