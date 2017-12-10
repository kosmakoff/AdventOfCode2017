using System.Collections.Generic;
using System.Linq;

namespace Day09.Parser
{
    internal class Group : GroupItem
    {
        public IList<GroupItem> Items { get; }

        public Group(IEnumerable<GroupItem> items)
        {
            Items = (items ?? new List<GroupItem>()).ToList();
        }

        public override string ToString()
        {
            return "{" + string.Join(",", Items.Select(item => item.ToString())) + "}";
        }

        public override void Accept(GroupItemVisitor groupItemVisitor)
        {
            groupItemVisitor.Visit(this);
        }
    }
}
