using HighSchool.OAuth.Models;
using IdentityServer3.Core.Extensions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HighSchool.OAuth.Controllers
{
    public class TermsController : Controller
    {
        // GET: Terms
        public ActionResult Index()
        {
            return View(new Message());
        }

        [HttpPost]
        public async Task<ActionResult> Index(bool acceptTerms)
        {
            if (!acceptTerms)
                return View(new Message { message = "Please accept the terms and conditions to continue" });

            var partialLogin = await Request.GetOwinContext()
                .Environment.GetIdentityServerPartialLoginAsync();

            if (partialLogin == null)
                return Redirect("/");

            var resumeUrl = await Request.GetOwinContext()
                .Environment.GetPartialLoginResumeUrlAsync();

            return Redirect(resumeUrl);
        }
    }
}