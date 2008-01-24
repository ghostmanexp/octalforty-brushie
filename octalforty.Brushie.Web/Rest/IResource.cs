using octalforty.Brushie.Web.Rest.Conversion;

namespace octalforty.Brushie.Web.Rest
{
    public interface IResource
    {
        IRepresentationSerializer[] RepresentationSerializers
        { get; }

    }
}
