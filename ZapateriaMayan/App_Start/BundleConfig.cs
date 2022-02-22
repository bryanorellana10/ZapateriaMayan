using System.Web;
using System.Web.Optimization;

namespace ZapateriaMayan
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery.min.js",
                         "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/Chart.min.js",
                        "~/Scripts/sparkline.js",
                        "~/Scripts/jquery.vmap.min.js",
                        "~/Scripts/jquery.vmap.usa.js",
                        "~/Scripts/jquery.knob.min.js",
                        "~/Scripts/daterangepicker.js",
                        "~/Scripts/tempusdominus-bootstrap-4.min.js",
                        "~/Scripts/summernote-bs4.min.js",
                        "~/Scripts/jquery.overlayScrollbars.min.js",
                        "~/Scripts/adminlte.js",
                        "~/Scripts/dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                    "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/all.min.css",
                      "~/Content/tempusdominus-bootstrap-4.min.css",
                      "~/Content/icheck-bootstrap.min.css",
                      "~/Content/jqvmap.min.css",
                      "~/Content/adminlte.min.css",
                      "~/Content/OverlayScrollbars.min.css",
                      "~/Content/daterangepicker.css",
                      "~/Content/summernote-bs4.css"));
        }
    }
}
