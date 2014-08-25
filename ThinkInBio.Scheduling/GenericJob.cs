using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Scheduling
{
    public abstract class GenericJob : IJob
    {
        public event Action Running;

        public event Action Completed;

        public event Action<Exception> Error;

        protected abstract void Execute();

        public void Run()
        {
            try
            {
                if (Running != null)
                {
                    Running();
                }
                this.Execute();
                if (Completed != null)
                {
                    Completed();
                }
            }
            catch (Exception ex)
            {
                if (Error != null)
                {
                    Error(ex);
                }
            }
        }
    }
}
