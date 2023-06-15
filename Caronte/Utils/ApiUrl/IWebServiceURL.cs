namespace Caronte.Utils.ApiUrl;

public interface IWebServiceURL
{
    public string GetWebServiceUrl();

    #region Login
    public string ValidateClient();
    #endregion

    #region errors
    public string SendErrorToServer();
    #endregion
}
