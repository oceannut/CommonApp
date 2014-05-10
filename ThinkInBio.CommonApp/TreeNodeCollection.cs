using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 
    /// </summary>
    public class TreeNodeCollection
    {

        private TreeNode owner;
        private List<TreeNode> cols;

        /// <summary>
        /// 
        /// </summary>
        public TreeNode Owner
        {
            get { return owner; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        internal TreeNodeCollection(TreeNode owner) 
        {
            this.owner = owner;
            this.cols = new List<TreeNode>();
        }

        public void Add(TreeNode node)
        {
        }

        public void Remove(TreeNode node)
        {
        }

    }

}
