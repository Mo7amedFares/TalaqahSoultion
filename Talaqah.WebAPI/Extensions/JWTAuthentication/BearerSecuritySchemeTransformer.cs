using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Talaqah.WebAPI.Extensions.JWTAuthentication
{
    public class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
    {
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            // 1. Define the Bearer Security Scheme
            var securityScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Enter your JWT token in the format: Bearer {your_token}"
            };

            // 2. Add it to the document components
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
            document.Components.SecuritySchemes["Bearer"] = securityScheme;

            // 3. Apply it globally to all endpoints using a document-linked reference
            var schemeReference = new OpenApiSecuritySchemeReference("Bearer", document);

            document.Security ??= [];
            document.Security.Add(new OpenApiSecurityRequirement
            {
                { schemeReference, new List<string>() }
            });

            return Task.CompletedTask;
        }
    }
}
