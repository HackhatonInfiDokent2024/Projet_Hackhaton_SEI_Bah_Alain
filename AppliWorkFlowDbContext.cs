using AppliWorkFlowProject.DataAccess.Models;

namespace AppliWorkFlowProject.BusinessLogic.Services
{
    public class AppliWorkFlowDbContext
    {
        public object Users { get; internal set; }

        internal object Entry(Users user)
        {
            throw new NotImplementedException();
        }

        internal Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}