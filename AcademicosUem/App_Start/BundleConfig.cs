using System.Web;
using System.Web.Optimization;

namespace AcademicosUem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            //add no template
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*",
                        "~/Scripts/init.js",
                        "~/Scripts/jquery-2.1.1.min.js",
                        "~/Scripts/materialize.js",
                        "~/Scripts/materialize.min.js",
                        "~/Scripts/modernizr.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                      "~/Scripts/init.js",
                      "~/Scripts/jquery-2.1.1.min.js",
                      "~/Scripts/materialize.js",
                      "~/Scripts/materialize.min.js",
                      "~/Scripts/modernizr.js",
                      "~/Scripts/custom-min.js",
                     "~/Scripts/plugin-min.js"));


            //add no template
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            //add no template
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/materialize.css",
                      "~/Content/materialize.min.css",
                      "~/Content/style.css",
                      "~/Content/custom-min.css",
                      "~/Content/plugin-min.css"));

        }
    }
}
