namespace EDPlugin
{
    public class ManagedCallbacks
    {
        public delegate bool EDDRequestRefresh(/*int lastjid*/);

        public EDDRequestRefresh RequestRefresh; // placeholder... might be more...
    }
}
