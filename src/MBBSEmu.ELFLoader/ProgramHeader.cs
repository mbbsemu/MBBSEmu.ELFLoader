namespace MBBSEmu.ELFLoader
{
    /// <summary>
    ///     ELF Program Header
    /// </summary>
    public class ProgramHeader
    {
        private readonly byte[] _programHeaderData;

        public ProgramHeader(byte[] programHeaderData)
        {
            _programHeaderData = programHeaderData;
        }
    }
}
