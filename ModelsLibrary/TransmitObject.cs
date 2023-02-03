using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dtoLibrary
{
    public record class TransmitObject
    {
        public TransmitObject(Guid Id, string status, string message, byte[] key )
        {
            ObjectID = Id;
            Key = key;
            Status = status;
            Message = message;
        }
        public Guid ObjectID { get; init; }
        public string Status { get; init; }
        public byte[] Key { get; init; }
        public string Message { get; init; }
    }
}
