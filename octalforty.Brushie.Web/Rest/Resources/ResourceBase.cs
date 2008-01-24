namespace octalforty.Brushie.Web.Rest.Resources
{
    public abstract class ResourceBase : IResource
    {
        private string name;

        protected ResourceBase()
        {
        }

        protected ResourceBase(string name)
        {
            this.name = name;
        }

        public virtual string Name
        {
            get { return name; }
        }
    }
}
