using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Application.Queries.GetAllVehicles
{
    public class GetAllVehiclesQuery : IRequest<IEnumerable<GetAllVehiclesQueryHandlerResult>>
    {
    }
}
