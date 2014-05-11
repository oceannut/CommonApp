using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    public static class Tree
    {

        public static int MaxDepthTraverse<T>(TreeNode<T> root,
            Action<TreeNode<T>> maxDepthChanged) where T : ICategoryable
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }
            int maxDepth = 0;
            Stack<int> depthStack = new Stack<int>();
            depthStack.Push(0);
            PreOrderTraverseAll<T>(root, (node) =>
            {
                int depth = depthStack.Pop();
                if (!node.IsLeaf)
                {
                    foreach (TreeNode<T> childNode in node.Children)
                    {
                        depthStack.Push(depth + 1);
                    }
                }
                else
                {
                    if (maxDepth <= depth)
                    {
                        maxDepth = depth;
                        if (maxDepthChanged != null)
                        {
                            maxDepthChanged(node);
                        }
                    }
                }
            });
            return maxDepth;
        }

        /// <summary>
        /// 先序遍历树。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node">树的根节点。</param>
        /// <param name="visitor"></param>
        public static void PreOrderTraverseAll<T>(TreeNode<T> root,
            Action<TreeNode<T>> visitor) where T : ICategoryable
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }
            if (visitor == null)
            {
                throw new ArgumentNullException("visitor");
            }

            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                TreeNode<T> currentNode = stack.Pop();
                visitor(currentNode);
                if (!currentNode.IsLeaf)
                {
                    TreeNodeCollection<T> childNodes = currentNode.Children;
                    int endIndex = childNodes.Count - 1;
                    for (int i = endIndex; i >= 0; i--)
                    {
                        stack.Push(childNodes[i]);
                    }
                }
            }
        }

        public static void PreOrderTraverseFirst<T>(TreeNode<T> root,
            Func<TreeNode<T>, bool> visitor) where T : ICategoryable
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }
            if (visitor == null)
            {
                throw new ArgumentNullException("visitor");
            }

            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                TreeNode<T> currentNode = stack.Pop();
                if (visitor(currentNode))
                {
                    stack.Clear();
                    return;
                }
                if (!currentNode.IsLeaf)
                {
                    TreeNodeCollection<T> childNodes = currentNode.Children;
                    int endIndex = childNodes.Count - 1;
                    for (int i = endIndex; i >= 0; i--)
                    {
                        stack.Push(childNodes[i]);
                    }
                }
            }
        }

        public static void PostOrderTraverseAll<T>(TreeNode<T> root,
            Action<TreeNode<T>> visitor) where T : ICategoryable
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }
            if (visitor == null)
            {
                throw new ArgumentNullException("visitor");
            }

            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            Stack<bool> visitedStack = new Stack<bool>();
            stack.Push(root);
            visitedStack.Push(false);
            while (stack.Count > 0)
            {
                TreeNode<T> currentNode = stack.Pop();
                bool visited = visitedStack.Pop();
                if (currentNode.IsLeaf || visited)
                {
                    //如果当前节点是叶子节点，或该节点已被访问过，则进行遍历操作。
                    visitor(currentNode);
                }
                else
                {
                    //如果当前节点不是叶子节点，且第一次访问，则重新压栈，并标识未已访问。
                    stack.Push(currentNode);
                    visitedStack.Push(true);
                    if (!currentNode.IsLeaf)
                    {
                        TreeNodeCollection<T> childNodes = currentNode.Children;
                        int endIndex = childNodes.Count - 1;
                        for (int i = endIndex; i >= 0; i--)
                        {
                            stack.Push(childNodes[i]);
                            visitedStack.Push(false);
                        }
                    }
                }
            }
        }

        public static void PostOrderTraverseFirst<T>(TreeNode<T> root,
            Func<TreeNode<T>, bool> visitor) where T : ICategoryable
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }
            if (visitor == null)
            {
                throw new ArgumentNullException("visitor");
            }

            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            Stack<bool> visitedStack = new Stack<bool>();
            stack.Push(root);
            visitedStack.Push(false);
            while (stack.Count > 0)
            {
                TreeNode<T> currentNode = stack.Pop();
                bool visited = visitedStack.Pop();
                if (currentNode.IsLeaf || visited)
                {
                    //如果当前节点是叶子节点，或该节点已被访问过，则进行遍历操作。
                    if (visitor(currentNode))
                    {
                        stack.Clear();
                        visitedStack.Clear();
                        return;
                    }
                }
                else
                {
                    //如果当前节点不是叶子节点，且第一次访问，则重新压栈，并标识未已访问。
                    stack.Push(currentNode);
                    visitedStack.Push(true);
                    if (!currentNode.IsLeaf)
                    {
                        TreeNodeCollection<T> childNodes = currentNode.Children;
                        int endIndex = childNodes.Count - 1;
                        for (int i = endIndex; i >= 0; i--)
                        {
                            stack.Push(childNodes[i]);
                            visitedStack.Push(false);
                        }
                    }
                }
            }
        }

        public static void LeavesTraverse<T>(TreeNode<T> root,
            Action<TreeNode<T>> visitor) where T : ICategoryable
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }
            if (visitor == null)
            {
                throw new ArgumentNullException("visitor");
            }
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                TreeNode<T> currentNode = queue.Dequeue();
                if (!currentNode.IsLeaf)
                {
                    foreach (TreeNode<T> childNode in currentNode.Children)
                    {
                        queue.Enqueue(childNode);
                    }
                }
                else
                {
                    visitor(currentNode);
                }
            }
        }

        public static void LevelOrderTraverseAll<T>(TreeNode<T> root,
            Action<TreeNode<T>> visitor) where T : ICategoryable
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }
            if (visitor == null)
            {
                throw new ArgumentNullException("visitor");
            }

            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                TreeNode<T> currentNode = queue.Dequeue();
                visitor(currentNode);
                if (!currentNode.IsLeaf)
                {
                    foreach (TreeNode<T> childNode in currentNode.Children)
                    {
                        queue.Enqueue(childNode);
                    }
                }
            }
        }

        public static void LevelOrderTraverseFirst<T>(TreeNode<T> root,
            Func<TreeNode<T>, bool> visitor) where T : ICategoryable
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }
            if (visitor == null)
            {
                throw new ArgumentNullException("visitor");
            }

            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                TreeNode<T> currentNode = queue.Dequeue();
                if (visitor(currentNode))
                {
                    queue.Clear();
                    return;
                }
                if (!currentNode.IsLeaf)
                {
                    foreach (TreeNode<T> childNode in currentNode.Children)
                    {
                        queue.Enqueue(childNode);
                    }
                }
            }
        }


    }

}
