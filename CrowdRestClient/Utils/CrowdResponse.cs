using RestSharp;

namespace CrowdRestClient.Utils
{
    public class CrowdResponse<T> : RestResponse<T>
    {
        public CrowdResponse(IRestResponse source, T data)
        {
            ContentEncoding = source.ContentEncoding;
            ContentLength = source.ContentLength;
            ContentType = source.ContentType;
            Cookies = source.Cookies;
            ErrorMessage = source.ErrorMessage;
            ErrorException = source.ErrorException;
            Headers = source.Headers;
            RawBytes = source.RawBytes;
            ResponseStatus = source.ResponseStatus;
            ResponseUri = source.ResponseUri;
            ProtocolVersion = source.ProtocolVersion;
            Server = source.Server;
            StatusCode = source.StatusCode;
            StatusDescription = source.StatusDescription;
            Request = source.Request;

            Data = data;
        }
    }
}