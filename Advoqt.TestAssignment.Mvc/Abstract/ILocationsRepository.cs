namespace Advoqt.TestAssignment.Mvc.Abstract
{
    using System.Collections.Generic;
    using Models;

    public interface ILocationsRepository
    {
        IEnumerable<LocationModel> ReadAllLocations();
    }
}