using System.ComponentModel;

namespace Sids.Prodesp.Model.Enum
{
    /// <summary>
    /// Representa o target para um Recurso (URL).
    /// Valores possíveis: _blank, _self, _parent e _top.
    /// </summary>
    public enum URLTarget
    {
        /// <summary>
        /// Opens the linked document in the same frame as it was clicked (this is default).
        /// </summary>
        [Description("_urlTargetSelf")]
        _self,

        /// <summary>
        /// Opens the linked document in a new window or tab.
        /// </summary>
        [Description("_urlTargetBlank")]
        _blank,

        /// <summary>
        /// Opens the linked document in the parent frame.
        /// </summary>
        [Description("_urlTargetParent")]
        _parent,

        /// <summary>
        /// Opens the linked document in the full body of the window.
        /// </summary>
        [Description("_urlTargetTop")]
        _top
    }
}
