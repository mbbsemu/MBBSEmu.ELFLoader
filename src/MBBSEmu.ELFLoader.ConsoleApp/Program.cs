using Iced.Intel;
using MBBSEmu.ELFLoader.Enums;

namespace MBBSEmu.ELFLoader.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading ELF File...");

            var elf = new ELFFile(@"..\..\..\..\..\examples\test.o");

            Console.WriteLine(elf.ToString());

            foreach (var s in elf.SectionHeaders)
            {
                Console.WriteLine($"Loaded Section: {s.Name}");
                Console.WriteLine($"Flags: {s.SH_FLAGS}");
                if (s.SH_FLAGS.HasFlag(EnumSectionFlags.SHF_EXECINSTR))
                {
                    //Decode the Segment
                    var instructionList = new InstructionList();
                    var codeReader = new ByteArrayCodeReader(s.Data);
                    var decoder = Decoder.Create(16, codeReader);
                    decoder.IP = 0x0;

                    while (decoder.IP < (ulong)s.Data.Length)
                    {
                        decoder.Decode(out instructionList.AllocUninitializedElement());
                    }

                    foreach (var i in instructionList)
                    {
                        Console.WriteLine(i);
                    }
                }
                Console.WriteLine("---------");
            }
        }
    }
}