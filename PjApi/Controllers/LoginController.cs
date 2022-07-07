using PjApi.Models;
using System;
using System.Web.Http;
namespace PjApi.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody] Login l)
        {
            try
            {
                CatalogAccessController.UserDetails u = CatalogAccessController.CatalogAccess.Login(
                    l.userName, 
                    EncryptorController.StringCipher.Encrypt(l.password)
                    );
                return Ok(u);

            }
            catch (Exception ex)
            {
                CatalogAccessController.CatalogAccess.Log_Error("DefaultController: " + ex.Message);
                return NotFound();
            }
        }
    }
}
