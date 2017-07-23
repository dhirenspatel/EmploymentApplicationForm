using System.Web;
using System.Web.Optimization;

namespace EmployInformationSystem
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
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/datatableCss").Include(
                "~/Content/DataTables/jquery.dataTables.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/datatableJs").Include(
               "~/Scripts/DataTables/jquery.dataTables.js"
               ));

            bundles.Add(new StyleBundle("~/bundles/datepickerCss").Include(
               "~/Content/_bootstrap-datetimepicker.less"
               ));

            bundles.Add(new ScriptBundle("~/bundles/datepickerJs").Include(
                "~/Scripts/moment.js",
               "~/Scripts/bootstrap-datetimepicker.js"
               ));

            bundles.Add(new StyleBundle("~/bundles/jqueryuiCss").Include(
               "~/Content/themes/base/jquery-ui.css"
               ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryuiJs").Include(
               "~/Scripts/jquery-ui-1.12.1.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusiveJs").Include(
             "~/Scripts/jquery.unobtrusive-ajax.js"
             ));

        }
    }
}
