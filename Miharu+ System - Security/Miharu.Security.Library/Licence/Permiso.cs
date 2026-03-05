using System;

namespace Miharu.Security.Library.Licence
{
    [Serializable]
    public class Permiso
    {
        #region Propiedades

        public string Cadena { get; set; }
        public bool Consultar { get; set; }
        public bool Agregar { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
        public bool Exportar { get; set; }
        public bool Imprimir { get; set; }

        #endregion

        #region Constructores

        public Permiso()
        {
            this.Cadena = "";
            this.Consultar = false;
            this.Agregar = false;
            this.Editar = false;
            this.Eliminar = false;
            this.Exportar = false;
            this.Imprimir = false;
        }

        public Permiso(string nCadena, bool nConsultar, bool nAgregar, bool nEditar, bool nEliminar, bool nExportar, bool nImprimir)
        {
            this.Cadena = nCadena;
            this.Consultar = nConsultar;
            this.Agregar = nAgregar;
            this.Editar = nEditar;
            this.Eliminar = nEliminar;
            this.Exportar = nExportar;
            this.Imprimir = nImprimir;
        }

        #endregion
    }
}