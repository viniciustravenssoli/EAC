using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Domain.Entities
{
    public class Transfer : Base
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string? SenderId { get; set; } // Id do remetente
        public string ReceiverId { get; set; } // Id do destinatário

        public ApplicationUser? Sender { get; set; } // Relacionamento com o remetente
        public ApplicationUser Receiver { get; set; } // Relacionamento com o destinatário
    }
}
