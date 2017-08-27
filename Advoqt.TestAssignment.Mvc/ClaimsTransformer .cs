namespace Advoqt.TestAssignment.Mvc
{
    using System;
    using System.Linq;
    using System.Security.Claims;

    public class ClaimsTransformer : ClaimsAuthenticationManager
    {
        private readonly IClaimsRepository _claimsRepository;

        public ClaimsTransformer(IClaimsRepository claimsRepository)
        {
            _claimsRepository = claimsRepository ?? throw new ArgumentNullException(nameof(claimsRepository));
        }

        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            var identity = incomingPrincipal.Identity;
            return !identity.IsAuthenticated
                ? base.Authenticate(resourceName, incomingPrincipal)
                : LoadUserClaims(identity.Name);
        }

        private ClaimsPrincipal LoadUserClaims(string userName)
        {
            // load claim entities from repository
            var claimEntities = _claimsRepository
                .GetClaims(userName)
                .ToList();

            // add missing required claim entities
            var requiredClaimTypes = new[] { ClaimTypes.GivenName, ClaimTypes.Name, ClaimTypes.NameIdentifier };
            var existingClaimTypes = claimEntities.Select(entity => entity.TypeUrl);
            var missingClaimTypes = requiredClaimTypes.Except(existingClaimTypes);
            claimEntities
                .AddRange(missingClaimTypes.Select(missingClaimType => new ClaimEntity { TypeUrl = missingClaimType, Value = userName }));

            // convert claim entities to actual claims
            var claims = claimEntities
                .Select(entity => new Claim(entity.TypeUrl, entity.Value));

            return new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType: "Custom"));
        }
    }
}
