namespace Advoqt.TestAssignment.Mvc.Services
{
    using System.Collections.Generic;
    using Abstract;
    using Models;

    public class LocationsRepository
        : ILocationsRepository
    {
        private readonly IDataAccess _dataAccess;

        public LocationsRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

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