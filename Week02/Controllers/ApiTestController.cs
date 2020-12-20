using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using Week02.Models;

namespace Week02.Controllers
{
    public class ApiTestController : ApiController
    {
        [HttpGet]
        [Route("detail")]
        public IHttpActionResult GetDetail(string id)
        {
            Model1 db = new Model1();
            SANPHAM detail = db.SANPHAMs.Where(abc => abc.MASP == id).SingleOrDefault();
            return Json(detail);
        }
    }
}
