using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 分类信息。
    /// </summary>
    public class Category : ICategoryable, IDisuseable<Category>
    {

        #region properties

        /// <summary>
        /// 编号。
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 编码。
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 详细。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图标。
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 上级分类编号。
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 排序号。
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 指示是否被废弃；true表示废弃，不再使用；false表示未废弃，仍在使用。
        /// </summary>
        public bool Disused { get; set; }

        /// <summary>
        /// 应用范围。
        /// </summary>
        public string Scope { get; set; }

        #endregion

        #region constructors

        /// <summary>
        /// 构建一个分类。
        /// </summary>
        public Category() { }

        /// <summary>
        /// 构建一个分类。
        /// </summary>
        /// <param name="id">编号。</param>
        public Category(long id)
        {
            this.Id = id;
        }

        #endregion

        #region

        public void Disuse(Action<Category> action)
        {
            this.Disused = true;

            if (action != null)
            {
                action(this);
            }
        }

        public void Use(Action<Category> action)
        {
            this.Disused = false;

            if (action != null)
            {
                action(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryTree"></param>
        /// <param name="action"></param>
        public void Save(CategoryTree categoryTree, 
            Action<Category> action)
        {
            if (this.Id > 0 && IsParentAlsoItself())
            {
                throw new CyclicInheritanceException(this.Id);
            }
            if (categoryTree != null && categoryTree.IsExisted(this))
            {
                throw new ObjectAlreadyExistedException(this.Id == 0 ? this.Code : this.Id.ToString());
            }
            if (action != null)
            {
                action(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newParentId"></param>
        /// <param name="categoryTree"></param>
        /// <param name="action"></param>
        public void ChangeParent(long newParentId, 
            CategoryTree categoryTree, 
            Action<Category> action)
        {
            if (this.Id == 0)
            {
                throw new InvalidOperationException();
            }
            if (this.ParentId != newParentId)
            {
                if (newParentId > 0)
                {
                    if (IsParentAlsoItself())
                    {
                        throw new CyclicInheritanceException(this.Id);
                    }
                    if (categoryTree != null && categoryTree.IsCyclic(this))
                    {
                        throw new CyclicInheritanceException(this.Id);
                    }
                }
                this.ParentId = newParentId;
                if (action != null)
                {
                    action(this);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newSequence"></param>
        public void ChangeSequence(int newSequence)
        {
            this.Sequence = newSequence;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Category target = obj as Category;
            if (target == null)
            {
                return false;
            }
            if (ReferenceEquals(this, target))
            {
                return true;
            }
            if (this.Id == 0
                || target.Id == 0)
            {
                throw new InvalidOperationException();
            }
            return this.Id == target.Id;
        }

        public override int GetHashCode()
        {
            int hashCode = 31;
            if (this.Id > 0)
            {
                hashCode += this.Id.GetHashCode() * 2 + 31;
            }
            return hashCode;
        }

        private bool IsParentAlsoItself()
        {
            if (ParentId == 0)
            {
                return false;
            }
            if (Id > 0 && Id == ParentId)
            {
                return true;
            }

            return false;
        }

        #endregion

    }

}
