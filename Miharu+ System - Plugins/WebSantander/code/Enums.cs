namespace  WebSantander.code
{
    public enum RequestType
    {
        Ajax,
        Post
    }

    public class consts
    {
        public const string SessionToken = "_SessionToken__";
        public const string SessionManager = "_SessionManager__";
        public const string SessionData = "_SessionData__";
                
        public const string SessionObraActual = "_SessionObraActual__";
        public const string SessionWebReport = "WebReport";


        public const string SessionFile = "_SessionFile__";        
        public const string SessionImage = "_SessionImage__";
        public const string CompanyImage = "_CompanyImage__";

        public const string EmptyCompanyLogo = "~/styles/frame/default_client_logo.png";
        
        public const string EmptyImage = "~/images/basic/empty_image.png";
        public const string EmptyImageContentType = "image/png";        
        public const string EmptyFileContentType = "text/plain";

        public const string BackPage = "_Back_Page__";

        public const string UserPagesWithAccess = "_UserPagesWithAccess__";
    }
}