using System.ComponentModel;

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
        [Description("Relocation Table")]
        ET_REL = 1,

        /// <summary>
        ///     Executable file
        /// </summary>
        [Description("Executable File")]
        ET_EXEC = 2,

        /// <summary>
        ///     Shared object
        /// </summary>
        [Description("Shared Object")]
        ET_DYN = 3,

        /// <summary>
        ///     Core file
        /// </summary>
        [Description("Core File")]
        ET_CORE = 4,

    }
}
