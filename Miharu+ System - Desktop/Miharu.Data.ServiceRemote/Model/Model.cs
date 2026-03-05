using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Miharu.Data.ServiceRemote.Model
{
    public class PostRequestModel
    {
        public string JsonPayload { get; set; }
        public string Url { get; set; }
    }
    public class QueryParameter
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class QueryRequest
    {
        public string name { get; set; }
        public QueryRequestType queryRequestType { get; set; }
        public QueryResponseType queryResponseType { get; set; }
        public List<QueryParameter> parameters { get; set; }
    }

    public enum QueryRequestType
    {
        StoredProcedure,
        Table
    }

    public class QueryResponse
    {
        public DataTable dataTable { get; set; }
        public object scalar { get; set; }
        public int rows { get; set; }
        public bool success { get; set; }
        public string error { get; set; }
    }

    public enum QueryResponseType
    {
        Table,
        Scalar,
        NonQuery
    }
}