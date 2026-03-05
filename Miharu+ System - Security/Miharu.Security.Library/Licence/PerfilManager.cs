using System.Collections.Generic;
using System;

namespace Miharu.Security.Library.Licence
{
    [Serializable]
    public class PerfilManager
    {
        #region Propiedades

        public List<Permiso> Permisos { get; private set; }

        #endregion

        #region Metodos

        public PerfilManager()
        {
            this.Permisos = new List<Permiso>();
        }

        ~PerfilManager()
        {
            this.Permisos.Clear();
            this.Permisos = null;
        }

        #endregion

        #region Funciones

        public bool PuedeAcceder(string nAcceso)
        {
            foreach (var MyPermiso in this.Permisos)
            {
                if (ValidarPermiso(nAcceso, MyPermiso.Cadena)) return true;
            }

            return false;
        }

        public bool PuedeConsultar(string nAcceso)
        {
            var permisos = getCadenas(nAcceso);

            foreach (var MyPermiso in permisos)
            {
                if (MyPermiso.Consultar) return true;
            }

            return false;
        }

        public bool PuedeAgregar(string nAcceso)
        {
            var permisos = getCadenas(nAcceso);

            foreach (var MyPermiso in permisos)
            {
                if (MyPermiso.Agregar) return true;
            }

            return false;
        }

        public bool PuedeEditar(string nAcceso)
        {
            var permisos = getCadenas(nAcceso);

            foreach (var MyPermiso in permisos)
            {
                if (MyPermiso.Editar) return true;
            }

            return false;
        }

        public bool PuedeEliminar(string nAcceso)
        {
            var permisos = getCadenas(nAcceso);

            foreach (var MyPermiso in permisos)
            {
                if (MyPermiso.Eliminar) return true;
            }

            return false;
        }

        public bool PuedeExportar(string nAcceso)
        {
            var permisos = getCadenas(nAcceso);

            foreach (var MyPermiso in permisos)
            {
                if (MyPermiso.Exportar) return true;
            }

            return false;
        }

        public bool PuedeImprimir(string nAcceso)
        {
            var permisos = getCadenas(nAcceso);

            foreach (var MyPermiso in permisos)
            {
                if (MyPermiso.Imprimir) 
                    return true;
            }

            return false;
        }

        private IEnumerable<Permiso> getCadenas(string nAcceso)
        {
            var permisos = new List<Permiso>();

            foreach (var MyPermiso in this.Permisos)
            {
                if (ValidarPermiso(nAcceso, MyPermiso.Cadena))
                    permisos.Add(MyPermiso);
            }

            return permisos;
        }

        private bool valida_startsWith(string BuscarEn,string Buscado)
        {
            var splitBuscarEn = BuscarEn.Split('.');
            var splitBuscado = Buscado.Split('.');

            if (splitBuscado.Length > splitBuscarEn.Length)
            {
                return false;
            }

            for (int i = 0; i <= splitBuscado.Length -1 ; i++)
            { 
                if (splitBuscarEn[i] != splitBuscado[i])
                {
                    return false;
                }
            }
                return true;
        }


        private bool ValidarPermiso(string nAcceso, string nPermiso)
        {
            if (nPermiso == "*")
                return true;

            if (nAcceso == nPermiso)
                return true;
                                 
            //if (nPermiso.StartsWith(nAcceso))         
            if (valida_startsWith(nPermiso,nAcceso))
            {
                var Acceso = nAcceso.Split('.').Length;
                var Permiso = nPermiso.Split('.').Length;

                if(Acceso != Permiso )
                    return true;
            }
            else if (nPermiso.EndsWith(".*"))
            {
                var RaizAcceso = nAcceso.Split('.');
                var Acceso = RaizAcceso[0].Length;
               
                var RaizPermiso = nPermiso.TrimEnd('*').TrimEnd('.');

               
                if (Acceso > 1) 
                {
                    if (RaizPermiso.Length > 1)
                        //return nAcceso.StartsWith(RaizPermiso);
                        return valida_startsWith(nAcceso, RaizPermiso);
                        
                }
                else 
                {
                    //return nAcceso.StartsWith(RaizPermiso); 
                    return valida_startsWith(nAcceso, RaizPermiso);
                }
            }

            return false;
        }

        #endregion
    }
}