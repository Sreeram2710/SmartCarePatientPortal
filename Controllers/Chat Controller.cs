using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class ChatController : Controller
{
    public IActionResult PatientChat()
    {
        ViewBag.ReceiverRole = "Doctor";
        return View("Chat");
    }

    public IActionResult DoctorChat()
    {
        ViewBag.ReceiverRole = "Patient";
        return View("Chat");
    }
}
