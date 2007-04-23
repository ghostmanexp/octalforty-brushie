using System.Collections;
#if FW1
#else
using System.Collections.ObjectModel;
#endif

namespace octalforty.Brushie.Text.Authoring.Textile.Dom
{
#if FW1
	/// <summary>
	/// Represents a collection of <see cref="DomElement"/> objects.
	/// </summary>
	public sealed class DomElementCollection : ArrayList
	{
	}
#else
	/// <summary>
    /// Represents a collection of <see cref="DomElement"/> objects.
    /// </summary>
    public sealed class DomElementCollection : Collection<DomElement>
    {
    }
#endif
}
