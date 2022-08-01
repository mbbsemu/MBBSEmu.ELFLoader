namespace MBBSEmu.ELFLoader.Enums
{
    public enum EnumSectionType : uint
    {
        /// <summary>
        ///     Section header table entry unused
        /// </summary>
        SHT_NULL = 0,

        /// <summary>
        ///     Program data
        /// </summary>
        SHT_PROGBITS = 0x1,

        /// <summary>
        ///     Symbol table
        /// </summary>
        SHT_SYMTAB = 0x2,

        /// <summary>
        ///     String table
        /// </summary>
        SHT_STRTAB = 0x3,

        /// <summary>
        ///     Relocation entries with addends
        /// </summary>
        SHT_RELA = 0x4,

        /// <summary>
        ///     Symbol hash table
        /// </summary>
        SHT_HASH = 0x5,

        /// <summary>
        ///     Dynamic linking information
        /// </summary>
        SHT_DYNAMIC = 0x6,

        /// <summary>
        ///     Notes
        /// </summary>
        SHT_NOTE = 0x7,

        /// <summary>
        ///     Program space with no data (bss)
        /// </summary>
        SHT_NOBITS = 0x8,

        /// <summary>
        ///     Relocation entries, no addends
        /// </summary>
        SHT_REL = 0x9,

        /// <summary>
        ///     Reserved
        /// </summary>
        SHT_SHLIB = 0xA,

        /// <summary>
        ///     Dynamic linker symbol table
        /// </summary>
        SHT_DYNSYM = 0xB,

        /// <summary>
        ///     Array of constructors
        /// </summary>
        SHT_INIT_ARRAY = 0xE,

        /// <summary>
        ///     Array of destructors
        /// </summary>
        SHT_FINI_ARRAY = 0xF,

        /// <summary>
        ///     Array of pre-constructors
        /// </summary>
        SHT_PREINIT_ARRAY = 0x10,

        /// <summary>
        ///     Section group
        /// </summary>
        SHT_GROUP = 0x11,

        /// <summary>
        ///     Extended section indices
        /// </summary>
        SHT_SYMTAB_SHNDX = 0x12,

        /// <summary>
        ///     Number of defined types
        /// </summary>
        SHT_NUM = 0x13,

        /// <summary>
        ///     Start OS-specific
        /// </summary>
        SHT_LOOS = 0x60000000
    }
}
