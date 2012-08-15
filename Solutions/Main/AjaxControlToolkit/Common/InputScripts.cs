using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

[assembly: WebResource("AjaxControlToolkit.Common.Input.js", "application/x-javascript")]

namespace AjaxControlToolkit
{
    [RequiredScript(typeof(CommonToolkitScripts))]
    [ClientScriptResource(null, "AjaxControlToolkit.Common.Input.js")]
    public static class InputScripts
    {
    }
}
