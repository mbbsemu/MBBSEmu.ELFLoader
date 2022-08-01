using MBBSEmu.ELFLoader.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MBBSEmu.ELFLoader
{
    /// <summary>
    ///     Represents an ELF File that is Loaded and Parsed into a usable format
    /// </summary>
    public class ELFFile
    {
        /// <summary>
        ///     ELF Header Signature ix 0x7F + 'ELF'
        /// </summary>
        private static readonly byte[] ELF_HEADER_SIGNATURE = new byte[] { 0x7F, 0x45, 0x4C, 0x46 };

        public readonly byte[] FileData;

        /// <summary>
        /// 	This byte is set to either 1 or 2 to signify 32- or 64-bit format, respectively.
        /// </summary>
        public byte EI_CLASS => FileData[0x4];

        /// <summary>
        ///     This byte is set to either 1 or 2 to signify little or big endianness, respectively.
        ///     This affects interpretation of multi-byte fields starting with offset 0x10
        /// </summary>
        public byte EI_DATA => FileData[0x5];

        /// <summary>
        /// 	Identifies the target operating system ABI.
        /// </summary>
        public EnumOSABI EI_OSBI => (EnumOSABI)FileData[0x6];

        /// <summary>
        ///     Further specifies the ABI version.
        /// </summary>
        public byte EI_ABIVERSION => FileData[0x7];

        /// <summary>
        ///     Identifies object file type
        /// </summary>
        public EnumObjectFileType E_TYPE => (EnumObjectFileType)FileData[0x10];

        /// <summary>
        ///     Specifies target instruction set architecture
        /// </summary>
        public EnumMachineISA E_MACHINE => (EnumMachineISA)BitConverter.ToUInt16(FileData, 0x12);

        /// <summary>
        ///     Set to 1 for the original version of ELF.
        /// </summary>
        public byte E_VERSION => FileData[0x14];

        /// <summary>
        ///     This is the memory address of the entry point from where the process starts executing.
        ///     This field is either 32 or 64 bits long, depending on the format defined earlier (byte 0x04).
        ///     If the file doesn't have an associated entry point, then this holds zero.
        /// </summary>
        public ulong E_ENTRY => EI_CLASS == 1 ? BitConverter.ToUInt32(FileData, 0x18) : BitConverter.ToUInt64(FileData, 0x18);

        /// <summary>
        ///     Start of the program header table.
        ///     It usually follows the file header immediately following this one, making the offset 0x34 or 0x40 for 32- and 64-bit ELF executables, respectively.
        /// </summary>
        public ulong E_PHOFF => EI_CLASS == 1 ? BitConverter.ToUInt32(FileData, 0x1C) : BitConverter.ToUInt64(FileData, 0x20);

        /// <summary>
        ///     Start of the section header table.
        /// </summary>
        public ulong E_SHOFF => EI_CLASS == 1 ? BitConverter.ToUInt32(FileData, 0x20) : BitConverter.ToUInt64(FileData, 0x28);

        /// <summary>
        ///     Interpretation of this field depends on the target architecture.
        /// </summary>
        public uint E_FLAGS => EI_CLASS == 1 ? BitConverter.ToUInt32(FileData, 0x24) : BitConverter.ToUInt32(FileData, 0x30);

        /// <summary>
        ///     Size of the file header, normally 64 Bytes for 64-bit and 52 Bytes for 32-bit format.
        /// </summary>
        public ushort E_EHSIZE => EI_CLASS == 1 ? BitConverter.ToUInt16(FileData, 0x28) : BitConverter.ToUInt16(FileData, 0x34);

        /// <summary>
        ///     Size of a program header table entry.
        /// </summary>
        public ushort E_PHENTSIZE => EI_CLASS == 1 ? BitConverter.ToUInt16(FileData, 0x2A) : BitConverter.ToUInt16(FileData, 0x36);

        /// <summary>
        ///     Number of entries in the program header table.
        /// </summary>
        public ushort E_PHNUM => EI_CLASS == 1 ? BitConverter.ToUInt16(FileData, 0x2C) : BitConverter.ToUInt16(FileData, 0x38);

        /// <summary>
        ///     Size of a section header table entry
        /// </summary>
        public ushort E_SHENTSIZE => EI_CLASS == 1 ? BitConverter.ToUInt16(FileData, 0x2E) : BitConverter.ToUInt16(FileData, 0x3A);

        /// <summary>
        ///     Number of entries in the section header table
        /// </summary>
        public ushort E_SHNUM => EI_CLASS == 1 ? BitConverter.ToUInt16(FileData, 0x30) : BitConverter.ToUInt16(FileData, 0x3C);

        /// <summary>
        ///     Index of the section header table entry that contains the section names
        /// </summary>
        public ushort E_SHSTRNDX => EI_CLASS == 1 ? BitConverter.ToUInt16(FileData, 0x32) : BitConverter.ToUInt16(FileData, 0x3E);

        /// <summary>
        ///     Collection of Program Headers Parsed
        /// </summary>
        public List<ProgramHeader> ProgramHeaders;

        /// <summary>
        ///     Collection of Section Headers Parsed
        /// </summary>
        public List<SectionHeader> SectionHeaders;

        public ELFFile(string pathToFile)
        {
            FileData = File.ReadAllBytes(pathToFile);

            Load();
        }

        public ELFFile(ReadOnlySpan<byte> fileData)
        {
            FileData = fileData.ToArray();

            Load();
        }

        /// <summary>
        ///     Load the ELF File
        /// </summary>
        /// <exception cref="FileLoadException"></exception>
        private void Load()
        {
            if (FileData.Length == 0)
                throw new FileLoadException("No ELF Data to Parse");

            if (FileData.Length < 4 ||
                !new ReadOnlySpan<byte>(FileData)[..4].SequenceEqual(new ReadOnlySpan<byte>(ELF_HEADER_SIGNATURE)))
                throw new FileLoadException("Invalid ELF File Header");

            var fileDataSpan = new ReadOnlySpan<byte>(FileData);

            //Load Sections
            SectionHeaders = new List<SectionHeader>(E_SHNUM);
            for (var i = E_SHOFF; i < E_SHOFF + (ulong)(E_SHNUM * E_SHENTSIZE); i+= E_SHENTSIZE)
            {
                var section = new SectionHeader(fileDataSpan.Slice((int)i, E_SHENTSIZE),
                    EI_CLASS == 1 ? EnumAddressingMode.X86_32 : EnumAddressingMode.X86_64);
                section.LoadDate(fileDataSpan);
                SectionHeaders.Add(section);
            }

            //Load Section Names
            var sectionNames = SectionHeaders[E_SHSTRNDX];
            foreach (var s in SectionHeaders)
            {
                s.Name = GetNullTerminatedString((int)s.SH_NAME, sectionNames.Data);
            }
        }

        /// <summary>
        ///     Returns the string that was terminated with a null from the specified Source
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private static string GetNullTerminatedString(int offset, ReadOnlySpan<byte> source)
        {
            var output = new StringBuilder();
            foreach (var b in source[offset..])
            {
                if (b == 0)
                    return output.ToString();

                output.Append((char)b);
            }

            return output.ToString();
        }
    }
}
