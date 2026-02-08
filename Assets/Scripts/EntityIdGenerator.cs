using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public static class EntityIdGenerator
    {
        private static int nextId = 1;
        public static int Next() => nextId++;
    }


