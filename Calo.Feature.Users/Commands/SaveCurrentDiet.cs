using Calo.Core.Models;
using Calo.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Feature.Users.Commands;

public class SaveCurrentDiet
{
    public class Command : IRequest<RequestStatus>
    {
        public Guid DietId { get; set; }
        public Guid Userid { get; set; }
    }

    public class Handler : IRequestHandler<Command, RequestStatus>
    {
        private readonly CaloContext dbContext;

        public Handler(CaloContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RequestStatus> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await this.dbContext.Users.Where(x => x.Id == command.Userid).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                return new RequestStatus(false, "User not logged in");
            }

            user.SelectedDietId = command.DietId;
            user.ModifiedDate = DateTime.Now;
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return new RequestStatus(true, "Updated user diet");
        }
    }
}
