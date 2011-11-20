using System;

namespace Aspect
{
    public class DbException :Exception
    {
        public DbException():base(){}

        public DbException(string msg):base(msg){}
    }
}