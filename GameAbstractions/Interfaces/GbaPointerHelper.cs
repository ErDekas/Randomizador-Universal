namespace GameAbstractions.Interfaces
{
    public static class GbaPointerHelper
    {
        public const uint RomBase = 0x08000000;

        public static long ToOffset(uint pointer)
            => pointer - RomBase;
    }

}
