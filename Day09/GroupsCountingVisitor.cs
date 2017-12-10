using Day09.Parser;

namespace Day09
{
    internal sealed class GroupsCountingVisitor : GroupItemVisitor
    {
        private int _currentLevel = 0;
        public int GroupsCount { get; private set; } = 0;

        public override void Visit(Group @group)
        {
            _currentLevel++;

            GroupsCount += _currentLevel;

            foreach (var groupItem in @group.Items)
            {
                groupItem.Accept(this);
            }

            _currentLevel--;
        }

        public override void Visit(Garbage garbage)
        {
            // ignore
        }
    }
}
