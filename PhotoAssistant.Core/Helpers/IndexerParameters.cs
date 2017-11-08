using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PhotoAssistant.Core.Helpers {
    public class IndexerParameters {
        [Option('i', nameof(IndexPath), Required = true)]
        public string IndexPath {
            get; set;
        }
        [Option('d', nameof(DataSource), Required = true)]
        public string DataSource {
            get; set;
        }
        [Option('t', nameof(ThumbWidth), Required = true)]
        public int ThumbWidth {
            get; set;
        }
        [Option('p', nameof(PreviewWidth), Required = true)]
        public int PreviewWidth {
            get; set;
        }
        public static string CreateCommandLine(IndexerParameters parameters) => UnParserExtensions.FormatCommandLine<IndexerParameters>(Parser.Default, parameters);
        public static IndexerParameters ParseCommandLine(IEnumerable<string> args) {
            ParserResult<IndexerParameters> res = Parser.Default.ParseArguments<IndexerParameters>(args);
            IndexerParameters result = res.MapResult(options => {
                return options;
            }, errors => {
                return null;
            });
            return result;
        }
    }
}