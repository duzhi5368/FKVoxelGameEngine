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
// Create Time         :    2017/7/24 17:46:08
// Update Time         :    2017/7/24 17:46:08
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Collections.Generic;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    /// <summary>
    /// 稀疏矩阵
    /// </summary>
    public class FKSparseMatrix<T>
    {
        protected Dictionary<uint, Dictionary<uint, T>> _Rows;

        public FKSparseMatrix()
        {
            _Rows = new Dictionary<uint, Dictionary<uint, T>>();
        }

        public T this[uint row, uint col]
        {
            get { return GetAt(row, col); }
            set { SetAt(row, col, value); }
        }

        public T GetAt(uint row, uint col)
        {
            Dictionary<uint, T> cols;
            if (_Rows.TryGetValue(row, out cols))
            {
                T value = default(T);
                if (cols.TryGetValue(col, out value))
                    return value;
            }
            return default(T);
        }

        public void SetAt(uint row, uint col, T value)
        {
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                RemoveAt(row, col);
            }
            else
            {
                Dictionary<uint, T> cols;
                if (!_Rows.TryGetValue(row, out cols))
                {
                    cols = new Dictionary<uint, T>();
                    _Rows.Add(row, cols);
                }
                cols[col] = value;
            }
        }

        public void RemoveAt(uint row, uint col)
        {
            Dictionary<uint, T> cols;
            if (_Rows.TryGetValue(row, out cols))
            {
                cols.Remove(col);
                if (cols.Count == 0)
                    _Rows.Remove(row);
            }
        }

        public IEnumerable<T> GetRowData(uint row)
        {
            Dictionary<uint, T> cols;
            if (_Rows.TryGetValue(row, out cols))
            {
                foreach (KeyValuePair<uint, T> pair in cols)
                {
                    yield return pair.Value;
                }
            }
        }

        public void removeRow(uint row)
        {
            _Rows.Remove(row);
        }

        public void removeColumn(uint col)
        {
            foreach (KeyValuePair<uint, Dictionary<uint, T>> rowdata in _Rows)
            {
                rowdata.Value.Remove(col);
            }
        }

        public int GetRowDataCount(uint row)
        {
            Dictionary<uint, T> cols;
            if (_Rows.TryGetValue(row, out cols))
            {
                return cols.Count;
            }
            return 0;
        }

        public IEnumerable<T> GetColumnData(uint col)
        {
            foreach (KeyValuePair<uint, Dictionary<uint, T>> rowdata in _Rows)
            {
                T result;
                if (rowdata.Value.TryGetValue(col, out result))
                    yield return result;
            }
        }

        public uint GetColumnDataCount(uint col)
        {
            uint result = 0;

            foreach (KeyValuePair<uint, Dictionary<uint, T>> cols in _Rows)
            {
                if (cols.Value.ContainsKey(col))
                    result++;
            }
            return result;
        }
    }
}