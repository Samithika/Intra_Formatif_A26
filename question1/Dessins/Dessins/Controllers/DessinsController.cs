using Dessins.Events;
using Microsoft.AspNetCore.Mvc;

namespace Dessins.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DessinsController : ControllerBase
    {
        [HttpGet]
        // Rien à modifier ici, juste un exemple de dessin très simple
        public ActionResult GetDrawing1()
        {
            var drawSquare = new DrawSquare(2, 2);
            
            return Ok(drawSquare);
        }

        // TODO: Il faut ajouter une nouvelle action pour dessiner la séquence mentionnée dans l'énoncé
        [HttpGet]
        public ActionResult GetDrawing2()
        {
            // Commence par dessiner un cercle bleu et on ajoute les autres événements subséquents dans sa liste d'événements
            var startDrawing = new DrawCircle(1, 1);
            startDrawing.DrawingEvents = new List<DrawingEvent>
            {
                new Wait(3)
            };

            // Event pour dessiner les carrés rouges
            var goRedEvent = new ChangeColor("red");
            goRedEvent.DrawingEvents = new List<DrawingEvent>
            {
                new DrawSquare(0, 2),
                new DrawSquare(2, 2),
                new Wait(1)
            };

            startDrawing.DrawingEvents.Add(goRedEvent);

            // Event pour dessiner l'étoile jaune
            var goYellow = new ChangeColor("yellow");
            goYellow.DrawingEvents = new List<DrawingEvent>
            {
                new DrawStar(1, 3, 20)
            };

            startDrawing.DrawingEvents.Add(goYellow);

            return Ok(startDrawing);
        }
    }
}
