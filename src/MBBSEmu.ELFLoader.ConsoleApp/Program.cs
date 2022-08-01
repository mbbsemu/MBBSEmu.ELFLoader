namespace MBBSEmu.ELFLoader.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading ELF File...");

            var elf = new ELFFile(@"..\..\..\..\..\examples\basic.o");

            foreach (var s in elf.SectionHeaders)
            {
                Console.WriteLine($"Loaded Section: {s.Name}");
            }
        }
    }
}