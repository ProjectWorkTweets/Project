using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Bogles.Charts.Data;

namespace Bogles.Charts.Web.Controllers
{
    public class ChartsController : ApiController
    {
        // GET: api/Products/5
        public IHttpActionResult Get(string id)
        {

            DataAccess data = new DataAccess();

            var countryData = data.GetChartData(id);

            if (countryData == null)
                return NotFound();

            return Ok(countryData);


        }
    }
}
