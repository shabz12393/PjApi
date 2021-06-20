using PjApi.App_Code;
using PjApi.App_Code.dsMainTableAdapters;
using PjApi.Models;
using System;
using System.Web.Http;
namespace PjApi.Controllers
{
    public class LoginController : ApiController
    {
        usersTableAdapter usersAdapter = new usersTableAdapter();
        errorsTableAdapter errorAdapter = new errorsTableAdapter();
        private dsMain dataset = new dsMain();
        [HttpPost]
        public IHttpActionResult Post([FromBody] Login l)
        {
            Users u = new Users();
            try
            {
                usersAdapter.FillBy(dataset.users, l.userName, EncryptorController.StringCipher.Encrypt(l.password));
                int userId = int.Parse(dataset.users.Rows[0][1].ToString());
                u.userId = userId;
                int roleId = int.Parse(dataset.users.Rows[0][4].ToString());
                u.role = usersAdapter.fnGetRole(roleId).ToString();
                u.fullName = usersAdapter.fnGetName(userId, u.role).ToString();
                return Ok(u);

            }
            catch (Exception ex)
            {
                errorAdapter.Insert("DefaultController: " + ex.Message);
                return NotFound();
            }
        }
    }
}
