using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 
    /// </summary>
    public class TreeNode<T> where T : ICategoryable
    {

        #region events

        /// <summary>
        /// 设置父节点时要发生的事件。
        /// </summary>
        public event Action<TreeNode<T>, TreeNode<T>> ParentChanged;

        #endregion

        #region fields

        protected TreeNode<T> parent;
        protected TreeNodeCollection<T> children;

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 父节点。
        /// </summary>
        public TreeNode<T> Parent
        {
            get { return parent; }
            set
            {
                if (parent != value)
                {
                    parent = value;
                    if (ParentChanged != null)
                    {
                        ParentChanged(this, parent);
                    }
                }
            }
        }

        /// <summary>
        /// 孩子节点。
        /// </summary>
        public TreeNodeCollection<T> Children
        {
            get { return children; }
        }

        /// <summary>
        /// 节点是否是树的根节点，即其没有父节点；返回True表示是根节点，否则返回Flase表示不是。
        /// </summary>
        public bool IsRoot
        {
            get { return this.Parent == null; }
        }

        /// <summary>
        /// 节点是否是树的叶子节点，即其没有孩子节点；返回True表示是叶子节点，否则返回Flase表示不是。
        /// </summary>
        public bool IsLeaf
        {
            get { return (Children == null || Children.Count == 0); }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 无参数的构建器。
        /// </summary>
        public TreeNode() 
        {
            children = new TreeNodeCollection<T>(this);
        }

        /// <summary>
        /// 构建器，构建一个有节点值的树节点。。
        /// </summary>
        /// <param name="value">节点值。</param>
        public TreeNode(T value)
            : this()
        {
            this.Value = value;
        }

        #endregion

        #region methods

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            TreeNode<T> target = obj as TreeNode<T>;
            if (target == null)
            {
                return false;
            }
            if (ReferenceEquals(this, target))
            {
                return true;
            }
            if (this.Value == null
                || target.Value == null)
            {
                throw new InvalidOperationException();
            }
            bool flag = this.Value.Equals(target.Value);
            //if (flag)
            //{
            //    if (this.Parent != null && target.Parent != null)
            //    {
            //        if (this.Parent.Equals(target.Parent))
            //        {
            //        }
            //    }
            //}
            return flag;
        }

        public override int GetHashCode()
        {
            int hashCode = 31;
            if (this.Value != null)
            {
                hashCode += this.Value.GetHashCode() * 2 + 31;
            }
            return hashCode;
        }

        #endregion

        //#region operators

        //public static bool operator ==(TreeNode<T> node1, TreeNode<T> node2)
        //{
        //    return false;
        //}

        //public static bool operator !=(TreeNode<T> node1, TreeNode<T> node2)
        //{
        //    return false;
        //}

        //#endregion

    }

}
