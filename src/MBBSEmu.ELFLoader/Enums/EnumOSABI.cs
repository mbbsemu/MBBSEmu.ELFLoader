namespace MBBSEmu.ELFLoader.Enums
{
    /// <summary>
    /// 	Identifies the target operating system ABI.
    /// </summary>
    public enum EnumOSABI : byte
    {
        SystemV = 1,
        HPUX = 2,
        NetBSD = 3,
        Linux = 4,
        GNUHurd = 5,
        Solaris = 6,
        AIX = 7,
        IRIX = 8,
        FreeBSD = 9,
        Tru64 = 10,
        NovellModesto = 11,
        OpenBSD = 12,
        OpenVMS = 13,
        NonStopKernel = 14,
        AROS = 15,
        FenixOS = 16,
        CloudABI = 17,
        OpenVOS = 18
    }
}
