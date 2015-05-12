using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Qyz.FrameWork.Utility
{
    public class BindList<T> : System.ComponentModel.BindingList<T>
    {
        #region 属性
        /// <summary>
        /// 当前删除项
        /// </summary>
        private T currentRemovedItem ;
        private List<T> _AddedList;

        /// <summary>
        /// 新增的集合
        /// </summary>
        public List<T> AddedList
        {
            get { return _AddedList; }
        }


        private List<T> _EditedList;
        /// <summary>
        /// 修改的集合
        /// </summary>
        public List<T> EditedList
        {
            get { return _EditedList; } 
        }

        private List<T> _DeletedList;
        /// <summary>
        /// 删除的集合
        /// </summary>
        public List<T> DeletedList
        {
            get { return _DeletedList; }
        }
        #endregion

        #region 构造函数
        public BindList()
            : base()
        { }

        public BindList(IList<T> list)
            : base(list)
        { }
        #endregion

        #region 方法
        protected override void OnListChanged(System.ComponentModel.ListChangedEventArgs e)
        {
            base.OnListChanged(e);

            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                T obj = this.Items[e.NewIndex];
                if (!this._AddedList.Contains(obj))
                {
                    if (!_DeletedList.Contains(obj))
                        this._AddedList.Add(obj);
                    else
                    {
                        this._EditedList.Add(obj);
                        this.DeletedList.Remove(obj);
                    }
                }
            }
            else if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                T obj = this.Items[e.NewIndex];
                if (!this._AddedList.Contains(obj) && !this._EditedList.Contains(obj))
                    this._EditedList.Add(obj);
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                T obj = currentRemovedItem;
                if (obj == null) return;
                if (this._AddedList.Contains(obj))
                    this._AddedList.Remove(obj);
                else
                {
                    if (!_DeletedList.Contains(obj))
                        this._DeletedList.Add(obj);
                    if (!this._EditedList.Contains(obj))
                        this.EditedList.Remove(obj);
                }
                this.currentRemovedItem = default(T);
            }
        }

        /// <summary>
        /// 按索引删除项
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            this.currentRemovedItem = base.Items[index];
            base.RemoveItem(index);
        }

        /// <summary>
        /// 清空所有数据
        /// </summary>
        public void Clear()
        {
            this._AddedList.Clear();
            this._EditedList.Clear();
            this._DeletedList.Clear();
            base.Items.Clear();
        }
        
        /// <summary>
        /// 确定修改数据
        /// </summary>
        public  void AcceptChanges()
        {
            this._AddedList.Clear();
            this._EditedList.Clear();
            this._DeletedList.Clear();
        }
        #endregion
    }
}
