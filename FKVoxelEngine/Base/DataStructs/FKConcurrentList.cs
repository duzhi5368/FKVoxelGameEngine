/* 
 * WRANING: These codes below is far away from bugs with the god and his animal protecting
 *                  _oo0oo_                   ┏┓　　　┏┓
 *                 o8888888o                ┏┛┻━━━┛┻┓
 *                 88" . "88                ┃　　　　　　　┃ 　
 *                 (| -_- |)                ┃　　　━　　　┃
 *                 0\  =  /0                ┃　┳┛　┗┳　┃
 *               ___/`---'\___              ┃　　　　　　　┃
 *             .' \\|     |# '.             ┃　　　┻　　　┃
 *            / \\|||  :  |||# \            ┃　　　　　　　┃
 *           / _||||| -:- |||||- \          ┗━┓　　　┏━┛
 *          |   | \\\  -  #/ |   |          　　┃　　　┃神兽保佑
 *          | \_|  ''\---/''  |_/ |         　　┃　　　┃永无BUG
 *          \  .-\__  '-'  ___/-. /         　　┃　　　┗━━━┓
 *        ___'. .'  /--.--\  `. .'___       　　┃　　　　　　　┣┓
 *     ."" '<  `.___\_<|>_/___.' >' "".     　　┃　　　　　　　┏┛
 *    | | :  `- \`.;`\ _ /`;.`/ - ` : | |   　　┗┓┓┏━┳┓┏┛
 *    \  \ `_.   \_ __\ /__ _/   .-` /  /   　　　┃┫┫　┃┫┫
 *=====`-.____`.___ \_____/___.-`___.-'=====　　　┗┻┛　┗┻┛ 
 *                  `=---='　　　
 *          佛祖保佑       永无BUG
 */
// =============================================================================== 
// Author              :    Frankie.W
// Create Time         :    2017/7/21 18:17:32
// Update Time         :    2017/7/21 18:17:32
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    /// <summary>
    /// 并发链表 http://www.deanchalk.me.uk/post/Task-Parallel-Concurrent-List-Implementation.aspx
    /// </summary>
    public class FKConcurrentList<T> : IList<T>, IList
    {
        private readonly List<T>            m_UnderlyingList = new List<T>();
        private readonly object             m_SyncRootLock = new object();
        private readonly ConcurrentQueue<T> m_UnderlyingQueue;
        private bool                        m_bIsRequiresSync;
        private bool                        m_bIsDirty;


        public FKConcurrentList()
        {
            m_UnderlyingQueue = new ConcurrentQueue<T>();
        }

        public FKConcurrentList(IEnumerable<T> items)
        {
            m_UnderlyingQueue = new ConcurrentQueue<T>(items);
        }

        private void UpdateLists()
        {
            if (!m_bIsDirty)
                return;
            lock (m_SyncRootLock)
            {
                m_bIsRequiresSync = true;
                T temp;
                while (m_UnderlyingQueue.TryDequeue(out temp))
                    m_UnderlyingList.Add(temp);
                m_bIsRequiresSync = false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                return m_UnderlyingList.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (m_bIsRequiresSync)
                lock (m_SyncRootLock)
                    m_UnderlyingQueue.Enqueue(item);
            else
                m_UnderlyingQueue.Enqueue(item);
            m_bIsDirty = true;
        }

        public int Add(object value)
        {
            if (m_bIsRequiresSync)
                lock (m_SyncRootLock)
                    m_UnderlyingQueue.Enqueue((T)value);
            else
                m_UnderlyingQueue.Enqueue((T)value);
            m_bIsDirty = true;
            lock (m_SyncRootLock)
            {
                UpdateLists();
                return m_UnderlyingList.IndexOf((T)value);
            }
        }

        public bool Contains(object value)
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                return m_UnderlyingList.Contains((T)value);
            }
        }

        public int IndexOf(object value)
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                return m_UnderlyingList.IndexOf((T)value);
            }
        }

        public void Insert(int index, object value)
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                m_UnderlyingList.Insert(index, (T)value);
            }
        }

        public void Remove(object value)
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                m_UnderlyingList.Remove((T)value);
            }
        }

        public void RemoveAt(int index)
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                m_UnderlyingList.RemoveAt(index);
            }
        }

        T IList<T>.this[int index]
        {
            get
            {
                lock (m_SyncRootLock)
                {
                    UpdateLists();
                    return m_UnderlyingList[index];
                }
            }
            set
            {
                lock (m_SyncRootLock)
                {
                    UpdateLists();
                    m_UnderlyingList[index] = value;
                }
            }
        }

        object IList.this[int index]
        {
            get { return ((IList<T>)this)[index]; }
            set { ((IList<T>)this)[index] = (T)value; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Clear()
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                m_UnderlyingList.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                return m_UnderlyingList.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                m_UnderlyingList.CopyTo(array, arrayIndex);
            }
        }

        public bool Remove(T item)
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                return m_UnderlyingList.Remove(item);
            }
        }

        public void CopyTo(Array array, int index)
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                m_UnderlyingList.CopyTo((T[])array, index);
            }
        }

        public int Count
        {
            get
            {
                lock (m_SyncRootLock)
                {
                    UpdateLists();
                    return m_UnderlyingList.Count;
                }
            }
        }

        public object SyncRoot
        {
            get { return m_SyncRootLock; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public int IndexOf(T item)
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                return m_UnderlyingList.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (m_SyncRootLock)
            {
                UpdateLists();
                m_UnderlyingList.Insert(index, item);
            }
        }
    }
}