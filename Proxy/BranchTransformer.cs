using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Transforms;

namespace Proxy;

// Based on https://microsoft.github.io/reverse-proxy/articles/direct-forwarding.html

public class BranchTransformer : HttpTransformer
{
    public override async ValueTask TransformRequestAsync(HttpContext httpContext, HttpRequestMessage proxyRequest, string destinationPrefix,
        CancellationToken cancellationToken)
    {
        await base.TransformRequestAsync(httpContext, proxyRequest, destinationPrefix, cancellationToken);

        var pathParts = httpContext.Request.Path.ToUriComponent().Split('/');

        string? branch = pathParts.FirstOrDefault();

        if (string.IsNullOrEmpty(branch))
        {
            return;
        }
        
        string restOfPath = "/" + string.Join('/', pathParts.Skip(1)).TrimStart('/');
        PathString.FromUriComponent(new Uri(restOfPath));

        proxyRequest.RequestUri =
            RequestUtilities.MakeDestinationAddress(branch, restOfPath,
                httpContext.Request.QueryString);
    }
}