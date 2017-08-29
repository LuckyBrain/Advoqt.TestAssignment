namespace Advoqt.TestAssignment.Mvc.Abstract
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Locations repository contract.
    /// </summary>
    public interface ILocationsRepository
    {
        /// <summary>
        /// Reads all the locations.
        /// </summary>
        /// <returns>The sequence of locations.</returns>
        IEnumerable<LocationModel> ReadAllLocations();
    }
}