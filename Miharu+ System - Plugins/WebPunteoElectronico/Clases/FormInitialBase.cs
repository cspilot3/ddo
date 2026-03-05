using WebPunteoElectronico.Master;

namespace WebPunteoElectronico.Clases
{
    public abstract class FormInitialBase : GenericBase
    {        
        #region Propiedades

        public new MasterForm Master
        {
            get { return (MasterForm)base.Master; }
        }

        #endregion

        #region Eventos

        #endregion
    }
}