using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 
    /// </summary>
    public class TreeNodeCollection<T> : IEnumerable<TreeNode<T>>, IEnumerable 
        where T : ICategoryable
    {

        #region events

        /// <summary>
        /// 添加一个孩子节点时要发生的事件。
        /// </summary>
        public event Action<TreeNode<T>> ChildAdded;

        /// <summary>
        /// 删除一个孩子节点时要发生的事件。
        /// </summary>
        public event Action<TreeNode<T>> ChildRemoved;

        #endregion

        #region fields

        private TreeNode<T> owner;
        private List<TreeNode<T>> col;

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public TreeNode<T> Owner
        {
            get { return owner; }
        }

        public int Count
        {
            get { return col.Count; }
        }

        /// <summary>
        /// 提供索引器方式的访问。
        /// </summary>
        /// <param name="index">索引位置。</param>
        /// <returns>返回对应的节点。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">索引值要大于0或小于Zoling.Collections.Treeing.TreeNodeCollection.Count。</exception>
        public TreeNode<T> this[int index]
        {
            get
            {
                return col[index];
            }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        internal TreeNodeCollection(TreeNode<T> owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException();
            }
            this.owner = owner;
            this.col = new List<TreeNode<T>>();
        }

        #endregion

        #region methods

        public void Add(TreeNode<T> node)
        {
            AddItemAt(Count, node);
        }

        public void Remove(TreeNode<T> node)
        {
        }

        /// <summary>
        /// 移除位于索引位置的节点。
        /// </summary>
        /// <param name="index">索引位置。</param>
        /// <exception cref="System.ArgumentOutOfRangeException">索引值要大于0或小于Zoling.Collections.Treeing.TreeNodeCollection.Count。</exception>
        public void RemoveAt(int index)
        {
            TreeNode<T> node = col[index];
            Remove(node);
        }

        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            return col.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return col.GetEnumerator();
        }

        /// <summary>
        /// 在指定的索引位置添加节点。
        /// </summary>
        /// <param name="index">索引位置。</param>
        /// <param name="item">节点。</param>
        private void AddItemAt(int index,
            TreeNode<T> node)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            if (owner != null)
            {
                if (IsCircuitRefrence(owner, node))
                {
                    throw new ArgumentException("The item added should not be himself or his ancestors");
                }
                node.Parent = owner;
            }
            col.Insert(index, node);
        }

        /// <summary>
        /// 解除子树上节点之间的关系。
        /// </summary>
        /// <param name="root">子树的根节点。</param>
        /// <param name="traverser">遍历器。</param>
        //private void DecoupleSubTree(TreeNode<T> root,
        //    ITreeTraverser traverser)
        //{
        //    //后序遍历子树，解除树形节点之间的关系。
        //    traverser.PostOrderTraverse(root, (n) =>
        //    {
        //        n.ParentNode = null;
        //        if (!n.IsLeaf)
        //        {
        //            n.ChildNodes.Clear();
        //        }
        //    });
        //}

        /// <summary>
        /// 判断添加子节点时，父节点与子节点之间是否形成了环路；如果形成了环路，则返回True，否则返回False。
        /// </summary>
        /// <param name="parentNode">父节点。</param>
        /// <param name="childNode">子节点。</param>
        /// <returns>如果形成了环路，则返回True，否则返回False。</returns>
        private bool IsCircuitRefrence(TreeNode<T> parentNode,
            TreeNode<T> childNode)
        {
            bool result = false;
            TreeNode<T> comparand = parentNode;
            while (comparand != null)
            {
                if (comparand.Equals(childNode))
                {
                    result = true;
                    break;

                }
                comparand = comparand.Parent;
            }
            return result;
        }

        #endregion

    }

}
