namespace Advoqt.TestAssignment.Mvc
{
    using System.Collections.Generic;

    public interface IClaimsRepository
    {
        IEnumerable<ClaimEntity> GetClaims(string userName);
    }
}