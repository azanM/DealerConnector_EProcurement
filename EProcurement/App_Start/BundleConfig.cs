using System.Web.Optimization;

namespace EProcurement
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            StyleBundle styles = new StyleBundle("~/bundles/Content/css");
            styles.Include("~/Content/css/bootstrap.css", new CssRewriteUrlTransform());
            styles.Include("~/Content/css/font-awesome.css", new CssRewriteUrlTransform());
            styles.Include("~/Content/css/ace-fonts.css", new CssRewriteUrlTransform());
            styles.Include("~/Content/css/ace.css", new CssRewriteUrlTransform());
            styles.Include("~/Content/css/select2.css", new CssRewriteUrlTransform());
            styles.Include("~/Content/css/select2-bootstrap.css", new CssRewriteUrlTransform());
            styles.Include("~/Content/css/datepicker.css", new CssRewriteUrlTransform());
            styles.Include("~/Content/css/bootstrap-datetimepicker.css", new CssRewriteUrlTransform());
            styles.Include("~/Content/css/ace-fonts.css", new CssRewriteUrlTransform());
            styles.Include("~/Content/css/ace.onpage-help.css", new CssRewriteUrlTransform());
            styles.Include("~/Scripts/js/themes/sunburst.css", new CssRewriteUrlTransform());


            ScriptBundle scripts = new ScriptBundle("~/bundles/js");           
            scripts.Include("~/Scripts/js/jquery.js");
            scripts.Include("~/Scripts/js/bootstrap.js");
            scripts.Include("~/Scripts/js/ace-extra.js");
            scripts.Include("~/Scripts/js/jquery-ui.custom.js");
            scripts.Include("~/Scripts/js/select2.js");
            scripts.Include("~/Scripts/js/jquery.ui.touch-punch.js");
            scripts.Include("~/Scripts/js/jquery.easypiechart.js");
            scripts.Include("~/Scripts/js/jquery.sparkline.js");
            scripts.Include("~/Scripts/js/flot/jquery.flot.js");
            scripts.Include("~/Scripts/js/flot/jquery.flot.pie.js");
            scripts.Include("~/Scripts/js/jquery.flot.resize.js");
            scripts.Include("~/Scripts/js/ace/elements.scroller.js");
            scripts.Include("~/Scripts/js/ace/elements.colorpicker.js");
            scripts.Include("~/Scripts/js/ace/elements.fileinput.js");
            scripts.Include("~/Scripts/js/ace/elements.typeahead.js");
            scripts.Include("~/Scripts/js/ace/elements.wysiwyg.js");
            scripts.Include("~/Scripts/js/ace/elements.spinner.js");
            scripts.Include("~/Scripts/js/ace/elements.treeview.js");
            scripts.Include("~/Scripts/js/ace/elements.wizard.js");
            scripts.Include("~/Scripts/js/ace/elements.aside.js");
            scripts.Include("~/Scripts/js/date-time/moment.js");
            scripts.Include("~/Scripts/js/date-time/bootstrap-datepicker.js");
            scripts.Include("~/Scripts/js/date-time/bootstrap-datetimepicker.js");
            scripts.Include("~/Scripts/js/dataTables/jquery.dataTables.js");
            scripts.Include("~/Scripts/js/dataTables/jquery.dataTables.bootstrap.js");
            scripts.Include("~/Scripts/js/dataTables/extensions/TableTools/js/dataTables.tableTools.js");
            scripts.Include("~/Scripts/js/dataTables/extensions/ColVis/js/dataTables.colVis.js");
            scripts.Include("~/Scripts/js/ace/ace.js");
            scripts.Include("~/Scripts/js/ace/ace.ajax-content.js");
            scripts.Include("~/Scripts/js/ace/ace.touch-drag.js");
            scripts.Include("~/Scripts/js/ace/ace.sidebar.js");
            scripts.Include("~/Scripts/js/ace/ace.sidebar-scroll-1.js");
            scripts.Include("~/Scripts/js/ace/ace.submenu-hover.js");
            scripts.Include("~/Scripts/js/ace/ace.widget-box.js");
            scripts.Include("~/Scripts/js/ace/ace.settings.js");
            scripts.Include("~/Scripts/js/ace/ace.settings-rtl.js");
            scripts.Include("~/Scripts/js/ace/ace.settings-skin.js");
            scripts.Include("~/Scripts/js/ace/ace.widget-on-reload.js");
            scripts.Include("~/Scripts/js/ace/ace.searchbox-autocomplete.js");
            scripts.Include("~/Scripts/js/ace/elements.onpage-help.js");
            scripts.Include("~/Scripts/js/ace/ace.onpage-help.js");
       
            bundles.Add(scripts);
            bundles.Add(styles);

            // https://goo.gl/y3tawY
            BundleTable.EnableOptimizations = true;


        }
    }
}
