using Cassette;
using Cassette.HtmlTemplates;
using Cassette.Scripts;
using Cassette.Stylesheets;
using System.Text.RegularExpressions;

namespace NancyWebBlog
{
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            //TODO : 
            //reference in release mode http://getcassette.net/documentation/v1/AssetReferences
            //maybe external scripts http://getcassette.net/documentation/v1/scripts/external
            AddStylesheetBundles(bundles);
            AddTemplateBundles(bundles);
            AddScriptBundles(bundles);

        }

        private static void AddStylesheetBundles(BundleCollection bundles)
        {
            bundles.AddPerSubDirectory<StylesheetBundle>("~/Content/css");
        }

        private static void AddTemplateBundles(BundleCollection bundles)
        {
            bundles.AddPerSubDirectory<HtmlTemplateBundle>("~/Content/templates");
        }

        private static void AddScriptBundles(BundleCollection bundles)
        {
            bundles.AddPerSubDirectory<ScriptBundle>("~/Scripts", new FileSearch
            {
                Pattern = "*.js",
                Exclude = new Regex("\\.intellisense\\.js$")
            });

        }
    }
}