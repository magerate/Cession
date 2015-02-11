using System;

namespace Cession.Projects
{
    public class ProjectInfo
    {
        public string Name{ get; set; }

        public DateTime CreateTime{ get; set; }

        public DateTime LastModified{ get; set; }

        public string Path{ get; set; }

        public ProjectInfo ()
        {
            CreateTime = DateTime.Now;
            LastModified = DateTime.Now;
        }
    }
}

