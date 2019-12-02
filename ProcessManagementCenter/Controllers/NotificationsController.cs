using Microsoft.AspNetCore.Mvc;
using ProcessManagementCenter.Domain;

namespace ProcessManagementCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Notification> PostNotification(Notification notification)
        {
            return Ok(notification);
        }
    }
}
