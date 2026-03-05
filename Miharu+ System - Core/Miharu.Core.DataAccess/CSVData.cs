using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Slyg.Tools;

// ReSharper disable InconsistentNaming
namespace DBCore
{
    public class CSVData
    {
        #region Declaraciones

        string[] _dataString;

        private Regex _regQuote = new Regex(@"^(\x22)(.*)(\x22)(\s*,)(.*)$", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
        private Regex _regNormal = new Regex(@"^([^,]*)(\s*,)(.*)$", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
        private Regex _regQuoteLast = new Regex(@"^(\x22)([\x22*]{2,})(\x22)$", RegexOptions.IgnoreCase);
        private Regex _regNormalLast = new Regex(@"^.*$", RegexOptions.IgnoreCase);

        #endregion

        #region Costructores

        public CSVData()
        {
            this.LinesToJump = 0;
            this.Separator = ",";
            this.TextQualifier = "\"";
            this.HasHeader = true;
            this.DateFormat = "yyyy-MM-dd HH:mm:ss";
            this.LoadEncoding = null;
            this.SaveEncoding = Encoding.UTF8;
        }

        public CSVData(string nSeparator, string nTextQualifier, bool nHasHeader)
            : this()
        {
            this.Separator = nSeparator;
            this.TextQualifier = nTextQualifier;
            this.HasHeader = nHasHeader;
        }

        //~CSVData()
        //{
        //    Dispose(false);
        //}

        #endregion

        #region Propiedades

        public int LinesToJump { get; set; }

        public string Separator { get; set; }

        public string TextQualifier { get; set; }

        public bool HasHeader { get; set; }

        public string DateFormat { get; set; }

        public CSVTable DataTable { get; set; }

        /// <summary>
        /// Define la codificación usada para leer los archivos de texto, si es null se lee a partir de la propiedades del archivo
        /// </summary>
        public Encoding LoadEncoding { get; set; }

        /// <summary>
        /// Define la codificación usada para escribir los archivos de texto
        /// </summary>
        public Encoding SaveEncoding { get; set; }

        #endregion

        #region Load CSV

        public void LoadCSV(string nCSVFile, List<CSVLoadColumnDefinition> nCSVLoadColumDefinition)
        {
            if (!File.Exists(nCSVFile))
                throw new Exception(nCSVFile + " no existe.");

            using (
                var sr = this.LoadEncoding == null
                    ? new StreamReader(nCSVFile, true)
                    : new StreamReader(nCSVFile, this.LoadEncoding))
            {
                LoadCSV(sr, nCSVLoadColumDefinition);
            }
        }

        public void LoadCSV(StreamReader nCSVFile, List<CSVLoadColumnDefinition> nCSVLoadColumDefinition)
        {
            if (nCSVLoadColumDefinition == null)
                throw new Exception("se debe ingresar la configuración de columnas");

            if (this.DataTable != null)
                this.DataTable.Clear();

            this.DataTable = new CSVTable();

            // Crear estructura
            foreach (var definition in nCSVLoadColumDefinition)
            {
                this.DataTable.Columns.Add(new CSVColumn(definition.ColumnName));
            }
            
            if(this.HasHeader)
                nCSVFile.ReadLine();

            if (this.LinesToJump > 0)
            {
                for (var i = 0; i < this.LinesToJump; i++)
                {
                    nCSVFile.ReadLine();
                }
            }

            var line = nCSVFile.ReadLine();

            while (line != null)
            {
                var newRow = this.DataTable.NewRow();

                for (var i =0 ; i<this.DataTable.Columns.Count; i++)
                {
                    var definition = nCSVLoadColumDefinition[i];
                    newRow[i] = line.Substring(definition.Start, definition.Length).Trim();
                }

                this.DataTable.Rows.Add(newRow);

                line = nCSVFile.ReadLine();
            }
            
            nCSVFile.Close();
        }

        public void LoadCSV(string nCSVFile)
        {
            if (!File.Exists(nCSVFile))
                throw new Exception(nCSVFile + " no existe.");

            using (var sr = this.LoadEncoding == null ? new StreamReader(nCSVFile, true) : new StreamReader(nCSVFile, this.LoadEncoding))
            {
                LoadCSV(sr);
            }
        }

        public void LoadCSV(string nCSVFile, bool nHasHeader)
        {
            this.HasHeader = nHasHeader;
            LoadCSV(nCSVFile);
        }

        public void LoadCSV(string nCSVFile, bool nHasHeader, string nSeparator)
        {
            this.Separator = nSeparator;
            LoadCSV(nCSVFile, nHasHeader);
        }

        public void LoadCSV(string nCSVFile, bool nHasHeader, string nSeparator, string nTextQualifier)
        {
            this.TextQualifier = nTextQualifier;
            LoadCSV(nCSVFile, nHasHeader, nSeparator);
        }


        public void LoadCSV(StreamReader nCSVFile)
        {
            SetupRegEx();

            if (this.DataTable != null)
                this.DataTable.Clear();

            this.DataTable = new CSVTable();

            var firstLine = true;

            if (this.LinesToJump > 0)
            {
                for (var i = 0; i < this.LinesToJump; i++)
                {
                    nCSVFile.ReadLine();
                }
            }

            do
            {
                ProcessLine(nCSVFile.ReadLine());

                //
                // Create Columns
                //
                if (firstLine)
                {
                    for (var idx = 0; idx <= _dataString.GetUpperBound(0); idx++)
                    {
                        if (this.HasHeader)
                            this.DataTable.Columns.Add(_dataString[idx]);
                        else
                            this.DataTable.Columns.Add("Column" + idx);
                    }
                }

                //
                // Add Data
                //
                if (!(firstLine && this.HasHeader))
                {
                    var dr = this.DataTable.NewRow();

                    for (var idx = 0; idx <= _dataString.GetUpperBound(0); idx++)
                    {
                        dr[idx] = _dataString[idx];
                    }

                    this.DataTable.Rows.Add(dr);
                }

                firstLine = false;
            }
            while (nCSVFile.Peek() > -1);

            nCSVFile.Close();
        }

        public void LoadCSV(StreamReader nCSVFile, bool nHasHeader)
        {
            this.HasHeader = nHasHeader;
            LoadCSV(nCSVFile);
        }

        public void LoadCSV(StreamReader nCSVFile, bool nHasHeader, string nSeparator)
        {
            this.Separator = nSeparator;
            LoadCSV(nCSVFile, nHasHeader);
        }

        public void LoadCSV(StreamReader nCSVFile, bool nHasHeader, string nSeparator, string nTextQualifier)
        {
            this.TextQualifier = nTextQualifier;
            LoadCSV(nCSVFile, nHasHeader, nSeparator);
        }

        #endregion

        #region Save As

        public void SaveAsCSV(string nCSVFile, bool nAutoTrim)
        {
            if (this.DataTable == null)
                return;

            SaveAsCSV(this.DataTable, nCSVFile, nAutoTrim);
        }

        public void SaveAsCSV(string nCSVFile, bool nAutoTrim, bool nHasHeader)
        {
            this.HasHeader = nHasHeader;
            SaveAsCSV(nCSVFile, nAutoTrim);
        }

        public void SaveAsCSV(string nCSVFile, bool nAutoTrim, bool nHasHeader, string nSeparator)
        {
            this.Separator = nSeparator;
            SaveAsCSV(nCSVFile, nAutoTrim, nHasHeader);
        }

        public void SaveAsCSV(string nCSVFile, bool nAutoTrim, bool nHasHeader, string nSeparator, string nTextQualifier)
        {
            this.TextQualifier = nTextQualifier;
            SaveAsCSV(nCSVFile, nAutoTrim, nHasHeader, nSeparator);
        }


        public void SaveAsCSV(CSVTable nDataTable, string nCSVFile, bool nAutoTrim)
        {
            SetupRegEx();

            var sLine = "";
            var sw = new StreamWriter(nCSVFile, false, this.SaveEncoding);

            if (this.HasHeader)
            {
                for (var iCol = 0; iCol < nDataTable.Columns.Count; iCol++)
                {
                    if (!nDataTable.Columns[iCol].Export) continue;
                    if (sLine.Length > 0)
                        sLine += this.Separator;

                    sLine += ExportFormat(nDataTable.Columns[iCol].ColumnTitle);
                }

                sw.WriteLine(sLine);
            }

            foreach (CSVRow dr in nDataTable.Rows)
            {
                sLine = "";

                for (var iCol = 0; iCol < nDataTable.Columns.Count; iCol++)
                {
                    if (!nDataTable.Columns[iCol].Export) continue;

                    if (iCol > 0) sLine += this.Separator;

                    if (dr[iCol] == null) continue;

                    var valor = ExportFormat(GetValueFormated(dr[iCol], nDataTable.Columns[iCol].Format));

                    if (valor.IndexOf(this.Separator) > -1)
                        sLine += this.TextQualifier + (nAutoTrim ? valor.Trim() : valor) + this.TextQualifier;
                    else
                        sLine += (nAutoTrim ? ExportFormat(valor.Trim()) : valor);
                }

                sw.WriteLine(sLine);
            }

            sw.Flush();
            sw.Close();
        }

        private string ExportFormat(string nExportText)
        {
            // Eliminar el enter
            var result = nExportText.Replace('\n', ' ').Replace("\r", "");

            // Remplazar identificador de texto
            if (this.TextQualifier != "")
                result = result.Trim(this.TextQualifier.ToCharArray());

            return result;
        }

        public void SaveAsCSV(CSVTable nDataTable, string nCSVFile, bool nAutoTrim, bool nHasHeader)
        {
            this.HasHeader = nHasHeader;
            SaveAsCSV(nDataTable, nCSVFile, nAutoTrim);
        }

        public void SaveAsCSV(CSVTable nDataTable, string nCSVFile, bool nAutoTrim, bool nHasHeader, string nSeparator)
        {
            this.Separator = nSeparator;
            SaveAsCSV(nDataTable, nCSVFile, nAutoTrim, nHasHeader);
        }

        public void SaveAsCSV(CSVTable nDataTable, string nCSVFile, bool nAutoTrim, bool nHasHeader, string nSeparator, string nTextQualifier)
        {
            this.TextQualifier = nTextQualifier;
            SaveAsCSV(nDataTable, nCSVFile, nAutoTrim, nHasHeader, nSeparator);
        }


        public void SaveAsCSV(CSVTable nDataTable, MemoryStream nCSVStream, bool nAutoTrim)
        {
            SetupRegEx();

            var sLine = "";

            if (this.HasHeader)
            {
                for (var iCol = 0; iCol < nDataTable.Columns.Count; iCol++)
                {
                    if (!nDataTable.Columns[iCol].Export) continue;
                    if (sLine.Length > 0)
                        sLine += this.Separator;

                    sLine += nDataTable.Columns[iCol].ColumnTitle;
                }

                var bytes = this.SaveEncoding.GetBytes(sLine + "\r\n");
                nCSVStream.Write(bytes, 0, bytes.Length);
            }

            foreach (CSVRow dr in nDataTable.Rows)
            {
                sLine = "";

                for (var iCol = 0; iCol < nDataTable.Columns.Count; iCol++)
                {
                    if (!nDataTable.Columns[iCol].Export) continue;
                    if (iCol > 0) sLine += this.Separator;

                    if (dr[iCol] == null) continue;
                    var valor = GetValueFormated(dr[iCol], nDataTable.Columns[iCol].Format);

                    if (valor.IndexOf(this.Separator) > -1)
                        sLine += this.TextQualifier + (nAutoTrim ? valor.Trim() : valor) + this.TextQualifier;
                    else
                        sLine += (nAutoTrim ? valor.Trim() : valor);
                }

                var bytes = this.SaveEncoding.GetBytes(sLine + "\r\n");
                nCSVStream.Write(bytes, 0, bytes.Length);
            }
        }

        public void SaveAsCSV(CSVTable nDataTable, MemoryStream nCSVStream, bool nAutoTrim, bool nHasHeader)
        {
            this.HasHeader = nHasHeader;
            SaveAsCSV(nDataTable, nCSVStream, nAutoTrim);
        }

        public void SaveAsCSV(CSVTable nDataTable, MemoryStream nCSVStream, bool nAutoTrim, bool nHasHeader, string nSeparator)
        {
            this.Separator = nSeparator;
            SaveAsCSV(nDataTable, nCSVStream, nAutoTrim, nHasHeader);
        }

        public void SaveAsCSV(CSVTable nDataTable, MemoryStream nCSVStream, bool nAutoTrim, bool nHasHeader, string nSeparator, string nTextQualifier)
        {
            this.TextQualifier = nTextQualifier;
            SaveAsCSV(nDataTable, nCSVStream, nAutoTrim, nHasHeader, nSeparator);
        }

        #endregion

        #region Metodos

        private void ProcessLine(string sLine)
        {
            _dataString = null;

            if (this.Separator != ControlChars.Tab)
                sLine = sLine.Replace(ControlChars.Tab, "    "); //Replace tab with 4 spaces

            do
            {
                string sData;

                Match m;
                if (_regQuote.IsMatch(sLine))
                {
                    _regQuote.Matches(sLine);
                    //
                    // "text",<rest of the line>
                    //
                    m = _regQuote.Match(sLine);
                    sData = m.Groups[2].Value;
                    sLine = m.Groups[5].Value;
                }
                else if (_regQuoteLast.IsMatch(sLine))
                {
                    //
                    // "text"
                    //
                    m = _regQuoteLast.Match(sLine);
                    sData = m.Groups[2].Value;
                    sLine = "";
                }
                else if (_regNormal.IsMatch(sLine))
                {
                    //
                    // text,<rest of the line>
                    //
                    m = _regNormal.Match(sLine);
                    sData = m.Groups[1].Value;
                    sLine = m.Groups[3].Value;
                }
                else if (_regNormalLast.IsMatch(sLine))
                {
                    //
                    // text
                    //
                    m = _regNormalLast.Match(sLine);
                    sData = m.Groups[0].Value;
                    sLine = "";
                }
                else
                {
                    //
                    // ERROR!!!!!
                    //
                    sData = "";
                    sLine = "";
                }

                sData = sData.Trim();

                if (this.Separator != ControlChars.Tab)
                    sLine = sLine.Trim();

                int idx;
                if (_dataString == null)
                {
                    _dataString = new string[1];
                    idx = 0;
                }
                else
                {
                    idx = _dataString.GetUpperBound(0) + 1;
                    var tmp = new string[idx + 1];

                    _dataString.CopyTo(tmp, 0);
                    _dataString = tmp;
                }

                _dataString[idx] = sData;
            }
            while (sLine.Length > 0);
        }

        private void SetupRegEx()
        {
            var sQuote = @"^(%Q)(.*)(%Q)(\s*%S)(.*)$";
            var sNormal = @"^([^%S]*)(\s*%S)(.*)$";
            var sQuoteLast = @"^(%Q)(.*)(%Q$)";
            const string sNormalLast = @"^.*$";

            _regQuote = null;
            _regNormal = null;
            _regQuoteLast = null;
            _regNormalLast = null;

            var sSep = this.Separator;
            var sQual = this.TextQualifier;

            const string caracteres = @".$^{[(|)]}*+?\";

            if (caracteres.IndexOf(sSep) > -1) sSep = @"\" + sSep;
            if (caracteres.IndexOf(sQual) > 0) sQual = @"\" + sQual;

            sQuote = sQuote.Replace(@"%S", sSep);
            sQuote = sQuote.Replace(@"%Q", sQual);
            sNormal = sNormal.Replace(@"%S", sSep);
            sQuoteLast = sQuoteLast.Replace(@"%Q", sQual);

            _regQuote = new Regex(sQuote, RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            _regNormal = new Regex(sNormal, RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            _regQuoteLast = new Regex(sQuoteLast, RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            _regNormalLast = new Regex(sNormalLast, RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
        }

        #endregion

        #region Funciones

        private string GetValueFormated(object nValue, string nFormat)
        {
            if (nFormat != "")
            {
                switch (nValue.GetType().FullName)
                {
                    case "System.DateTime": return ((DateTime)nValue).ToString(nFormat);
                    case "System.Single": return ((Single)nValue).ToString(nFormat);
                    case "System.Double": return ((Double)nValue).ToString(nFormat);
                    case "System.Decimal": return ((Decimal)nValue).ToString(nFormat);
                    case "System.Byte": return ((Byte)nValue).ToString(nFormat);
                    case "System.Int16": return ((Int16)nValue).ToString(nFormat);
                    case "System.Int32": return ((Int32)nValue).ToString(nFormat);
                    case "System.Int64": return ((Int64)nValue).ToString(nFormat);
                    default: return nValue.ToString();
                }
            }

            switch (nValue.GetType().FullName)
            {
                case "System.DateTime":
                    return ((DateTime)nValue).ToString(this.DateFormat);

                case "System.Decimal":
                case "System.Double":
                case "System.Single":
                    return nValue.ToString().Replace(',', '.');

                default:
                    return nValue.ToString();
            }
        }

        #endregion
    }

    public class CSVTable : IEnumerable
    {
        #region Declaraciones

        public delegate void OnColumnAddDelegate(CSVColumn nColumn);
        public event OnColumnAddDelegate OnColumnAdd;

        public delegate void OnColumnRemoveDelegate(int nColumnIndex);
        public event OnColumnRemoveDelegate OnColumnRemove;

        public delegate void OnColumnsClearDelegate();
        public event OnColumnsClearDelegate OnColumnsClear;

        #endregion

        #region Propiedades

        public CSVRow this[int nRowIndex]
        {
            get { return this.Rows[nRowIndex]; }
        }

        public CSVColumnList Columns { get; private set; }

        public CSVRowList Rows { get; private set; }

        public int Count
        {
            get { return this.Rows.Count; }
        }

        #endregion

        #region Constructores

        public CSVTable()
        {
            this.Columns = new CSVColumnList(this);
            this.Rows = new CSVRowList(this);
        }

        public CSVTable(DataTable nDataTable)
            : this()
        {
            foreach (DataColumn column in nDataTable.Columns)
            {
                this.Columns.Add(column.ColumnName, column.DataType);
            }

            foreach (DataRow row in nDataTable.Rows)
            {
                var newRow = this.NewRow();

                for (var i = 0; i < this.Columns.Count; i++)
                {
                    if (!row.IsNull(i))
                        newRow[i] = row[i];
                }

                this.Rows.Add(newRow);
            }
        }

        ~CSVTable()
        {
            this.Columns.Clear();
            this.Columns = null;

            this.Rows.Clear();
            this.Rows = null;
        }

        #endregion

        #region Implementacion - IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Rows.GetEnumerator();
        }

        #endregion

        #region Metodos

        internal void ColumnAdd(CSVColumn newColumn)
        {
            if (this.OnColumnAdd != null) this.OnColumnAdd(newColumn);
        }

        internal void ColumnRemove(int nColumnIndex)
        {
            if (this.OnColumnRemove != null) this.OnColumnRemove(nColumnIndex);
        }

        internal void ColumnsClear()
        {
            if (this.OnColumnsClear != null) this.OnColumnsClear();
        }

        public void Clear()
        {
            this.Rows.Clear();
        }

        #endregion

        #region Funciones

        public DataTable ToDataTable()
        {
            return ToDataTable("Table1");
        }

        public DataTable ToDataTable(string nTableName)
        {
            var newDataTable = new DataTable();

            if (nTableName != "") newDataTable.TableName = nTableName;

            foreach (CSVColumn column in this.Columns)
            {
                newDataTable.Columns.Add(column.ColumnName, column.ColumnType);
            }

            foreach (CSVRow row in this.Rows)
            {
                var newRow = newDataTable.NewRow();

                for (var i = 0; i < this.Columns.Count; i++)
                {
                    if (row[i] != null)
                        newRow[i] = row[i];
                }

                newDataTable.Rows.Add(newRow);
            }

            return newDataTable;
        }

        public CSVRow NewRow()
        {
            return new CSVRow(this);
        }

        public CSVRow[] ToArray()
        {
            var result = new CSVRow[this.Count];

            for (var i = 0; i < this.Rows.Count; i++)
            {
                result[i] = this.Rows[i];
            }

            return result;
        }

        #endregion
    }

    public class CSVColumnList : IEnumerable
    {
        #region Declaraciones

        private Dictionary<string, CSVColumn> _columnsDictionary;

        private List<CSVColumn> _columnsList;

        #endregion

        #region Propiedades

        public CSVColumn this[int nColumnIndex]
        {
            get { return this._columnsList[nColumnIndex]; }
        }

        public CSVColumn this[string nColumnName]
        {
            get { return this._columnsDictionary[nColumnName]; }
        }

        public CSVTable Table { get; internal set; }

        public int Count
        {
            get { return this._columnsList.Count; }
        }

        #endregion

        #region Constructores

        internal CSVColumnList(CSVTable nTable)
        {
            this._columnsDictionary = new Dictionary<string, CSVColumn>();
            this._columnsList = new List<CSVColumn>();
            this.Table = nTable;
        }

        ~CSVColumnList()
        {
            this.Table = null;
            this._columnsDictionary.Clear();
            this._columnsList.Clear();
        }

        #endregion

        #region Implementacion - IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._columnsList.GetEnumerator();
        }

        #endregion

        #region Metodos

        public void Add(CSVColumn nColumn)
        {
            nColumn.Table = this.Table;
            this._columnsDictionary.Add(nColumn.ColumnName, nColumn);
            this._columnsList.Add(nColumn);
            this.Table.ColumnAdd(nColumn);

            // Reportar cambio en la colección de columnas
            this.Table.ColumnAdd(nColumn);
        }

        public void Remove(CSVColumn nColumn)
        {
            var columnIndex = this._columnsList.IndexOf(nColumn);
            this._columnsDictionary.Remove(nColumn.ColumnName);
            this._columnsList.Remove(nColumn);

            // Reportar cambio en la colección de columnas
            this.Table.ColumnRemove(columnIndex);
        }

        public void Clear()
        {
            this._columnsDictionary.Clear();
            this._columnsList.Clear();
            if (this.Table != null) this.Table.ColumnsClear();
        }

        #endregion

        #region Funciones

        public CSVColumn Add(string nColumnName)
        {
            return Add(nColumnName, typeof(string));
        }

        public CSVColumn Add(string nColumnName, Type nColumnType)
        {
            var newColumn = new CSVColumn(nColumnName, nColumnType);
            Add(newColumn);
            return newColumn;
        }

        public CSVColumn Remove(string nColumnName)
        {
            var removeColumn = this._columnsDictionary[nColumnName];
            Remove(removeColumn);
            return removeColumn;
        }

        public CSVColumn Remove(int nColumnIndex)
        {
            var removeColumn = this._columnsList[nColumnIndex];
            this._columnsDictionary.Remove(removeColumn.ColumnName);
            this._columnsList.Remove(removeColumn);
            return removeColumn;
        }

        internal int IndexOf(string nColumnName)
        {
            return this._columnsList.IndexOf(this._columnsDictionary[nColumnName]);
        }

        #endregion
    }

    public class CSVColumn
    {
        #region Propiedades

        public CSVTable Table { get; internal set; }

        public string ColumnName { get; private set; }

        public string ColumnTitle { get; set; }

        public Type ColumnType { get; private set; }

        public int ColumnIndex
        {
            get { return this.Table != null ? this.Table.Columns.IndexOf(this.ColumnName) : -1; }
        }

        public bool Export { get; set; }

        public string Format { get; set; }

        #endregion

        #region Constructores

        public CSVColumn(string nColumnName)
            : this(nColumnName, nColumnName, typeof(string))
        { }

        public CSVColumn(string nColumnName, string nColumnTitle)
            : this(nColumnName, nColumnTitle, typeof(string))
        { }

        public CSVColumn(string nColumnName, Type nColumnType)
            : this(nColumnName, nColumnName, nColumnType)
        { }

        public CSVColumn(string nColumnName, string nColumnTitle, Type nColumnType)
        {
            this.ColumnName = nColumnName;
            this.ColumnTitle = nColumnTitle;
            this.ColumnType = nColumnType;
            this.Export = true;
        }

        #endregion
    }

    public class CSVRowList : IEnumerable
    {
        #region Declaraciones

        private List<CSVRow> _rows;

        #endregion

        #region Propiedades

        public CSVRow this[int nRowIndex]
        {
            get { return this._rows[nRowIndex]; }
        }

        public CSVTable Table { get; internal set; }

        public int Count
        {
            get { return this._rows.Count; }
        }

        #endregion

        #region Constructores

        internal CSVRowList(CSVTable nTable)
        {
            this._rows = new List<CSVRow>();
            this.Table = nTable;
        }

        ~CSVRowList()
        {
            this.Table = null;
            this._rows.Clear();
        }

        #endregion

        #region Implementacion - IEnumerable

        public IEnumerator GetEnumerator()
        {
            return this._rows.GetEnumerator();
        }

        #endregion

        #region Metodos

        public void Add(CSVRow nRow)
        {
            if (nRow.Table == this.Table)
                this._rows.Add(nRow);
            else
                throw new Exception("El Row que intenta agregar no pertenece a la tabla");
        }

        public void Remove(CSVRow nRow)
        {
            this._rows.Remove(nRow);
        }

        public void Remove(int nRowIndex)
        {
            this._rows.RemoveAt(nRowIndex);
        }

        public void Clear()
        {
            this._rows.Clear();
        }

        #endregion
    }

    public class CSVRow : IEnumerable
    {
        #region Declaraciones

        private List<object> _items;

        #endregion

        #region Propiedades

        public object this[int nColumnIndex]
        {
            get { return this._items[nColumnIndex]; }
            set { this._items[nColumnIndex] = Convert.ChangeType(value, this.Table.Columns[nColumnIndex].ColumnType); }
        }

        public object this[string nColumnName]
        {
            get { return this._items[this.Table.Columns.IndexOf(nColumnName)]; }
            set
            {
                var columnIndex = this.Table.Columns.IndexOf(nColumnName);
                this._items[columnIndex] = Convert.ChangeType(value, this.Table.Columns[columnIndex].ColumnType);
            }
        }

        public CSVTable Table { get; internal set; }

        public int Count
        {
            get { return this._items.Count; }
        }

        #endregion

        #region Constructores

        internal CSVRow(CSVTable nTable)
        {
            this.Table = nTable;
            this._items = new List<object>();

            for (var i = 0; i < this.Table.Columns.Count; i++)
            {
                this._items.Add(null);
            }

            this.Table.OnColumnAdd += Table_OnColumnAdd;
            this.Table.OnColumnRemove += Table_OnColumnRemove;
            this.Table.OnColumnsClear += Table_OnColumnsClear;
        }

        ~CSVRow()
        {
            this._items.Clear();
            this._items = null;
        }

        #endregion

        #region Eventos

        void Table_OnColumnsClear()
        {
            if (this._items != null)
                this._items.Clear();
        }

        void Table_OnColumnRemove(int nColumnIndex)
        {
            this._items.RemoveAt(nColumnIndex);
        }

        void Table_OnColumnAdd(CSVColumn nColumn)
        {
            this._items.Add(null);
        }

        #endregion

        #region Implementacion - IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        #endregion
    }

    public class CSVLoadColumnDefinition
    {
        public string ColumnName { get; set; }

        public short Start { get; set; }

        public short Length { get; set; }

        public CSVLoadColumnDefinition(){}

        public CSVLoadColumnDefinition(string nColumnName, short nStart, short nLength)
        {
            this.ColumnName = nColumnName;
            this.Start = nStart;
            this.Length = nLength;
        }
    }
}
// ReSharper restore InconsistentNaming
