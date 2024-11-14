using Microsoft.AspNetCore.Mvc;

namespace TasksAPI
{
    [ApiController]
    [Route("[controller]")]
    public class ExercisesController : ControllerBase
    {
        public static List<string> StaticStringList = new List<string>
            {
                "Value1",
                "Value2",
                "Value3"
            };

        [HttpGet("getStringList")]
        public IActionResult GetStringList()
        {
            return Ok(StaticStringList);
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            return Ok($"Received ID: {id}");
        }

        [HttpGet("sum/id")]
        public IActionResult GetSum(int id, double param1, double param2)
        {
            double sum = param1 + param2;
            return Ok($" {id}: {sum}");
        }

        [HttpPost("sumList")]
        public IActionResult SumListOfNumber(int id, List<double> numbers)
        {
            if (numbers == null || numbers.Count == 0)
            {
                return BadRequest("List of numbers is empty");
            }
            double sum = numbers.Sum();
            return Ok($"{id}: {sum}");
        }

        [HttpPut("update/index")]
        public IActionResult UpdateItem(int index, [FromBody] string newValue)
        {
            if(newValue == null)
            {
                return BadRequest("New value is null");
            }
            if(index < 0 || index >= StaticStringList.Count)
            {
                return BadRequest("Index out of range");
            }
            StaticStringList[index] = newValue;
            return Ok(StaticStringList);
        }
        [HttpDelete("delete/index")]
        public IActionResult DeleteItem(int index)
        {
            if(index < 0 || index >= StaticStringList.Count)
            {
                return BadRequest("Index out of range");
            }
            StaticStringList.RemoveAt(index);
            return Ok(StaticStringList);
        }
        
    }
}
