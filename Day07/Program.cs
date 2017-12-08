using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day07
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 07");

            var input = File.ReadAllLines("Input.txt")
                .Select(RawNodeData.Parse)
                .ToArray();

            var treeNodesList = input.Select(rawNode => new TreeNode<RawNodeData>(rawNode)).ToHashSet();

            foreach (var treeNode in treeNodesList)
            {
                var data = treeNode.Value;
                if (data.Subnodes == null)
                    continue;

                foreach (var subnodeName in data.Subnodes)
                {
                    var subTreeNode = treeNodesList.Single(node => node.Value.Name.Equals(subnodeName));
                    treeNode.AttachChild(subTreeNode);
                }
            }

            var topTreeNode = treeNodesList.Single(node => node.Parent == null);

            PrintAnswer("Problem 1 answer", topTreeNode.Value.Name);

            var wrongNode = topTreeNode;
            int wrongWeight = 0;

            while (true)
            {
                var childWeights = (wrongNode.Children ?? new List<TreeNode<RawNodeData>>()).Select(CalculateTreeNodeWeight).ToList();

                var maxWeight = childWeights.Max();
                var allSame = childWeights.All(weight => weight == maxWeight);

                if (wrongNode.Children == null || allSame)
                {
                    // currentNode is the one with wrong weight, as all children (if any) are ok
                    break;
                }
                
                // find child with wrong node
                wrongWeight = childWeights
                    .GroupBy(weight => weight)
                    .Select(grp => new {Value = grp.Key, Count = grp.Count()})
                    .First(a => a.Count == 1)
                    .Value;

                var wrongWeightIndex = childWeights.IndexOf(wrongWeight);
                wrongNode = wrongNode.Children[wrongWeightIndex];
            }

            // currentNode is the one to change
            var siblingNodesWithWeights = wrongNode.Parent.Children
                .Select(node => new
                {
                    Node = node,
                    Weight = CalculateTreeNodeWeight(node)
                });

            var firstCorrectNodeWithWeight = siblingNodesWithWeights.First(a => a.Node != wrongNode);
            var correctTotalWeight = firstCorrectNodeWithWeight.Weight;
            var weightOfWrongNodeChildren = wrongWeight - wrongNode.Value.Weight;
            var correctWeight = correctTotalWeight - weightOfWrongNodeChildren;

            PrintAnswer("Problem 2 answer", correctWeight);
        }

        private static int CalculateTreeNodeWeight(TreeNode<RawNodeData> treeNode)
        {
            return treeNode.Value.Weight +
                   (treeNode.Children ?? new List<TreeNode<RawNodeData>>())
                   .Select(CalculateTreeNodeWeight)
                   .Sum();
        }
    }
}
