using Microsoft.AspNetCore.Mvc;

namespace PetNameGenerator.Controllers
{
    [ApiController]
    [Route("api/generate")]
    public class PetNameController : ControllerBase
    {
        private string[] dog = { "Buddy", "Max", "Charlie", "Rocky", "Rex" };
        private string[] cat = { "Whiskers", "Mittens", "Luna", "Simba", "Tiger" };
        private string[] bird = { "Tweety", "Sky", "Chirpy", "Raven", "Sunny" };

        [HttpPost]
        public IActionResult Generate([FromQuery] string animalType, [FromQuery] bool? twoPart)
        {
            if (string.IsNullOrEmpty(animalType))
            {
                return BadRequest(new { error = "animalType is required (dog, cat, or bird)." });
            }

            string[] nameList;
            string animalTypeLower = animalType.ToLower();

            switch (animalTypeLower)
            {
                case "dog":
                    nameList = dog;
                    break;
                case "cat":
                    nameList = cat;
                    break;
                case "bird":
                    nameList = bird;
                    break;
                default:
                    return BadRequest(new { error = "Invalid animalType. The input must be 'dog', 'cat', or 'bird'." });
            }

            Random rnd = new Random();
            string generatedName;

            if (twoPart == true)
            {
                string firstPetName = nameList[rnd.Next(nameList.Length)];
                string secondPetName = nameList[rnd.Next(nameList.Length)];
                generatedName = firstPetName + " " + secondPetName;
            }
            else
            {
                generatedName = nameList[rnd.Next(nameList.Length)];
            }

            return Ok(new { petName = generatedName });
        }

    }
}
