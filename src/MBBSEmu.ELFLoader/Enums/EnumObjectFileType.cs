namespace MBBSEmu.ELFLoader.Enums
{
    public enum EnumObjectFileType : ushort
    {
        /// <summary>
        ///     Unknown
        /// </summary>
        ET_NONE = 0,

        /// <summary>
        ///     Relocation Table
        /// </summary>
        ET_REL = 1,

        /// <summary>
        ///     Executable file
        /// </summary>
        ET_EXEC = 2,

        /// <summary>
        ///     Shared object
        /// </summary>
        ET_DYN = 3,

        /// <summary>
        ///     Core file
        /// </summary>
        ET_CORE = 4,

    }
}
