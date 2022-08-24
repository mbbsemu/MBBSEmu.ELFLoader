using System;
namespace MBBSEmu.ELFLoader.Structs
{
    public class Elf32_Rela
    {
        private readonly byte[] _data;

        public uint Offset => BitConverter.ToUInt32(_data, 0);

        public uint Info => BitConverter.ToUInt32(_data, 4);

        public int Addend => BitConverter.ToInt32(_data, 8);

        public ReadOnlySpan<byte> Data => _data;

        public Elf32_Rela(ReadOnlySpan<byte> data)
        {
            if (data.Length != 8)
                throw new ArgumentException($"Invalid Relocation Data Length: {data.Length}, Expected: 8");

            //Make a Copy
            _data = data.ToArray();
        }
    }
}

