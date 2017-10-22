using System.Web.Optimization;

namespace SmartBazaar.Web
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-js").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap-css").Include(
                    "~/Content/bootstrap.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/custom.min.css",
                        "~/Content/site.css"
                      ));

            bundles.Add(new StyleBundle("~/bundles/fonts").Include(
                        "~/Content/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js"
                ));

            //plugins

            bundles.Add(new StyleBundle("~/plugins/datepicker-css").Include(
                "~/Content/bootstrap-datepicker.min.css",
                "~/Content/bootstrap-datepicker3.min.css"
                ));

            bundles.Add(new ScriptBundle("~/plugins/datepicker-js").Include(
                "~/Scripts/bootstrap-datepicker.min.js",
                "~/Scripts/locales/bootstrap-datepicker.tr.min.js"
                ));

            bundles.Add(new StyleBundle("~/plugins/jstree-css").Include(
                "~/Content/themes/default/style.min.css"
                ));

            bundles.Add(new ScriptBundle("~/plugins/jstree-js").Include(
                "~/Scripts/jstree.min.js"
                ));

            bundles.Add(new ScriptBundle("~/plugins/summernote-js").Include(
                "~/Scripts/summernote/summernote.min.js"
                ));

            bundles.Add(new StyleBundle("~/plugins/summernote-css").Include(
                "~/Scripts/summernote/summernote.css"
                ));

            bundles.Add(new StyleBundle("~/plugins/select2-css").Include("~/Content/css/select2.css"));
            bundles.Add(new ScriptBundle("~/plugins/select2-js").Include(
                "~/Scripts/select2.js"
                ));

            bundles.Add(new StyleBundle("~/plugins/fancybox-css").Include(
                "~/Content/jquery.fancybox.css"
                ));
            bundles.Add(new ScriptBundle("~/plugins/fancybox-js").Include(
                "~/Scripts/jquery.fancybox.pack.js",
                "~/Scripts/jquery.fancybox.mousewheel-3.0.6.pack.js"
                ));

            bundles.Add(new StyleBundle("~/plugins/touchspin-css").Include(
                "~/Content/jquery.bootstrap-touchspin.min.css"
                ));
            bundles.Add(new ScriptBundle("~/plugins/touchspin-js").Include(
                "~/Scripts/jquery.bootstrap-touchspin.min.js"
                ));

            bundles.Add(new StyleBundle("~/plugins/superfish-css").Include(
                "~/Content/superfish.css",
                "~/Content/superfish-vertical.css",
                "~/Content/superfish-navbar.css"
                ));

            bundles.Add(new ScriptBundle("~/plugins/superfish-js").Include(
                "~/Scripts/supersubs.js",
                "~/Scripts/superfish.min.js",
                "~/Scripts/hoverintent.js"
                ));

            //metronic

            //general

            bundles.Add(new StyleBundle("~/metronic/global-css").Include(
                "~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                "~/Content/themes/uniformjs/default/css/uniform.default.min.css",
                "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                "~/Content/themes/base/theme.css"
                ));

            bundles.Add(new StyleBundle("~/metronic/theme-css").Include(
                "~/assets/global/css/components.css",
                "~/assets/global/css/plugins.css",
                "~/assets/admin/layout/css/layout.css",
                "~/assets/admin/layout/css/themes/default.css",
                "~/assets/admin/layout/css/custom.css"
                ));

            bundles.Add(new ScriptBundle("~/metronic/global-js").Include(
                "~/Scripts/jquery-migrate-{version}.min.js",
                "~/Scripts/jquery-ui-{version}.min.js",
                "~/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                "~/Scripts/jquery.slimscroll.min.js",
                "~/Scripts/jquery.blockUI.js",
                "~/assets/global/plugins/jquery.cokie.min.js",
                "~/Scripts/jquery.uniform.min.js",
                "~/assets/global/scripts/metronic.js",
                "~/assets/admin/layout/scripts/layout.js"
                ));

            //plugins

            bundles.Add(new ScriptBundle("~/metronic/charts-js").Include(
                "~/assets/global/plugins/flot/jquery.flot.min.js",
                "~/assets/global/plugins/flot/jquery.flot.resize.min.js",
                "~/assets/global/plugins/flot/jquery.flot.pie.min.js",
                "~/assets/global/plugins/flot/jquery.flot.stack.min.js",
                "~/assets/global/plugins/flot/jquery.flot.crosshair.min.js",
                "~/assets/global/plugins/flot/jquery.flot.categories.min.js",
                "~/assets/global/plugins/flot/jquery.flot.time.js"
                ));

        }
    }
}
