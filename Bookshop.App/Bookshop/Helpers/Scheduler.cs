using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Helpers
{
    public interface IJobScheduler
    {
        void Schedule();
    }
    public interface IScheduledJob
    {
        void Execute( CancellationToken token);
    }
}
