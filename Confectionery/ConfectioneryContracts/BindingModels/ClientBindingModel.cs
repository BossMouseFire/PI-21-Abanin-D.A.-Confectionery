using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConfectioneryContracts.BindingModels
{
    public class ClientBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string FIO { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
