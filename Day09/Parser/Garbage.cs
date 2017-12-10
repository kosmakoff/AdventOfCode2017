namespace Day09.Parser
{
    internal sealed class Garbage : GroupItem
    {
        public string Content { get; }

        public Garbage(string content)
        {
            Content = content;
        }

        public override string ToString()
        {
            return $"<{Content}>";
        }

        public override void Accept(GroupItemVisitor groupItemVisitor)
        {
            groupItemVisitor.Visit(this);
        }
    }
}
