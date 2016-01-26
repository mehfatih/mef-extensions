namespace mef_extensions
{
    public sealed class DefaultImporter : ImporterBase
    {
        private readonly object[] _composedParts;

        public DefaultImporter(object[] composedParts)
        {
            _composedParts = composedParts;
        }

        public override object[] GetComposedParts()
        {
            return _composedParts;
        }
    }
}