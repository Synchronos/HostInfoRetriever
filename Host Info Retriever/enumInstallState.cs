using System;

namespace Host_Info_Retriever
{
    public enum enumInstallState : int
    {
        Bad_Configuration = -6,
        Invalid_Argument = -2,
        Unknown_Package = -1,
        Advertised = 1,
        Absent = 2,
        Installed = 5,
    }
}
