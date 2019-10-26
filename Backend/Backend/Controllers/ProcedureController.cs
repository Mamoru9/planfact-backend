using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Backend.Controllers
{
    public class ProcedureController : ApiController
    {
        //Получает сумму заказов со статусом выполнен по каждому клиенту, произведённых в день рождения клиента
        //GET: api/Procedure/sum
        [ResponseType(typeof(SumInBirthday_Result))]
        [Route("api/Procedure/sum")]
        public IHttpActionResult GetSumInBirthday()
        {
            using (backendEntities entities = new backendEntities())
            {
                var procedure = entities.SumInBirthday().ToList();
                if (procedure == null)
                {
                    return NotFound();
                }
                return Ok(procedure);
            }
        }

        //Получает список часов от 00:00 до 24:00 в порядке убывания со средним чеком за каждый час
        //GET: api/Procedure/AvrSum/26/11/1999
        [ResponseType(typeof(AverageSum_Result))]
        [Route("api/Procedure/AvrSum/{dd:int}/{mm:int}/{yy:int}")]
        public IHttpActionResult GetAvrSum(int yy, int mm, int dd)
        {
            using (backendEntities entities = new backendEntities())
            {
                var procedure = entities.AverageSum(dd, mm, yy).ToList();
                if (procedure == null)
                {
                    return NotFound();
                }
                return Ok(procedure);
            }
        }
    }
}
