//using AppliWorkFlowProject.BusinessLogic.Interfaces.InterfacesRepository;
//using AppliWorkFlowProject.DataAccess.Data;
//using MongoDB.Driver;

//namespace AppliWorkFlowProject.DataAccess.Repository
//{
//    public class WorkflowRepository : IWorkFlowRepository
//    {
//        private readonly IworkFlowCollection<WorkFlow> _database;

//        public WorkflowRepository(IworkFlowCollection<WorkFlow> database)
//        {
//            _database = database;
//        }

//        // Récupère tous les workflows de manière asynchrone
//        public async Task<IEnumerable<WorkFlow>> GetAllAsync(CancellationToken cancellationToken)
//        {
//            return await _database.Collection.FindAsync(filter => true, cancellationToken: cancellationToken).ToListAsync();
//        }

//        // Récupère un workflow par ID de manière asynchrone
//        public async Task<WorkFlow> GetByIdAsync(string id, CancellationToken cancellationToken)
//        {
//            var filter = Builders<WorkFlow>.Filter.Eq(w => w.Id, id);
//            return await _database.Collection.FindAsync(filter, cancellationToken: cancellationToken).FirstOrDefaultAsync();
//        }

//        // Ajoute un workflow de manière asynchrone
//        public async Task<WorkFlow> AddAsync(WorkFlow workflow, CancellationToken cancellationToken)
//        {
//            await _database.Collection.InsertOneAsync(workflow, cancellationToken: cancellationToken);
//            return workflow;
//        }

//        // Met à jour un workflow existant de manière asynchrone
//        public async Task<bool> UpdateAsync(WorkFlow workflow, CancellationToken cancellationToken)
//        {
//            var filter = Builders<WorkFlow>.Filter.Eq(w => w.Id, workflow.Id);
//            var update = Builders<WorkFlow>.Update
//                .Set(w => w.Name, workflow.Name)
//                .Set(w => w.Description, workflow.Description)
//                .Set(w => w.CurrentStep, workflow.CurrentStep)
//                .Set(w => w.Status, workflow.Status);

//            var updateResult = await _database.Collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
//            return updateResult.IsModifiedCountChanged;
//        }

//        // Supprime un workflow par ID de manière asynchrone
//        public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
//        {
//            var filter = Builders<Workflow>.Filter.Eq(w => w.Id, id);
//            var deleteResult = await _database.Collection.DeleteOneAsync(filter, cancellationToken: cancellationToken);
//            return deleteResult.DeletedCount > 0;
//        }
//    }
//}
