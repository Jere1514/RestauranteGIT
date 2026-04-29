using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class RestauranteController : Controller
    {
        private readonly BL.Restaurante _restauranteBL;

        public RestauranteController(DL.RestauranteContext context)
        {
            _restauranteBL = new BL.Restaurante(context);
        }

        public IActionResult GetAll()
        {
            return View();
        }

        public JsonResult GetAllRestaurante()
        {
            ML.Result result = _restauranteBL.GetAllRestaurante();

            if (result.Correct)
            {
                return Json(result.Objects);
            }
            else
            {
                return Json(new { error = "No se pudieron obtener los restaurantes." });
            }
        }

        public JsonResult Delete(int id)
        {
            ML.Result result = _restauranteBL.Delete(id);
            if (result.Correct)
            {
                return Json(new { succes = true });

            }
            else
            {
                return Json(new { error = result.ErrorMessage });
            }
        }

        public JsonResult GetById(int id)
        {
            ML.Result result = _restauranteBL.GetById(id);

            if (result.Correct)
            {
                return Json(result.Object);
            }
            else
            {
                return Json(new { error = result.ErrorMessage });
            }
        }
    }
}
