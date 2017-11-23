using Belatrix.DocumentTranslator.Models;
using CsvHelper.Configuration;

namespace Belatrix.DocumentTranslator.Map
{
    public sealed class RetailerMap : ClassMap<Example>
    {
        public RetailerMap()
        {
            AutoMap();
        }
    }
}
