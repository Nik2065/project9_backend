using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Project9CustomException : Exception
    {
        public Project9CustomException(string message):base(message)
        { 
        }
    }
}
