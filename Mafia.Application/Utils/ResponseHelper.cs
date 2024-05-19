using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Application.Utils
{
    public static class ResponseHelper
    {
        public static ActionResult BaseTemplateResult(bool response)
        {
            if (response)
            {
                return new OkObjectResult("Success");
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}
