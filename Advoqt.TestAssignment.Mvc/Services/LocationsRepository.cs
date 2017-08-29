namespace Advoqt.TestAssignment.Mvc.Services
{
    using System.Collections.Generic;
    using Abstract;
    using Models;

    /// <summary>
    /// Location repository.
    /// </summary>
    public class LocationsRepository
        : ILocationsRepository
    {
        private readonly IDataAccess _dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationsRepository"/> class.
        /// </summary>
        /// <param name="dataAccess">The database access.</param>
        public LocationsRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// Reads all the locations.
        /// </summary>
        /// <returns>
        /// The sequence of locations.
        /// </returns>
        public IEnumerable<LocationModel> ReadAllLocations()
        {
            return _dataAccess.GetSequence<LocationModel>(@"
SELECT
    Id, Name, ImageUrlSuffix, ImageDescription
FROM dbo.City
ORDER BY
    Name");
        }
    }
}