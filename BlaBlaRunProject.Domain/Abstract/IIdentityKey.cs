using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMSWebApplication.Domain.Abstract
{
    public interface IIdentityKey<TKey>
    {
        TKey Id { get; set; }
    }
}
