using MBBSEmu.ELFLoader.Enums;
using System;

namespace MBBSEmu.ELFLoader
{
    /// <summary>
    ///     ELF Section Header
    /// </summary>
    public class SectionHeader
    {
        private readonly byte[] _sectionHeaderData;

        private readonly EnumAddressingMode _addressingMode;

        /// <summary>
        ///     An offset to a String in the .shstrtab Section that represents the name of this section
        /// </summary>
        public uint SH_NAME => BitConverter.ToUInt32(_sectionHeaderData, 0);

        /// <summary>
        ///     Identifies the type of this Section
        /// </summary>
        public EnumSectionType SH_TYPE => (EnumSectionType)BitConverter.ToUInt32(_sectionHeaderData, 0x4);

        /// <summary>
        ///     Attributes of the section
        /// </summary>
        public EnumSectionFlags SH_FLAGS => _addressingMode == EnumAddressingMode.X86_32
            ? (EnumSectionFlags)BitConverter.ToUInt32(_sectionHeaderData, 0x08)
            : (EnumSectionFlags)BitConverter.ToUInt64(_sectionHeaderData, 0x08);

        /// <summary>
        ///     Virtual address of the section in memory, for sections that are loaded
        /// </summary>
        public ulong SH_ADDR => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x0C)
            : BitConverter.ToUInt64(_sectionHeaderData, 0x10);

        /// <summary>
        ///     Absolute Offset of the section in the file image
        /// </summary>
        public ulong SH_OFFSET => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x10)
            : BitConverter.ToUInt64(_sectionHeaderData, 0x18);

        /// <summary>
        ///     Size in bytes of the section in the file image
        ///
        ///     May be 0
        /// </summary>
        public ulong SH_SIZE => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x14)
            : BitConverter.ToUInt64(_sectionHeaderData, 0x20);

        /// <summary>
        ///     Contains the section index of an associated section
        ///
        ///     This field is used for several purposes, depending on the type of section
        /// </summary>
        public uint SH_LINK => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x18)
            : BitConverter.ToUInt32(_sectionHeaderData, 0x28);

        /// <summary>
        ///     Contains extra information about the section
        ///
        ///     This field is used for several purposes, depending on the type of section
        /// </summary>
        public uint SH_INFO => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x1C)
            : BitConverter.ToUInt32(_sectionHeaderData, 0x2C);

        /// <summary>
        ///     Contains the required alignment of the section
        ///
        ///     This field must be a power of two
        /// </summary>
        public ulong SH_ADDRALIGN => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x20)
            : BitConverter.ToUInt64(_sectionHeaderData, 0x30);

        /// <summary>
        ///     Contains the size, in bytes, of each entry, for sections that contain fixed-size entries
        ///
        ///     Otherwise, this field contains zero
        /// </summary>
        public ulong SH_ENTSIZE => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x24)
            : BitConverter.ToUInt64(_sectionHeaderData, 0x38);

        public SectionHeader(ReadOnlySpan<byte> sectionHeaderData, EnumAddressingMode addressingMode)
        {
            _sectionHeaderData = sectionHeaderData.ToArray();
            _addressingMode = addressingMode;
        }

        /// <summary>
        ///     Specified Name of Segment
        ///
        ///     This is explicitly loaded external of the header as Segment Names themselves are stored
        ///     within a specific segment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Segment Data
        ///
        ///     This needs to be explicitly loaded
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        ///     Absolute Offset of Segment within the ELF File it is loaded from
        /// </summary>
        public uint AbsoluteOffset { get; set; }

        /// <summary>
        ///     Loads the Segment Data from the provided ELF File Data
        /// </summary>
        /// <param name="fileData"></param>
        public void LoadData(ReadOnlySpan<byte> fileData)
        {
            Data = fileData.Slice((int)SH_OFFSET, (int)SH_SIZE).ToArray();
        }

    }
}
