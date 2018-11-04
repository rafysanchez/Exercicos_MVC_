using System.Web.Optimization;

namespace Sids.Prodesp.UI
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/js/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts1").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Content/js/jquery.bootstrap-duallistbox.js",
                "~/Content/js/moment.js",
                "~/Content/js/fullcalendar.js",
                "~/Content/js/pt-br.js",
                "~/Content/js/bootstrap-datepicker.js",
                "~/Content/js/bootstrap-datepicker.pt-BR.min.js",
                "~/Content/js/bootstrap-datetimepicker.min.js",
                "~/Content/js/jquery.mask.js",
                "~/Content/Bootstrap Validator/js/modernizr.js",
                "~/Content/Bootstrap Validator/js/bootstrapvalidator.js",
                "~/Content/jquery.confirm/jquery.confirm.js",
                "~/Content/js/waiting.js",
                "~/Content/hotkeys/hotkeys.js",
                "~/Content/js/scripts.js",
                "~/Content/js/jquery.customextensions.js"));

            bundles.Add(new StyleBundle("~/bundles/styles1").Include(
                "~/Content/bootstrap.css",
                "~/Content/css/bootstrap-custom.css",
                "~/Content/css/bootstrap-duallistbox.css",
                "~/Content/css/fullcalendar.css",
                "~/Content/css/bootstrap-datepicker.css",
                "~/Content/css/bootstrap-datetimepicker.css",
                "~/Content/BootstrapValidator/css/bootstrap-theme.min.css",
                "~/Content/BootstrapValidator/css/bootstrapValidator.min.css",
                "~/Content/css/style.css").Include("~/Content/css/font-awesome.css", new CssRewriteUrlTransform()));


            bundles.Add(new ScriptBundle("~/bundles/scriptsDataTable").Include(
                "~/Content/dataTable/js/jquery.dataTables.js",
                "~/Content/dataTable/js/dataTables.bootstrap.js",
                "~/Content/dataTable/js/moment.min.js",
                "~/Content/dataTable/js/datetime-moment.js",
                "~/Content/dataTable/Config/buildDataTable.js"));

            bundles.Add(new StyleBundle("~/bundles/stylesDataTable").Include(
                "~/Content/dataTable/css/dataTables.bootstrap.css",
                "~/Content/dataTable/css/jquery.dataTables.css"));

            bundles.Add(new ScriptBundle("~/bundles/parametrizacaonl").Include("~/Content/js/parametrizacaonl/parametrizacaonl.js"));

            #region ConfirmacaoPagamento
            bundles.Add(new StyleBundle("~/bundles/ConfirmacaoPagamentoCss").Include("~/Content/css/ConfirmacaoPagamento/ConfirmacaoPagamento.css", "~/Content/DataTable/css/responsive.dataTables.min.css", "~/Content/DataTable/css/jquery.dataTables.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/ConfirmacaoPagamentoJs").Include("~/Content/js/ConfirmacaoPagamento/ConfirmacaoPagamento.js"));
            #endregion
        }
    }
}