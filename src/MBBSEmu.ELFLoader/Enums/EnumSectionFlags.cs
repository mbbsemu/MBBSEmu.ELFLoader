using System;

namespace MBBSEmu.ELFLoader.Enums
{
    [Flags]
    public enum EnumSectionFlags
    {
        SHF_WRITE = 1,
        SHF_ALLOC = 1 << 1,
        SHF_EXECINSTR = 1 << 2,
        SHF_MERGE = 1 << 4,
        SHF_STRINGS = 1 << 5,
        SHF_INFO_LINK = 1 << 6,
        SHF_LINK_ORDER = 1 << 7,
        SHF_OS_NONCONFORMING = 1 << 8,
        SHF_GROUP = 1 << 9,
        SHF_TLS = 1 << 10,
    }
}
