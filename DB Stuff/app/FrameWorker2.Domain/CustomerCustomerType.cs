using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpLite.Domain;

namespace FrameWorker2.Domain
{
    public class CustomerCustomerType : Entity
    {
        public virtual Customer Customer { get; set; }
        public virtual CustomerType CustomerType { get; set; }
    }
}
