using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    public abstract class Entity
    {
        public string Name { get; set; }
        public string CodeName { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}
