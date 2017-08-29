namespace Advoqt.TestAssignment.Mvc.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Abstract;
    using Models;
    using Properties;
    using Services;

    public class LocationsController : ApiController
    {
        private readonly ILocationsRepository _locationsRepository;

        public LocationsController()
            : this(locationsRepository: null)
        {
        }

        public LocationsController(ILocationsRepository locationsRepository)
        {
            _locationsRepository = locationsRepository ?? new LocationsRepository(new DataAccess(Settings.Default.ConnectionString));
        }

        // GET api/<controller>
        public IEnumerable<LocationModel> Get()
        {
            return _locationsRepository.ReadAllLocations();
        }
    }
}