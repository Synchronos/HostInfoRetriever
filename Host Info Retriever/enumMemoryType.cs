using System;

namespace Host_Info_Retriever
{
    public enum enumMemoryType : int
    {
        Unknown = 0,
        Other = 1,
        DRAM = 2,
        Synchronous_DRAM = 3,
        Cache_DRAM = 4,
        EDO = 5,
        EDRAM = 6,
        VRAM = 7,
        SRAM = 8,
        RAM = 9,
        ROM = 10,
        Flash = 11,
        EEPROM = 12,
        FEPROM = 13,
        EPROM = 14,
        CDRAM = 15,
        _3DRAM = 16,
        SDRAM = 17,
        SGRAM = 18,
        RDRAM = 19,
        DDR = 20
    }
}
