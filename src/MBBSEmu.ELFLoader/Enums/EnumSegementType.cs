namespace MBBSEmu.ELFLoader.Enums
{
    /// <summary>
    ///     	Identifies the type of the Segment
    /// </summary>
    public enum EnumSegementType : uint
    {
        /// <summary>
        ///     Program header table entry (Unused)
        /// </summary>
        PT_NULL = 0x0,

        /// <summary>
        ///     Loadable segment
        /// </summary>
        PT_LOAD = 0x1,

        /// <summary>
        ///     Dynamic linking information
        /// </summary>
        PT_DYNAMIC = 0x2,

        /// <summary>
        ///     Interpreter information
        /// </summary>
        PT_INTERP = 0x3,

        /// <summary>
        ///     Auxiliary information
        /// </summary>
        PT_NOTE = 0x4,

        /// <summary>
        ///     Reserved
        /// </summary>
        PT_SHLIB = 0x5,

        /// <summary>
        ///     Segment containing program header table itself
        /// </summary>
        PTPHDR = 0x6,

        /// <summary>
        ///     Thread-Local Storage template
        /// </summary>
        PT_TLS = 0x7
    }
}
