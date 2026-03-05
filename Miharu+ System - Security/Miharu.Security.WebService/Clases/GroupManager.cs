using System;
using DBSecurity.SchemaSecurity;

namespace Miharu.Security.WebService.Clases
{
    [Serializable]
    public class GroupManager
    {
        public TBL_LDAPDataTable Gropus { get; private set; }

        public GroupManager(TBL_LDAPDataTable nGropus)
        {
            this.Gropus = nGropus;
        }

        public TBL_LDAPRow Find(string nLdapGrupo)
        {
            foreach (var Group in this.Gropus)
            {
                if (Group.Grupo_LDAP.ToUpper() == nLdapGrupo.ToUpper())
                    return Group;

            }
            return null;
        }
    }
}