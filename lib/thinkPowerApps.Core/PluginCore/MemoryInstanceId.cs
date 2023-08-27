using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkCrm.Core.PluginCore
{
    
    public sealed class MemoryInstanceId
    {
        private static readonly MemoryInstanceId instance = new MemoryInstanceId();

        private readonly Guid _id;
        private readonly DateTime _loadedtime;

        static MemoryInstanceId()
        {
        }

        private MemoryInstanceId()
        {
            _id = Guid.NewGuid();
        }

        public Guid Id => _id;

        public DateTime LoadedTime => _loadedtime;

        public static MemoryInstanceId Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
