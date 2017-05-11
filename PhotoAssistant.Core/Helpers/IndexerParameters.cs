using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using System.Threading.Tasks;
using System.Drawing;

namespace PhotoAssistant.Core.Helpers {
    public class IndexerParameters {
        [Option('i', "IndexPath", Required = true)]
        public string IndexPath { get; set; }
        [Option('d', "DataSource", Required = true)]
        public string DataSource { get; set; }
        [Option('t', "ThumbWidth", Required = true)]
        public int ThumbWidth { get; set; }
        [Option('p', "PreviewWidth", Required = true)]
        public int PreviewWidth { get; set; }

        public static string CreateCommandLine(IndexerParameters parameters) {
            return UnParserExtensions.FormatCommandLine<IndexerParameters>(Parser.Default, parameters);
        }
        public static IndexerParameters ParseCommandLine(IEnumerable<string> args) {
            var res = Parser.Default.ParseArguments<IndexerParameters>(args);
            IndexerParameters result = res.MapResult(
              options => {
                  return options;
              },
              errors => {
                  return null;
              });
            return result;
        }
    }
}