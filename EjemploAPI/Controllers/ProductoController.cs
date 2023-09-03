using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//agrego referencias
using Microsoft.EntityFrameworkCore;
using EjemploAPI.Models;

namespace EjemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        //declaro un objeto dbcontext para usar las operaciones CRUD para producto y categoria
        public readonly DbapiContext _dbContext;

        //creo constructor que recibo un contexto y lo asigno al objeto creado en la línea 15
        public ProductoController(DbapiContext _context)
        {
            _dbContext = _context;
        }


        [HttpGet]  //creo el tipo de api 
        [Route("Lista")]  //creo la ruta para que puedan llamar a la api

        public IActionResult Listar()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                //asigno a la lista lo que trae el dbcontext de la tabla productos, que me incluya la tabla categoria y lo liste
                lista = _dbContext.Productos.Include(c => c.oCategoria).ToList();

                //devuelvo la lista y me muestra el código 200 que indica que todo está bien, un mensaje de ok y un response con la lista
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = lista });
            }
            catch (Exception ex)
            {
                //devuelvo la lista y me muestra el código 200 que indica que todo está bien, el error y un response con la lista
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = lista });
            }
        }

        [HttpGet]  //creo el tipo de api 
        [Route("Obtener/{idProducto}")]  //creo la ruta para que puedan llamar a la api

        public IActionResult Obtener(int idProducto)
        {
            //al obj oProducto le asigno lo que el dbcontext encuentre con el mismo id
            Producto oProducto = _dbContext.Productos.Find(idProducto); 

            //si no encuentra ningún producto con ese id, lanza este response
            if (oProducto == null)
                return BadRequest("Producto no encontrado");

            try
            {
                //asigno al obj tipo producto lo que el dbcontext encuentre con el idProducto recibido que coincida con un idProducto del objeto de la tabla
                oProducto = _dbContext.Productos.Include(c => c.oCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();

                //devuelvo la lista y me muestra el código 200 que indica que todo está bien, un mensaje de ok y un response con la lista
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = oProducto });
            }
            catch (Exception ex)
            {
                //devuelvo la lista y me muestra el código 200 que indica que todo está bien, el error y un response con la lista
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = oProducto });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Producto prod) 
        {
            try
            {
                _dbContext.Productos.Add(prod);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok"});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }
        }





    }
}
