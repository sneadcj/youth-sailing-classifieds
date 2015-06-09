using YouthSailingClassifieds.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouthSailingClassifieds.Controllers
{
    public class BaseController : Controller
    {
        protected IUow _uow;
    }
}