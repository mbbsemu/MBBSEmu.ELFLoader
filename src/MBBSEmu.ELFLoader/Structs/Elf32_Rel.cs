using System;
namespace MBBSEmu.ELFLoader.Structs
{
    public class Elf32_Rel
    {
        private readonly byte[] _data;

        public uint Offset => BitConverter.ToUInt32(_data, 0);

        public uint Info => BitConverter.ToUInt32(_data, 4);

        public ReadOnlySpan<byte> Data => _data;

        public Elf32_Rel(ReadOnlySpan<byte> data)
        {
            if (data.Length != 8)
                throw new ArgumentException($"Invalid Relocation Data Length: {data.Length}, Expected: 8");

            //Make a Copy
            _data = data.ToArray();
        }
    }

}

