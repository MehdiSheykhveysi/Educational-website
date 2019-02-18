namespace Site.Core.Infrastructures.Utilities
{
    public static class Assert
    {
        public static bool NotNull<T>(T value) where T : class
        {
            if (value == null)
                return false;
            else
                return true;
        }
    }
}
