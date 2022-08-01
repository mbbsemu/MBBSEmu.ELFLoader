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

        public uint SH_NAME => BitConverter.ToUInt32(_sectionHeaderData, 0);

        public EnumSectionType SH_TYPE => (EnumSectionType)BitConverter.ToUInt32(_sectionHeaderData, 0x4);

        public EnumSectionFlags SH_FLAGS => _addressingMode == EnumAddressingMode.X86_32
            ? (EnumSectionFlags)BitConverter.ToUInt32(_sectionHeaderData, 0x08)
            : (EnumSectionFlags)BitConverter.ToUInt64(_sectionHeaderData, 0x08);

        public ulong SH_ADDR => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x0C)
            : BitConverter.ToUInt64(_sectionHeaderData, 0x10);

        public ulong SH_OFFSET => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x10)
            : BitConverter.ToUInt64(_sectionHeaderData, 0x18);

        public ulong SH_SIZE => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x14)
            : BitConverter.ToUInt64(_sectionHeaderData, 0x20);

        public uint SH_LINK => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x18)
            : BitConverter.ToUInt32(_sectionHeaderData, 0x28);

        public uint SH_INFO => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x1C)
            : BitConverter.ToUInt32(_sectionHeaderData, 0x2C);

        public ulong SH_ADDRALIGN => _addressingMode == EnumAddressingMode.X86_32
            ? BitConverter.ToUInt32(_sectionHeaderData, 0x20)
            : BitConverter.ToUInt64(_sectionHeaderData, 0x30);

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
        ///     Loads the Segment Data from the provided ELF File Data
        /// </summary>
        /// <param name="fileData"></param>
        public void LoadData(ReadOnlySpan<byte> fileData)
        {
            Data = fileData.Slice((int)SH_OFFSET, (int)SH_SIZE).ToArray();
        }

    }
}
