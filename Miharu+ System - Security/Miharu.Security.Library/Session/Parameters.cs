using System.Collections;
using System;

namespace Miharu.Security.Library.Session
{
    [Serializable]
    public class Parameters : IEnumerable, ICollection
    {
        #region Declaraciones

        private Hashtable _Items;

        #endregion

        #region Propiedades

        public object this[string Key]
        {
            get {
                return _Items.Contains(Key) ? _Items[Key] : null;
            }
            set
            {
                if (_Items.Contains(Key))
                    _Items[Key] = value;
                else
                    _Items.Add(Key, value);
            }
        }

        #endregion

        #region Constructores

        public Parameters()
        {
            _Items = new Hashtable();
        }

        #endregion

        #region Metodos

        public void Clear()
        {
            _Items.Clear();
        }

        #endregion

        #region Implementacion IEnumerable

        public IEnumerator GetEnumerator() { return _Items.GetEnumerator(); }

        #endregion

        #region Implementacion ICollection

        public void CopyTo(Array array, int index)
        {
            _Items.CopyTo(array, index);
        }

        public int Count { get { return _Items.Count; } }

        public bool IsSynchronized { get { return _Items.IsSynchronized; } }

        public object SyncRoot { get { return _Items.SyncRoot; } }

        #endregion
    }
}