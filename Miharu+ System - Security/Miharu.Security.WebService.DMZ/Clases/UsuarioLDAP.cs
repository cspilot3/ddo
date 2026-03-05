using System;
using System.Collections.Generic;

namespace Miharu.Security.WebService.DMZ.Clases
{
    [Serializable]
    public class UsuarioLDAP
    {
        public bool Valido = false;

        public string Login = "";
        public string Nombres = "";
        public string Direccion = "";
        public string Telefono = "";
        public string Correo = "";

        public List<short> Permisos = new List<short>();
    }

    //public class Validador
    //{
    //    public UsuarioLDAP ValidarUsuarioLDAP(string nUser, string nPassword, GroupManager nGroupManager)
    //    {
    //        UsuarioLDAP usuario = new UsuarioLDAP() { Login = nUser };
    //        DirectoryEntry entry = null;

    //        try
    //        {
    //            entry = new DirectoryEntry(Program.LDAP.ServerPath, nUser, nPassword);

    //            object obj = entry.NativeObject;

    //            DirectorySearcher searcher = new DirectorySearcher(entry);
    //            searcher.CacheResults = false;
    //            searcher.Filter = "samaccountname=" + nUser;
    //            searcher.PropertiesToLoad.Add("memberof");
    //            searcher.PropertiesToLoad.Add("name");
    //            searcher.PropertiesToLoad.Add("mail");
    //            searcher.PropertiesToLoad.Add("streetaddress");
    //            searcher.PropertiesToLoad.Add("telephonenumber");

    //            SearchResultCollection results = searcher.FindAll();

    //            foreach (SearchResult resultados in results)
    //            {
    //                ResultPropertyCollection colProperties = resultados.Properties;

    //                // Grupos
    //                foreach (var value in colProperties["memberof"])
    //                {
    //                    string group = value.ToString();
    //                    string[] groupParts = group.Split(',');
    //                    foreach (string part in groupParts)
    //                    {
    //                        if (part.StartsWith("CN="))
    //                        {
    //                            var hGroup = nGroupManager.Find(part.Replace("CN=", ""));
    //                            if (hGroup != null)
    //                                usuario.Permisos.Add(hGroup.fk_Perfil);
    //                        }
    //                    }
    //                }

    //                // Nombre
    //                if (colProperties["name"].Count > 0)
    //                    usuario.Nombres = colProperties["name"][0].ToString();

    //                if (colProperties["mail"].Count > 0)
    //                    usuario.Correo = colProperties["mail"][0].ToString();

    //                if (colProperties["streetaddress"].Count > 0)
    //                    usuario.Direccion = colProperties["streetaddress"][0].ToString();

    //                if (colProperties["telephonenumber"].Count > 0)
    //                    usuario.Telefono = colProperties["telephonenumber"][0].ToString();
    //            }

    //            usuario.Valido = true;
    //        }
    //        catch (System.Runtime.InteropServices.COMException ex)
    //        {
    //            switch (ex.ErrorCode)
    //            {
    //                case -2147023570:// Usuario o contraseña inválido
    //                    usuario.Valido = false;
    //                    break;

    //                default:
    //                    throw new Exception("Error LDAP: " + ex.Message, ex);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error LDAP: " + ex.Message, ex);
    //        }
    //        finally
    //        {
    //            if (entry != null) entry.Close();
    //        }

    //        return usuario;
    //    }
    //}
}