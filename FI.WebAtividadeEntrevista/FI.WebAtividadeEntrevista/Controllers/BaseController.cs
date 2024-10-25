﻿using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BaseController : Controller
    {
        protected Logger logger;

        public BaseController()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
    }
}