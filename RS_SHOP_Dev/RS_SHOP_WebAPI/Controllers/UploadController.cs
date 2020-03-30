using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RS_SHOP_WebAPI.Controllers
{
    public class UploadController : ApiController
    {
        [Route("api/Files/Upload")]
        public async Task<string> Post()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if(httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedfile = httpRequest.Files[file];

                        var filename = postedfile.FileName.Split('\\').LastOrDefault().Split
                            ('/').LastOrDefault();

                        var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + filename);
                        postedfile.SaveAs(filePath);

                        return "/Uploads/" + filename;
                    }
                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}