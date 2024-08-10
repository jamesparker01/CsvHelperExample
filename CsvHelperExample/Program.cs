using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

var csv = new CsvRepository().ReadCsv().ToArray();

foreach (var row in csv)
{
    Console.WriteLine($"Id: {row.Id} Name: {row.Name}");
}
Console.ReadKey();

public class CsvRepository
{
    public IEnumerable<Foo> ReadCsv()
    {
        using (var reader = new StreamReader("testdata.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<FooMap>();
            return csv.GetRecords<Foo>().ToList();
        }
    }
}

public record Foo
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public sealed class FooMap : ClassMap<Foo>
{
    public FooMap()
    {
        Map(m => m.Id).Name("ColumnA");
        Map(m => m.Name).Name("ColumnD");
    }
}