using Grpc.Core;
using Testowe_GRPC.Context;
using Microsoft.EntityFrameworkCore;
using Testowe_GrpcService;
using Testowe_GRPC.Models;

namespace Testowe_GRPC.Services
{
    public class DecryptorService:Decryptor.DecryptorBase
    {

        MessageDbContext db;
        IDecryptionService _decryptionService;
        private readonly ILogger<DecryptorService> _logger;
        public DecryptorService(MessageDbContext db, IDecryptionService decryptionService, ILogger<DecryptorService> logger)
        {
            this.db = db;
            _decryptionService = decryptionService;
            _logger = logger;
        }

        public override async Task<Response> Decrypt(Request request, ServerCallContext context)
        {
            var key = request.Key;
            var id = request.Id;

            var item = await db.Messages.FirstOrDefaultAsync(m => m.ID.ToString().Equals(id));
            if (item == default(Message))
            {
                return await Task.FromResult(new Response
                {
                    Message = "Messge is missing!",
                    Status = "I'm teapot"
                });
            }
            string message = item.EncryptedText;

            //Delete junk from database
            db.Messages.Remove(item);
            db.SaveChanges();

            return await _decryptionService.Decrypt(message, key);
        }

    }
}
