using System;
using System.Web;
using System.Reflection;
using System.Threading;
using System.Data;
using Miharu.Security.Library.Session;
using Slyg.Data;

namespace  WebSantander.code
{
    public abstract class page_form : page_generic
    {
        #region Declaraciones

        public EditOptions EditOptions = new EditOptions();

        public RequestType RequestType = RequestType.Post;

        #endregion

        #region Propiedades

        public new master.master_form Master
        {
            get { return (master.master_form)base.Master; }
        }

        #endregion

        #region Eventos

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            if (Request["RequestType"] != null) RequestType = (Request["RequestType"] == "Ajax") ? RequestType.Ajax : RequestType.Post;

            if (!this.IsPostBack && RequestType == RequestType.Post)
                this.MiharuSession.Pagina = new Pagina(this.ClientID, "", "", "");

            switch (RequestType)
            {
                case RequestType.Post:
                    Init_Page();
                    break;
                case RequestType.Ajax:
                    Init_Ajax();
                    break;
            }
        }

        public abstract void Config_Page();
        
        public void Init_Page()
        {
            if (IsSecurityPage() && !this.MiharuSession.UserLogged)
                ScriptHelper.Site.ShowAlert(this, "La sesión ha finalizado, por favor salga de la aplicación e inicie nuevamente", MsgBoxIcon.IconWarning);
            else if (IsSecurityPage() && !this.UserPagesWithAccess.Contains(FormatUrl( this.Request.Url.AbsolutePath)))
                ScriptHelper.Site.ShowAlert(this, "Acceso no permitido " + FormatUrl(this.Request.Url.AbsolutePath), MsgBoxIcon.IconError);
            else
                Config_Page();
        }

        private void Init_Ajax()
        {
            try
            {
                Response.Clear();

                if (IsSecurityPage() && !this.MiharuSession.UserLogged)
                    throw new ApplicationException("La sesión ha finalizado, por favor salga de la aplicación e inicie nuevamente");
                
                if (IsSecurityPage() && !this.UserPagesWithAccess.Contains(FormatUrl( this.Request.Url.AbsolutePath)))
                    throw new ApplicationException("Acceso no permitido");

                var ajaxMethod = Request["AjaxMethod"];
                var controller = Request["Controller"];

                MethodInfo methodInfo;
                object methodController;

                if (controller == "Page")
                {
                    methodController = this;
                    methodInfo = (this).GetType().GetMethod(ajaxMethod);
                }
                else
                {
                    FieldInfo methodControllerInfo = null;
                    foreach (var f in (this).GetType().GetFields())
                        if (f.FieldType.Name == controller)
                            methodControllerInfo = f;
                    if (methodControllerInfo == null) throw new Exception("Controlador Ajax no existe o no es público, " + controller);

                    methodController = methodControllerInfo.GetValue(this);
                    if (methodController == null) throw new Exception("Controlador Ajax no existe o no es público, " + controller);

                    methodInfo = methodControllerInfo.FieldType.GetMethod(ajaxMethod);
                }

                if (methodInfo == null) throw new Exception("Metodo Ajax no existe o no es público, " + ajaxMethod);

                var writer = new ScriptBuilder();

                methodInfo.Invoke(methodController, new object[] { writer });
                Response.Write(writer.ToString());

                Response.End();
            }
            catch (ThreadAbortException exa)
            {
                IgnoreException(exa);
            }
            catch (Exception ex)
            {
                base.TraceError(ex);
                ResponseError(ex);
            }
        }

        public void ResponseError(Exception nException)
        {
            ResponseError(nException.Message);
        }

        public void ResponseError(string nMessage)
        {
            //ExceptionController.Register(nException.Message, False, MsgBoxIcon.IconError, "", "")
            Response.Clear();
            Response.Write("ERROR: " + nMessage);
            Response.End();
        }

        public void TraceError(ScriptBuilder nHtml, Exception nException)
        {
            //Trace log
            if (!(nException is ApplicationException))               
                base.TraceError(nException);
            
            if (RequestType == RequestType.Ajax)
            {
                nHtml.Clear();
                nHtml.Append("ERROR: " + nException.Message);
            }
        }

        public new void TraceError(Exception nException)
        {
            if (!(nException is ApplicationException)) //Trace log
                base.TraceError(nException);

            if (RequestType == RequestType.Ajax)
                ResponseError(nException);
        }

        public void ResponseText(string nText, bool nEndResponse = true)
        {
            Response.Write(nText);
            if (nEndResponse) Response.End();
        }

        public void IgnoreException(ThreadAbortException exa)
        {
        }

        #endregion

        #region Funciones

        public virtual bool IsSecurityPage() 
        { 
            return true; 
        }

        public string GetValue(ColumnEnum nColumnEnum, bool nRequired = true)
        {
            return GetValue(nColumnEnum.ColumnName, nRequired);
        }

        public string GetValue(string nRequestKey, bool nRequired = true)
        {
            try
            {
// ReSharper disable ConstantNullCoalescingCondition
                var myRequest = Request ?? HttpContext.Current.Request;
// ReSharper restore ConstantNullCoalescingCondition

                if (myRequest[nRequestKey] == null || myRequest[nRequestKey].Trim()=="")
                    if (nRequired)
                        throw new ApplicationException("Valor de campo no encontrado " + nRequestKey);
                    else
                        return "";

                return myRequest[nRequestKey];
            }
            catch (Exception ex)
            {
                if (nRequired) 
                    throw new ApplicationException("Valor de campo no encontrado " + nRequestKey + ", " + ex.Message, ex);
                
                return "";
            }
        }

        public T GetValue<T>(ColumnEnum nColumnEnum, bool nRequired = true)
        {
            return GetValue<T>(nColumnEnum.ColumnName, nRequired);
        }

        public T GetValue<T>(string nRequestKey, bool nRequired = true)
        {
            var type = typeof(T);
            try
            {
// ReSharper disable ConstantNullCoalescingCondition
                var myRequest = Request ?? HttpContext.Current.Request;
// ReSharper restore ConstantNullCoalescingCondition

                if (myRequest[nRequestKey] == null) // || myRequest[nRequestKey].Trim() == "")
                {
                    if (nRequired)
                        throw new ApplicationException("Valor de campo no encontrado " + nRequestKey);

                    return (T) type.Assembly.CreateInstance(type.FullName);
                }

                if (type == typeof(decimal))
                    return (T) ((myRequest[nRequestKey].Trim() == "" && !nRequired) ? new Decimal(0) : (object)Program.ParseDecimal(myRequest[nRequestKey].Replace("$", "").Replace(",", "")));

                //if (type == typeof(byte))
                //    return (T)((myRequest[nRequestKey].Trim() == "" && !nRequired) ? (byte)0 : (object)byte.Parse(myRequest[nRequestKey]));

                if (type == typeof(string))
                    return (T)((myRequest[nRequestKey].Trim() == "" && !nRequired) ? "" : (object)myRequest[nRequestKey].ToString());

                if (type == typeof(bool))
                    return (T) ((myRequest[nRequestKey].Trim() == "" && !nRequired) ? false : (object) Slyg.Tools.DataConvert.ToBool(myRequest[nRequestKey]));

                if (type == typeof (DateTime))
                    return (T) ((myRequest[nRequestKey].Trim() == "" && !nRequired) ? DateTime.MinValue : (object) Program.ParseDateTime(myRequest[nRequestKey]));

                if (type == typeof (Slyg.Tools.SlygNullable<DateTime>))
                    return (T) ((myRequest[nRequestKey].Trim() == "" && !nRequired) 
                        ? new Slyg.Tools.SlygNullable<DateTime>(DBNull.Value) 
                        : (object) new Slyg.Tools.SlygNullable<DateTime>(Program.ParseDateTime(myRequest[nRequestKey])));

                if (type == typeof (Slyg.Tools.SlygNullable<decimal>))
                    return
                        (T) ((myRequest[nRequestKey].Trim() == "" && !nRequired)
                             ? new Slyg.Tools.SlygNullable<decimal>(DBNull.Value)
                             : (object) new Slyg.Tools.SlygNullable<decimal>(Program.ParseDecimal(myRequest[nRequestKey])));

                if (type == typeof (Slyg.Tools.SlygNullable<int>))
                    return (T) ((myRequest[nRequestKey].Trim() == "" && !nRequired)
                             ? new Slyg.Tools.SlygNullable<int>(DBNull.Value)
                             : (object)new Slyg.Tools.SlygNullable<int>(Convert.ToInt32(myRequest[nRequestKey])));

                if (type == typeof(Slyg.Tools.SlygNullable<short>))
                    return (T) ((myRequest[nRequestKey].Trim() == "" && !nRequired)
                             ? new Slyg.Tools.SlygNullable<short>(DBNull.Value)
                             : (object)new Slyg.Tools.SlygNullable<short>(Convert.ToInt16(myRequest[nRequestKey])));

                if (type == typeof (Slyg.Tools.SlygNullable<byte>))
                    return (T) ((myRequest[nRequestKey].Trim() == "" && !nRequired)
                             ? new Slyg.Tools.SlygNullable<byte>(DBNull.Value)
                             : (object)new Slyg.Tools.SlygNullable<byte>(Convert.ToByte(myRequest[nRequestKey])));

                return (T) ((myRequest[nRequestKey].Trim() == "" && !nRequired)
                         ? type.Assembly.CreateInstance(type.FullName)
                         : Convert.ChangeType(myRequest[nRequestKey], type));
            }
            catch (Exception ex)
            {
                if (nRequired)
                    throw new ApplicationException("Valor de campo no encontrado " + nRequestKey + ", " + ex.Message, ex);
                
                return (T) type.Assembly.CreateInstance(type.FullName);
            }
        }

        public string GetJsonValue(DataRow nRow, ColumnEnum nColumnEnum)
        {
            return GetJsonValue(nRow, nColumnEnum.ColumnName);
        }

        public string GetJsonValue(DataRow nRow, string nColumnName)
        {
            var val = "";
            if (!nRow.Table.Columns.Contains(nColumnName))
                throw new Exception("No se encuentra la columna " + nColumnName);
            var type = nRow.Table.Columns[nColumnName].DataType;

            if (nRow[nColumnName] != null)
            {
                if (type == typeof (DateTime))
                    val = string.Format(Program.DateFormat, nRow[nColumnName]);
                else if (type == typeof (decimal))
                    val = nRow[nColumnName].ToString().Replace(",", ".");
                else if (type == typeof (bool))
                    val = nRow[nColumnName].ToString().ToLower();
                else
                    val = nRow[nColumnName].ToString();
            }

            if (type == typeof (decimal) || type == typeof (int) || type == typeof (short) || type == typeof (bool))
                return FormatVariableName(nColumnName) + ":" +
                       ((nRow[nColumnName] == null || nRow[nColumnName] == DBNull.Value) ? "null" : val);

            return FormatVariableName(nColumnName) + ":" +
                   ((nRow[nColumnName] == null || nRow[nColumnName] == DBNull.Value)
                        ? "null"
                        : "'" + new ScriptBuilder().CleanListCharacters(val) + "'");
        }

        public string FormatVariableName(string nColumnName)
        {
            //var invalidCharacters = new[] { "@", ".", "$", " ", "ñ" };
            var invalidCharacters = new[] { "@", ".", "$", " "};
            foreach (string ichar in invalidCharacters)
            {
                nColumnName = nColumnName.Replace(ichar, "_");
            }

            return nColumnName;
        }

        #endregion
    }
}