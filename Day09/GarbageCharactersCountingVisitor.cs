using Day09.Parser;

namespace Day09
{
    internal class GarbageCharactersCountingVisitor : GroupItemVisitor
    {
        public int CharactersCount { get; private set; } = 0;

        public override void Visit(Group @group)
        {
            foreach (var groupItem in @group.Items)
            {
                groupItem.Accept(this);
            }
        }

        public override void Visit(Garbage garbage)
        {
            CharactersCount += garbage.Content.Length;
        }
    }
}
