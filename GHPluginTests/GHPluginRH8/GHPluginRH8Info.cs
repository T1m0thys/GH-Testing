using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace GHPluginRH8
{
    public class GhPluginRh8Info : GH_AssemblyInfo
    {
        public override string Name => "GHPluginRH8";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("7599a5b5-845a-4075-bb9d-5d6e33ad58be");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}