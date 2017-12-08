using System;
using System.Collections.Generic;
using System.Text;

namespace Day07
{
    internal class TreeNode<T>
    {
        public T Value { get; }

        public TreeNode<T> Parent { get; private set; } = null;

        public List<TreeNode<T>> Children { get; } = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            Value = value;
        }

        public void AttachChild(TreeNode<T> child)
        {
            if (child.Parent != null && child.Parent != this)
                throw new InvalidOperationException("Can't attach someone else's child");

            if (child.Parent != null)
                throw new InvalidOperationException("Can't attach own child several times");

            if (Children.Contains(child))
                throw new InvalidOperationException("Can't attach same child twice");

            child.Parent = this;
            Children.Add(child);
        }
    }
}
