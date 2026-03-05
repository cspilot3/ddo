using System;
using System.Reflection;
using System.Threading;

namespace  Miharu.Client.Browser.code
{
    public abstract class page_site : page_generic
    {
        #region Propiedades

        public RequestType RequestType = RequestType.Post;

        public new master.master_site Master
        {
            get { return (master.master_site)base.Master; }
        }

        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Request["RequestType"] != null) RequestType = (Request["RequestType"] == "Ajax") ? RequestType.Ajax : RequestType.Post;

            if (RequestType == RequestType.Post) Config_Page();
            if (RequestType == RequestType.Ajax) Config_Ajax();
        }

        #endregion

        #region Metodos

        public abstract void Config_Page();

        private void Config_Ajax()
        {
            try
            {
                Response.Clear();
                var AjaxMethod = Request["AjaxMethod"];
                var Controller = Request["Controller"];

                MethodInfo methodInfo;
                object methodController;

                if (Controller == "Page")
                {
                    methodController = this;
                    methodInfo = (this).GetType().GetMethod(AjaxMethod);
                }
                else
                {
                    FieldInfo methodControllerInfo = null;
                    foreach (var f in (this).GetType().GetFields())
                        if (f.FieldType.Name == Controller)
                            methodControllerInfo = f;
                    if (methodControllerInfo == null) throw new Exception("Controlador Ajax no exite o no es publico, " + Controller);

                    methodController = methodControllerInfo.GetValue(this);
                    if (methodController == null) throw new Exception("Controlador Ajax no exite o no es publico, " + Controller);

                    methodInfo = methodControllerInfo.FieldType.GetMethod(AjaxMethod);
                }

                if (methodInfo == null) throw new Exception("Metodo Ajax no exite o no es publico, " + AjaxMethod);

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
                Program.TraceError(ex);
            }
        }

// ReSharper disable UnusedParameter.Local
        private void IgnoreException(Exception exa)
// ReSharper restore UnusedParameter.Local
        {
        }

        #endregion
    }
}