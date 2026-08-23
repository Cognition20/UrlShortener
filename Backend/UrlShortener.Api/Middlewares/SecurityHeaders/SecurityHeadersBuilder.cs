namespace UrlShortener.Api.Middlewares.SecurityHeaders;

public class SecurityHeadersBuilder
{
    private readonly SecurityHeadersPolicy _policy = new SecurityHeadersPolicy();

        public SecurityHeadersBuilder AddDefaultSecurePolicy()
        {
            RemoveHeader(SecurityHeader.XPoweredBy);
            RemoveHeader(SecurityHeader.Server);
            
            AddXContentTypeOptions();
            AddReferrerPolicy();
            AddContentSecurityPolicy();
            AddXFrameOptions();
            
            return this;
        }
        
        private SecurityHeadersBuilder AddXContentTypeOptions()
        {
            _policy.SetHeaders[SecurityHeader.XContentTypeOptions] = SecurityHeaderValues.XContentTypeOptions;
            return this;
        }

        private SecurityHeadersBuilder AddReferrerPolicy()
        {
            _policy.SetHeaders[SecurityHeader.ReferrerPolicy] = SecurityHeaderValues.ReferrerPolicy;
            return this;
        }
        
        private SecurityHeadersBuilder AddContentSecurityPolicy()
        {
            _policy.SetHeaders[SecurityHeader.ContentSecurityPolicy] = SecurityHeaderValues.ContentSecurityPolicy;
            return this;
        }
        
        private SecurityHeadersBuilder AddXFrameOptions()
        {
            _policy.SetHeaders[SecurityHeader.XFrameOptions] =
                SecurityHeaderValues.XFrameOptions;

            return this;
        }

        public SecurityHeadersBuilder AddCustomHeader(string header, string value)
        {
            _policy.SetHeaders[header] = value;
            return this;
        }

        private SecurityHeadersBuilder RemoveHeader(string header)
        {
            _policy.RemoveHeaders.Add(header);
            return this;
        }

        public SecurityHeadersPolicy Build()
        {
            return _policy;
        }
}