using System;

namespace Host_Info_Retriever
{
    public enum enumDomainRole : int
    {
        Standalone_Workstation = 0,
        Member_Workstation = 1,
        Standalone_Server = 2,
        Member_Server = 3,
        Backup_Domain_Controller = 4,
        Primary_Domain_Controller = 5
    }

}
